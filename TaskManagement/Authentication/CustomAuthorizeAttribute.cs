using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TaskManagement.Data;

namespace TaskManagement.Authentication
{
    public class CustomAuthorizeAttribute : TypeFilterAttribute
    {       
        public CustomAuthorizeAttribute(params RoleType[] roleTypes) : base(typeof(ClaimRequirementFilter))
        {
            var roles = new Dictionary<string, string>();
            foreach (var item in roleTypes)
            {
                roles.Add(item.ToString(), item.ToString());
            }

            Arguments = new object[] { roles };

            //var roles = new Dictionary<int, int>();
            //foreach (var item in roleTypes)
            //{
            //    roles.Add((int)item, (int)item);
            //}

            //Arguments = new object[] { roles };

            //Arguments = new object[] { new Claim(roleType.ToString(), resourceValue) };

        }
    }

    public class ClaimRequirementFilter : IAuthorizationFilter
    {
        //readonly Claim _claim;
        readonly Dictionary<string, string> _role;
        //readonly Dictionary<int, int> _role;
        private readonly TaskManagementContext _db;

        public ClaimRequirementFilter(Dictionary<string, string> role, TaskManagementContext db)
        {
            _role = role;
            _db = db;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //var employeeNo = context.HttpContext.Session["EmployeeNo"];
            var userName = context.HttpContext.Session.GetString("UserName");
            
            var hasClaim = false;           
            if (userName != null)
            {  
                var employeeRoleFromDb = _db.User.Where(x => x.Username == userName).FirstOrDefault();
                context.HttpContext.Session.SetString("UserName", employeeRoleFromDb.Username);
                context.HttpContext.Session.SetInt32("UserRoleId", employeeRoleFromDb.RoleID);

                if (employeeRoleFromDb != null)
                {
                    //var roleFromDb = _db.Role.Where(x => x.RoleID == employeeRoleFromDb.RoleID).FirstOrDefault();
                    //if (_role.TryGetValue(roleFromDb.RoleName, out var roleFound)) //Find the key pair value in dictionary.
                    //{
                    //    hasClaim = true;
                    //}
                    var roleFromDb = _db.Role.Where(x => x.RoleID == employeeRoleFromDb.RoleID).FirstOrDefault();
                    if (roleFromDb != null)
                    {
                        context.HttpContext.Session.SetString("RoleName", roleFromDb.RoleName);                        
                        hasClaim = true;
                    }
                }
            }

            if (!hasClaim)
            {
                if (userName != null)
                {
                    //context.Result = new ForbidResult();
                    context.Result = new ViewResult
                    {
                        ViewName = "~/Views/Shared/Forbidden.cshtml"
                    };
                }
                else{
                    context.Result = new ViewResult
                    {
                        ViewName = "~/Views/Login/Login.cshtml"
                    };
                }
            }
        }
    }
}
