using Microsoft.AspNetCore.Identity;
using AEMS_Zundel.Data;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace AEMS_Zundel.Infrastructure
{
    public class EmployeeManagmentPrincipalFactory : UserClaimsPrincipalFactory<EmployeeManagmentUser, IdentityRole>
    {
        public EmployeeManagmentPrincipalFactory(
            UserManager<EmployeeManagmentUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<IdentityOptions> options) : base(userManager, roleManager, options)
        {
        }

        public override async Task<ClaimsPrincipal> CreateAsync(EmployeeManagmentUser user)
        {
            // gather any services and data we need
            var principal = await base.CreateAsync(user);
            var identity = (ClaimsIdentity)principal.Identity;

            var claims = new List<Claim>();

            claims.Add(new Claim("userID", user.Id));

            identity.AddClaims(claims);

            return principal;
        }
    }
}
