using AEMS_Zundel.Data;
using AEMS_Zundel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace AEMS_Zundel.Infrastructure
{
    public class EmployeeAuthorizationRequirement : IAuthorizationRequirement
    {
        public bool AllowMembers { get; set; }
        public bool AllowAdmins { get; set; }
    }
    public class EmployeeAuthorizationHandler : AuthorizationHandler<EmployeeAuthorizationRequirement>
    {
        private UserManager<EmployeeManagmentUser> userManager;

        public EmployeeAuthorizationHandler(UserManager<EmployeeManagmentUser> userManager)
        {
            this.userManager = userManager;
        }



        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EmployeeAuthorizationRequirement requirement)
        {
            // determine if the resource is consumable for the given user
            Employee project = context.Resource as Employee;
            string user = userManager.GetUserId(context.User);

            StringComparison compare = StringComparison.OrdinalIgnoreCase;

            //bool isLead = project.LeadId.Equals(user, compare);

            bool isMember = false;

            //if (project.Memberships != null)
            //{
            //    isMember = project.Memberships.Exists(m => m.MemberId.Equals(user, compare));
            //}


            if (project != null &&
                user != null &&
                //(requirement.AllowMembers && (isLead || isMember)) ||
                (requirement.AllowAdmins && context.User.IsInRole("Admins"))
            )
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}
