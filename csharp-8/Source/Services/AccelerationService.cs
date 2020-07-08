using System.Collections.Generic;
using System.Linq;
using Codenation.Challenge.Models;
using Microsoft.EntityFrameworkCore;

namespace Codenation.Challenge.Services
{
    public class AccelerationService : IAccelerationService
    {
        private readonly CodenationContext _context;
        public AccelerationService(CodenationContext context)
        {
            _context = context;
        }

        public IList<Acceleration> FindByCompanyId(int companyId)
        {
            var result = this._context.Candidates
                .Include(_ => _.Acceleration)
                .Include(_ => _.Company)
                .Where(_ => _.Company.Id == companyId)
                .Select(_ => _.Acceleration)
                .Distinct()
                .ToList();

            return result;
        }

        public Acceleration FindById(int id)
        {
            var result = this._context.Accelerations
               .Where(_ => _.Id == id)
               .FirstOrDefault();

            return result;
        }

        public Acceleration Save(Acceleration acceleration)
        {
            var result = this.FindById(acceleration.Id);

            if (result != null)
            {
                this._context.Entry(result).CurrentValues.SetValues(acceleration);
                this._context.SaveChanges();

                return this.FindById(acceleration.Id); ;
            }
            else
            {
                this._context.Accelerations.Add(acceleration);
                this._context.SaveChanges();
                return this.FindById(acceleration.Id); ;
            }
        }
    }
}
