using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Data;
using TaskManagement.Models;

namespace TaskManagement.Controllers
{
    public class TaskAssignmentController : Controller
    {
        private readonly TaskManagementContext _context;

        public TaskAssignmentController(TaskManagementContext context)
        {
            _context = context;
        }

        // GET: TaskAssignment
        public IActionResult Index()
        {
            var taskAssignments = _context.TaskAssignment.Include(t => t.Task).Include(t => t.User).ToList();
            return View(taskAssignments);
        }

        // GET: TaskAssignment/Details/5
        public IActionResult Details(int id)
        {
            var taskAssignment = _context.TaskAssignment.Find(id);
            if (taskAssignment == null)
            {
                return NotFound();
            }
            return View(taskAssignment);
        }

        public IActionResult Create()
        {
            ViewBag.TaskID = new SelectList(_context.Task, "TaskID", "TaskName");
            ViewBag.UserID = new SelectList(_context.User, "UserID", "Username");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("AssignmentID,TaskID,UserID,AssignedDate,Active,CreatedAt,UpdatedAt,CreatedBy,UpdatedBy")] TaskAssignment taskAssignment)
        {
            if (ModelState.IsValid)
            {
                taskAssignment.CreatedAt = DateTime.Now;
                _context.Add(taskAssignment);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.TaskID = new SelectList(_context.Task, "TaskID", "TaskName", taskAssignment.TaskID);
            ViewBag.UserID = new SelectList(_context.User, "UserID", "Username", taskAssignment.UserID);
            return View(taskAssignment);
        }

        public IActionResult Edit(int id)
        {
            var taskAssignment = _context.TaskAssignment.Find(id);
            if (taskAssignment == null)
            {
                return NotFound();
            }
            ViewBag.TaskID = new SelectList(_context.Task, "TaskID", "TaskName", taskAssignment.TaskID);
            ViewBag.UserID = new SelectList(_context.User, "UserID", "Username", taskAssignment.UserID);
            return View(taskAssignment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("AssignmentID,TaskID,UserID,AssignedDate,Active,CreatedAt,UpdatedAt,CreatedBy,UpdatedBy")] TaskAssignment taskAssignment)
        {
            if (id != taskAssignment.AssignmentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    taskAssignment.UpdatedAt = DateTime.Now;
                    _context.Update(taskAssignment);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    if (!TaskAssignmentExists(taskAssignment.AssignmentID))
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
            ViewBag.TaskID = new SelectList(_context.Task, "TaskID", "TaskName", taskAssignment.TaskID);
            ViewBag.UserID = new SelectList(_context.User, "UserID", "Username", taskAssignment.UserID);
            return View(taskAssignment);
        }


        //// GET: TaskAssignment/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: TaskAssignment/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create([Bind("AssignmentID,TaskID,UserID,AssignedDate,Active,CreatedAt,UpdatedAt,CreatedBy,UpdatedBy")] TaskAssignment taskAssignment)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        taskAssignment.CreatedAt = DateTime.Now;
        //        _context.Add(taskAssignment);
        //        _context.SaveChanges();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(taskAssignment);
        //}

        //// GET: TaskAssignment/Edit/5
        //public IActionResult Edit(int id)
        //{
        //    var taskAssignment = _context.TaskAssignments.Find(id);
        //    if (taskAssignment == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(taskAssignment);
        //}

        //// POST: TaskAssignment/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit(int id, [Bind("AssignmentID,TaskID,UserID,AssignedDate,Active,CreatedAt,UpdatedAt,CreatedBy,UpdatedBy")] TaskAssignment taskAssignment)
        //{
        //    if (id != taskAssignment.AssignmentID)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            taskAssignment.UpdatedAt = DateTime.Now;
        //            _context.Update(taskAssignment);
        //            _context.SaveChanges();
        //        }
        //        catch (Exception)
        //        {
        //            if (!TaskAssignmentExists(taskAssignment.AssignmentID))
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
        //    return View(taskAssignment);
        //}

        // GET: TaskAssignment/Delete/5
        public IActionResult Delete(int id)
        {
            var taskAssignment = _context.TaskAssignment.Find(id);
            if (taskAssignment == null)
            {
                return NotFound();
            }
            return View(taskAssignment);
        }

        // POST: TaskAssignment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var taskAssignment = _context.TaskAssignment.Find(id);
            _context.TaskAssignment.Remove(taskAssignment);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskAssignmentExists(int id)
        {
            return _context.TaskAssignment.Any(e => e.AssignmentID == id);
        }
    }
}
