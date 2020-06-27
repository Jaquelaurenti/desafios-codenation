using System.Collections.Generic;
using System.Linq;
using Codenation.Challenge.Models;
using Microsoft.EntityFrameworkCore;

namespace Codenation.Challenge.Services
{
  /*    Método FindById: deve retornar um candidato a partir do id do usuário, do id da aceleração e do id da empresa
        Método FindByCompanyId: deve retornar uma lista candidatos a partir do id da empresa
        Método FindByAccelerationId: deve retornar uma lista de candidatos a partir do id da aceleração
        Método Save: deve retornar um candidato após salvar os dados. Caso a tupla UserId,
            AccelartionId e CompanyId não exista, fará a inserção do candidato, caso contrário fará a atualização dos dados do candidato
         */

    public class CandidateService : ICandidateService
    {
        private CodenationContext _context;

        public CandidateService(CodenationContext context)
        {
            _context = context;
        }

        public IList<Candidate> FindByAccelerationId(int accelerationId)
        {
            return _context.Accelerations.Where(x => x.Id == accelerationId)
                                              .SelectMany(x => x.Candidates)
                                              .Distinct()
                                              .ToList();
        }

        public IList<Candidate> FindByCompanyId(int companyId)
        {
            return _context.Companies.Where(x => x.Id == companyId)
                                     .SelectMany(x => x.Candidates)
                                     .Distinct()
                                     .ToList();
        }

        public Candidate FindById(int userId, int accelerationId, int companyId)
        {
            return _context.Candidates.Find(userId, accelerationId, companyId);
        }

        public Candidate Save(Candidate candidate)
        {
            var existe = _context.Candidates.Find(candidate.UserId, candidate.AccelerationId, candidate.CompanyId);

            if (existe == null)
            {
                _context.Candidates.Add(candidate);
            }
            else
            {
                existe.Status = candidate.Status;
            }   

            _context.SaveChanges();


            return candidate;
        }
    }
}
