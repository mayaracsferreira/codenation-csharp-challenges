using System.Collections.Generic;
using System.Linq;
using Codenation.Challenge.Models;
using Microsoft.EntityFrameworkCore;

namespace Codenation.Challenge.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly CodenationContext _context;
        public CandidateService(CodenationContext context)
        {
            _context = context;
        }

        public IList<Candidate> FindByAccelerationId(int accelerationId)
        {
            var result = this._context.Candidates
                .Include(_ => _.Acceleration)
               .Where(_ => _.Acceleration.Id == accelerationId)
               .ToList();

            return result;
        }

        public IList<Candidate> FindByCompanyId(int companyId)
        {
            var result = this._context.Candidates
                .Include(_ => _.Company)
               .Where(_ => _.Company.Id == companyId)
               .ToList();

            return result;
        }

        public Candidate FindById(int userId, int accelerationId, int companyId)
        {
            var result = this._context.Candidates
                .Where(_ => _.UserId == userId
                    && _.AccelerationId == accelerationId
                    && _.CompanyId == companyId)
                .FirstOrDefault();

            return result;
        }

        public Candidate Save(Candidate candidate)
        {
            var result = this.FindById(candidate.UserId, candidate.AccelerationId, candidate.CompanyId);

            if (result != null)
            {
                this._context.Entry(result).CurrentValues.SetValues(candidate);
                this._context.SaveChanges();

                return this.FindById(candidate.UserId, candidate.AccelerationId, candidate.CompanyId);
            }
            else
            {
                this._context.Candidates.Add(candidate);
                this._context.SaveChanges();
                return this.FindById(candidate.UserId, candidate.AccelerationId, candidate.CompanyId);
            }
        }
    }
}
