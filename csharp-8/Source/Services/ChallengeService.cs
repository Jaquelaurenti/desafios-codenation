using System.Collections.Generic;
using System.Linq;
using Codenation.Challenge.Models;
using Microsoft.EntityFrameworkCore;

namespace Codenation.Challenge.Services
{
    /*  Método FindByAccelerationIdAndUserId: deve retornar uma lista de desafios a partir do id da aceleração e do id do usuário
        Método Save: deve retornar um desafio após salvar os dados. Caso o Id seja zero, fará a inserção da aceleração,
        caso contrário fará a atualização dos dados da aceleração com o Id fornecido.*/

    public class ChallengeService : IChallengeService
    {
        private CodenationContext _context;

        public ChallengeService(CodenationContext context)
        {
            _context = context;
        }

        public IList<Models.Challenge> FindByAccelerationIdAndUserId(int accelerationId, int userId)
        {
            return _context.Users.Where(x => x.Id == userId)
                                              .SelectMany(x => x.Candidates)
                                              .Where(x => x.AccelerationId == accelerationId)
                                              .Select(x => x.Acceleration.Challenge)
                                              .Distinct()
                                              .ToList();
        }

        public Models.Challenge Save(Models.Challenge challenge)
        {
            var estado = challenge.Id == 0 ? EntityState.Added : EntityState.Modified;

            _context.Entry(challenge).State = estado;

            _context.SaveChanges();

            return challenge;
        }
    }
}