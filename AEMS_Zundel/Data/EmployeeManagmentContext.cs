using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using AEMS_Zundel.Models;

namespace AEMS_Zundel.Data
{
    public class EmployeeManagmentContext : IdentityDbContext<EmployeeManagmentUser>
    {
        public EmployeeManagmentContext(DbContextOptions<EmployeeManagmentContext> options)
            : base(options)
        {
        }

        
           

        public DbSet<AEMS_Zundel.Models.Employee> Employee { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

    }
}