using System.Collections.Generic;
using System.Linq;
using Codenation.Challenge.Models;
using Microsoft.EntityFrameworkCore;

namespace Codenation.Challenge.Services
{
    public class ChallengeService : IChallengeService
    {
        private readonly CodenationContext _context;
        public ChallengeService(CodenationContext context)
        {
            _context = context;
        }

        public Models.Challenge FindById(int id)
        {
            var result = this._context.Challenges
                .Where(_ => _.Id == id)
                .FirstOrDefault();

            return result;
        }

        public IList<Models.Challenge> FindByAccelerationIdAndUserId(int accelerationId, int userId)
        {
            var result = this._context.Candidates
                .Include(_ => _.Acceleration)                
                .ThenInclude(_ => _.Challenge)
                .Where(_ => _.UserId == userId && _.Acceleration.Id == accelerationId)
                .Select(_ => _.Acceleration.Challenge)
                .Distinct()
                .ToList();

            return result;
        }

        public Models.Challenge Save(Models.Challenge challenge)
        {
            var result = this.FindById(challenge.Id);

            if (result != null)
            {
                this._context.Entry(result).CurrentValues.SetValues(challenge);
                this._context.SaveChanges();

                return this.FindById(challenge.Id); ;
            }
            else
            {
                this._context.Challenges.Add(challenge);
                this._context.SaveChanges();
                return this.FindById(challenge.Id); ;
            }
        }
    }
}