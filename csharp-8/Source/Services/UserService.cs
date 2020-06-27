using System.Collections.Generic;
using System.Linq;
using Codenation.Challenge.Models;
using Microsoft.EntityFrameworkCore;

/*
Método FindById: deve retornar um usuário a partir do id do usuário
Método FindByAccelerationName: deve retornar uma lista de usuários a partir do nome da aceleração
Método FindByCompanyId: deve retornar uma lista de usuários a relacionado com a empresa pelo id da empresa
Método Save: deve retornar um usuário após salvar os dados.
Caso o Id seja zero, fará a inserção do usuário, caso contrário fará a atualização dos dados do usuário
com o Id informado
     */

namespace Codenation.Challenge.Services
{
    public class UserService : IUserService
    {

        private CodenationContext _context;

        public UserService(CodenationContext context)
        {
            _context = context;
        }

        public IList<User> FindByAccelerationName(string name)
        {
            return _context.Candidates.Where(x => x.Acceleration.Name == name)
                                     .Select(x => x.User)
                                     .Distinct()
                                     .ToList();
        }

        public IList<User> FindByCompanyId(int companyId)
        {
           return _context.Candidates.Where(x => x.CompanyId == companyId)
                                      .Select(x => x.User)
                                      .Distinct()
                                      .ToList();
                    
        }

        public User FindById(int id)
        {
            return _context.Users.Find(id);
        }

        public User Save(User user)
        {
            var estado = user.Id == 0 ? EntityState.Added : EntityState.Modified;

            _context.Entry(user).State = estado;

            _context.SaveChanges();

            return user;
        }
    }
}
