using System.Collections.Generic;
using System.Linq;
using Codenation.Challenge.Models;
using Microsoft.EntityFrameworkCore;

namespace Codenation.Challenge.Services
{
    /*  Método FindById: deve retornar uma empresa a partir do id da empresa
        Método FindByAccelerationId: deve retornar uma lista de empresas a partir do id da aceleração
        Método FindByUserId: deve retornar uma lista de empresas a partir do id do usuário
        Método Save: deve retorna uma empresa após salvar os dados. Caso o Id da instância não seja fornecido, fará a inserção da empresa, caso contrário fará a atualização dos dados da empresa com o Id informado
    */
    public class CompanyService : ICompanyService
    {
        private CodenationContext _context;
        public CompanyService(CodenationContext context)
        {
            _context = context;
        }

        public IList<Company> FindByAccelerationId(int accelerationId)
        {
            return _context.Candidates.Where(x => x.AccelerationId == accelerationId)
                                      .Select(x => x.Company)
                                      .Distinct()
                                      .ToList();
        }

        public Company FindById(int id)
        {
            return _context.Companies.Find(id);
        }

        public IList<Company> FindByUserId(int userId)
        {
            return _context.Candidates.Where(x => x.UserId == userId)
                                      .Select(x => x.Company)
                                      .Distinct()
                                      .ToList();

        }

        public Company Save(Company company)
        {

            var estado = company.Id == 0 ? EntityState.Added : EntityState.Modified;

            _context.Entry(company).State = estado;

            _context.SaveChanges();

            return company;
        }
    }
}