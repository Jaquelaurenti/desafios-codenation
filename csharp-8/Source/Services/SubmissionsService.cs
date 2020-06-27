using System.Collections.Generic;
using System.Linq;
using Codenation.Challenge.Models;
using Microsoft.EntityFrameworkCore;

namespace Codenation.Challenge.Services
{
    /*
        Método FindHigherScoreByChallengeId: deve retornar o valor do maior score a partir do id do desafio
        Método FindByChallengeIdAndAccelerationId: deve retornar uma lista de submissões a partir do id do desafio e do id da aceleração
        Método Save: deve retornar uma submissão após salvar os dados. Caso a tupla UserId e ChallengeId não exista, fará a inserção da submissão, caso contrário fará a atualização dos dados da submissão
     */
    public class SubmissionService : ISubmissionService
    {
        private CodenationContext _context;

        public SubmissionService(CodenationContext context)
        {
            _context = context;
        }

        public IList<Submission> FindByChallengeIdAndAccelerationId(int challengeId, int accelerationId)
        {
            var listSubmission = _context.Candidates.Where(x => x.AccelerationId == accelerationId)
                                                    .Select(x => x.User)
                                                    .SelectMany(x => x.Submissions)
                                                    .Where(x => x.ChallengeId == challengeId)
                                                    .Distinct()
                                                    .ToList();

            return listSubmission;
        }

        public decimal FindHigherScoreByChallengeId(int challengeId)
        {
            return _context.Submissions.Where(x => x.ChallengeId == challengeId).Max(x => x.Score);
        }

        public Submission Save(Submission submission)
        {
            var existe = _context.Submissions.Find(submission.UserId, submission.ChallengeId) == null
                ? _context.Add(submission) : _context.Update(submission.Score);

            _context.SaveChanges();

            return submission;
        }
    }
}
