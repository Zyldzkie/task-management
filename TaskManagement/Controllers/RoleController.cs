using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Data;
using TaskManagement.Models;


namespace TaskManagement.Controllers
{
    public class RoleController : Controller
    {
        private readonly TaskManagementContext _context;

        public RoleController(TaskManagementContext context)
        {
            _context = context;
        }

        // GET: Role
        public IActionResult Index()
        {
            var roles = _context.Role.ToList();
            return View(roles);
        }

        // GET: Role/Details/5
        public IActionResult Details(int id)
        {
            var role = _context.Role.Find(id);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        // GET: Role/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Role/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("RoleID,RoleName,Active,CreatedAt,UpdatedAt,CreatedBy,UpdatedBy")] Role role)
        {
            if (ModelState.IsValid)
            {
                role.CreatedAt = DateTime.Now;
                _context.Add(role);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(role);
        }

        // GET: Role/Edit/5
        public IActionResult Edit(int id)
        {
            var role = _context.Role.Find(id);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        // POST: Role/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("RoleID,RoleName,Active,CreatedAt,UpdatedAt,CreatedBy,UpdatedBy")] Role role)
        {
            if (id != role.RoleID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    role.UpdatedAt = DateTime.Now;
                    _context.Update(role);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    if (!RoleExists(role.RoleID))
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
            return View(role);
        }

        // GET: Role/Delete/5
        public IActionResult Delete(int id)
        {
            var role = _context.Role.Find(id);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        // POST: Role/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var role = _context.Role.Find(id);
            _context.Role.Remove(role);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool RoleExists(int id)
        {
            return _context.Role.Any(e => e.RoleID == id);
        }
    }
}
