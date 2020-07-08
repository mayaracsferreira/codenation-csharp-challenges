using System.Collections.Generic;
using System.Linq;
using Codenation.Challenge.Models;
using Microsoft.EntityFrameworkCore;

namespace Codenation.Challenge.Services
{
    public class UserService : IUserService
    {

        private readonly CodenationContext _context;
        public UserService(CodenationContext context)
        {
            _context = context;
        }

        public IList<User> FindByAccelerationName(string name)
        {
            var result = this._context.Candidates
                .Include(_ => _.Acceleration)
                .Include(_ => _.User)
                .Where(_ => _.Acceleration.Name.Equals(name))
                .Select(_ => _.User)
                .ToList();

            return result;
        }

        public IList<User> FindByCompanyId(int companyId)
        {
            var result = this._context.Candidates
               .Include(_ => _.Company)
               .Include(_ => _.User)
               .Where(_ => _.Company.Id == companyId)
               .Select(_ => _.User)
               .Distinct()
               .ToList();

            return result;
        }

        public User FindById(int id)
        {
            var result = this._context.Users
                .Where(_ => _.Id == id)
                .FirstOrDefault();

            return result;
        }

        public User Save(User user)
        {
            var result = this.FindById(user.Id);

            if (result != null)
            {
                this._context.Entry(result).CurrentValues.SetValues(user);
                this._context.SaveChanges();

                return this.FindById(user.Id); ;
            }
            else
            {
                this._context.Users.Add(user);
                this._context.SaveChanges();
                return this.FindById(user.Id); ;
            }

        }
    }
}
