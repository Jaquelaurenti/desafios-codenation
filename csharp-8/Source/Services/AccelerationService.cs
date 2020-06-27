using System.Collections.Generic;
using System.Linq;
using Codenation.Challenge.Models;
using Microsoft.EntityFrameworkCore;

namespace Codenation.Challenge.Services
{
    /*
        Método FindByAccelerationIdAndUserId: deve retornar uma lista de desafios a partir do id da aceleração e do id do usuário
        Método Save: deve retornar um desafio após salvar os dados. Caso o Id seja zero, fará a inserção da aceleração,
            caso contrário fará a atualização dos dados da aceleração com o Id fornecido.*/
    public class AccelerationService : IAccelerationService
    {
        private CodenationContext _context;

        public AccelerationService(CodenationContext context)
        {
            _context = context;
        }

        public IList<Acceleration> FindByCompanyId(int companyId)
        {
            return _context.Candidates.Where(x => x.CompanyId == companyId)
                                      .Select(x => x.Acceleration)
                                      .Distinct()
                                      .ToList();
        }

        public Acceleration FindById(int id)
        {
            return _context.Accelerations.Find(id);
        }

        public Acceleration Save(Acceleration acceleration)
        {
            var estado = acceleration.Id == 0 ? EntityState.Added : EntityState.Modified;

            _context.Entry(acceleration).State = estado;

            _context.SaveChanges();

            return acceleration;
        }
    }
}
