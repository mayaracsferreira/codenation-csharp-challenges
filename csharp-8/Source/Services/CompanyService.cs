using System.Collections.Generic;
using System.Linq;
using Codenation.Challenge.Models;
using Microsoft.EntityFrameworkCore;

namespace Codenation.Challenge.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly CodenationContext _context;

        public CompanyService(CodenationContext context)
        {
            _context = context;
        }

        public IList<Company> FindByAccelerationId(int accelerationId)
        {
            var result = this._context.Candidates
               .Include(_ => _.Company)
               .Include(_ => _.Acceleration)
               .Where(_ => _.Acceleration.Id == accelerationId)
               .Select(_ => _.Company)
               .Distinct()
               .ToList();

            return result;
        }

        public Company FindById(int id)
        {
            var result = this._context.Companies                
                .Where(_ => _.Id == id)
                .FirstOrDefault();

            return result;
        }

        public IList<Company> FindByUserId(int userId)
        {
            var result = this._context.Candidates
               .Include(_ => _.Company)
               .Include(_ => _.User)
               .Where(_ => _.User.Id == userId)
               .Select(_ => _.Company)
               .Distinct()
               .ToList();

            return result;
        }

        public Company Save(Company company)
        {
            var result = this.FindById(company.Id);

            if (result != null)
            {
                this._context.Entry(result).CurrentValues.SetValues(company);
                this._context.SaveChanges();

                return this.FindById(company.Id); ;
            }
            else
            {
                this._context.Companies.Add(company);
                this._context.SaveChanges();
                return this.FindById(company.Id); ;
            }
        }
    }
}