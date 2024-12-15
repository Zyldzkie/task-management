using Microsoft.AspNetCore.Mvc;
using TaskManagement.Data;
using TaskManagement.Models;

namespace TaskManagement.Controllers
{
    public class RegisterController : Controller
    {
        private readonly TaskManagementContext _context;

        public RegisterController(TaskManagementContext context)
        {
            _context = context;
        }

        // GET: /Register
        public IActionResult Index()
        {
            ViewBag.Roles = _context.Role.ToList(); 
            return View();
        }

        // POST: /Register/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RegisterModel registrationModel)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Username = registrationModel.EmployeeNo,
                    Email = registrationModel.Email,
                    Password = registrationModel.Password, 
                    RoleID = registrationModel.RoleID,
                    CreatedAt = registrationModel.CreatedAt,
                    UpdatedAt = registrationModel.UpdatedAt,
                    CreatedBy = registrationModel.CreatedBy,
                    UpdatedBy = registrationModel.UpdatedBy
                };

                _context.User.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Index", "/"); 
            }
            ViewBag.Roles = _context.Role.ToList();
            return View("Index", registrationModel);
        }
    }
}