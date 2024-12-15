//using Microsoft.AspNetCore.Mvc;
//using TaskManagement.Data;
//using TaskManagement.Models;
//using Microsoft.EntityFrameworkCore;

//namespace TaskManagement.Controllers
//{
//    public class AccountController : Controller
//    {
//        private readonly TaskManagementContext _context;

//        public AccountController(TaskManagementContext context)
//        {
//            _context = context;
//        }

//        // GET: /Account/Register
//        public IActionResult Register()
//        {
//            ViewBag.Roles = _context.Role.ToList();
//            return View();
//        }

//        // POST: /Account/Register
//        [HttpPost]
//        public IActionResult Register(User user)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.User.Add(user);
//                _context.SaveChanges();
//                return RedirectToAction("Login", "Account");
//            }
//            ViewBag.Roles = _context.Role.ToList();
//            return View(user);
//        }
//    }
//}
