using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using TodoApp.Data;

namespace TodoApp.Infrastructure
{
    public class AdditionalUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<AppUser, IdentityRole>
    {
        public AdditionalUserClaimsPrincipalFactory(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<IdentityOptions> options) : base(userManager, roleManager, options)
        {
        }

        public override async Task<ClaimsPrincipal> CreateAsync(AppUser user)
        {
            // gather any services and data we need
            var principal = await base.CreateAsync(user);
            var identity = (ClaimsIdentity)principal.Identity;

            var claims = new List<Claim>();

            // validate it
            if (user.Email != null)
            {
                claims.Add(new Claim("CustomEmail", user.Email));
            }
            else
            {
                claims.Add(new Claim("CustomEmail", "none"));
            }

            // add our claims

            identity.AddClaims(claims);

            return principal;
        }
    }
}