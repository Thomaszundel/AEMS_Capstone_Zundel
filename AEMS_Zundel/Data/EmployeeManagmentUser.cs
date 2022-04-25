using Microsoft.AspNetCore.Identity;
using AEMS_Zundel.Models;

namespace AEMS_Zundel.Data
{
    public class EmployeeManagmentUser : IdentityUser
    {
        public List<Employee> Employees { get; set; }
        
    }
}
