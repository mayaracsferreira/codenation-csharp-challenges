using Codenation.Challenge.Models;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using System.Linq;
using System.Threading.Tasks;

namespace Codenation.Challenge.Services
{
    public class PasswordValidatorService : IResourceOwnerPasswordValidator
    {
        private CodenationContext _context;
        public PasswordValidatorService(CodenationContext dbContext)
        {
            _context = dbContext;
        }

        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email == context.UserName);

            if (user != null && user.Password.TrimEnd() == context.Password)
            {
                context.Result = new GrantValidationResult(
                    subject: user.Id.ToString(),
                    authenticationMethod: "custom",
                    claims: UserProfileService.GetUserClaims(user)
                );
                return Task.CompletedTask;
            }
            else
            {
                context.Result = new GrantValidationResult(
                    TokenRequestErrors.InvalidGrant, "Usuario ou senha invalidos");

                return Task.FromResult(context.Result);
            }
        }

    }
}