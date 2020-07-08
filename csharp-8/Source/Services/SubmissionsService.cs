using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using Codenation.Challenge.Models;
using Microsoft.EntityFrameworkCore;

namespace Codenation.Challenge.Services
{
    public class SubmissionService : ISubmissionService
    {
        private readonly CodenationContext _context;
        public SubmissionService(CodenationContext context)
        {
            _context = context;
        }

        public Submission FindById(int userId, int challengeId)
        {
            var result = this._context.Submissions
                .Where(_ => _.UserId == userId && _.ChallengeId == challengeId)
                .FirstOrDefault();

            return result;
        }

        public IList<Submission> FindByChallengeIdAndAccelerationId(int challengeId, int accelerationId)
        {
            var result = _context.Submissions
                .Where(c => c.ChallengeId == challengeId)
                .ToList();

            return result;
        }

        public decimal FindHigherScoreByChallengeId(int challengeId)
        {
            var result = this._context.Submissions
                .Where(_ => _.ChallengeId == challengeId)
                .Select(_ => _.Score)
                .Max();

            return result;
        }

        public Submission Save(Submission submission)
        {
            var result = this.FindById(submission.UserId, submission.ChallengeId);

            if (result != null)
            {
                this._context.Entry(result).CurrentValues.SetValues(submission);
                this._context.SaveChanges();

                return this.FindById(submission.UserId, submission.ChallengeId);
            }
            else
            {
                this._context.Submissions.Add(submission);
                this._context.SaveChanges();
                return this.FindById(submission.UserId, submission.ChallengeId);
            }
        }
    }
}
