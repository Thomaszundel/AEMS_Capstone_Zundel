using AEMS_Zundel.Data;
using Microsoft.AspNetCore.Identity;
namespace AEMS_Zundel.Models
{
    public class SeedData
    {
        public static async Task CreateAdminAccount(IServiceProvider serviceProvider)
        {
            UserManager<EmployeeManagmentUser> userManager = serviceProvider.GetRequiredService<UserManager<EmployeeManagmentUser>>();
            RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            EmployeeManagmentContext context = serviceProvider.GetRequiredService<EmployeeManagmentContext>();

            string username = "admin@site.com";
            string email = "admin@site.com";
            string password = "123Secret!"; // must follow password guidelines
            string role = "Admins";

            if (await userManager.FindByNameAsync(username) == null)
            {
                if (await roleManager.FindByNameAsync(role) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }

                EmployeeManagmentUser user = new EmployeeManagmentUser
                {
                    UserName = username,
                    Email = email
                };

                IdentityResult result = await userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }


               

               

                
                await context.SaveChangesAsync();
            }
        }
    }
}
