using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaskManagement.Data;
using TaskManagement.Models;
using TaskManagement.Models;

namespace TaskManagement.Controllers
{
    public class UserController : Controller
    {
        private readonly TaskManagementContext _context;

        public UserController(TaskManagementContext context)
        {
            _context = context;
        }

        // GET: User
        public IActionResult Index()
        {
            
            var users = _context.User.ToList();
            return View(users);
        }

        // GET: User/Details/5
        public IActionResult Details(int id)
        {
            var user = _context.User.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        //// GET: User/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: User/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create([Bind("UserID,Username,Password,Email,RoleID,Active,CreatedAt,UpdatedAt,CreatedBy,UpdatedBy")] User user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        user.CreatedAt = DateTime.Now;
        //        _context.Add(user);
        //        _context.SaveChanges();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(user);
        //}

        public IActionResult Create()
        {
            ViewBag.RoleID = new SelectList(_context.Role, "RoleID", "RoleName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("UserID,Username,Password,Email,RoleID,Active,CreatedAt,UpdatedAt,CreatedBy,UpdatedBy")] User user)
        {
            if (ModelState.IsValid)
            {
                user.CreatedAt = DateTime.Now;
                _context.Add(user);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.RoleID = new SelectList(_context.Role, "RoleID", "RoleName", user.RoleID);
            return View(user);
        }




        //// GET: User/Edit/5
        //public IActionResult Edit(int id)
        //{
        //    var user = _context.Users.Find(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(user);
        //}

        //// POST: User/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit(int id, [Bind("UserID,Username,Password,Email,RoleID,Active,CreatedAt,UpdatedAt,CreatedBy,UpdatedBy")] User user)
        //{
        //    if (id != user.UserID)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            user.UpdatedAt = DateTime.Now;
        //            _context.Update(user);
        //            _context.SaveChanges();
        //        }
        //        catch (Exception)
        //        {
        //            if (!UserExists(user.UserID))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(user);
        //}

        public IActionResult Edit(int id)
        {
            var user = _context.User.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            ViewBag.RoleID = new SelectList(_context.Role, "RoleID", "RoleName", user.RoleID);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("UserID,Username,Password,Email,RoleID,Active,CreatedAt,UpdatedAt,CreatedBy,UpdatedBy")] User user)
        {
            if (id != user.UserID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    user.UpdatedAt = DateTime.Now;
                    _context.Update(user);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    if (!UserExists(user.UserID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.RoleID = new SelectList(_context.Role, "RoleID", "RoleName", user.RoleID);
            return View(user);
        }


        // GET: User/Delete/5
        public IActionResult Delete(int id)
        {
            var user = _context.User.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var user = _context.User.Find(id);
            _context.User.Remove(user);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.UserID == id);
        }

        // POST: User/Accept/5
        [HttpPost]
        public IActionResult Accept(int id)
        {
            var user = _context.User.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            user.IsValidated = true;

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
      
                ModelState.AddModelError("", "Unable to accept user. Please try again.");
                return View("Index", _context.User.ToList());
            }

            return RedirectToAction(nameof(Index)); 
        }
    }
}
