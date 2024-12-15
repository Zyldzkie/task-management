using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Data;
using TaskManagement.Models;
using TaskManagement.Models;

namespace TaskManagement.Controllers
{
    public class TaskHistoryController : Controller
    {
        private readonly TaskManagementContext _context;

        public TaskHistoryController(TaskManagementContext context)
        {
            _context = context;
        }

        // GET: TaskHistory
        public IActionResult Index()
        {
            var taskHistories = _context.TaskHistory.ToList();
            return View(taskHistories);
        }

        // GET: TaskHistory/Details/5
        public IActionResult Details(int id)
        {
            var taskHistory = _context.TaskHistory.Find(id);
            if (taskHistory == null)
            {
                return NotFound();
            }
            return View(taskHistory);
        }



        //public IActionResult Create() { 
        //    ViewBag.TaskID = new SelectList(_context.Tasks, "TaskID", "TaskName"); 
        //    return View(); 
        //}

        //[HttpPost][ValidateAntiForgeryToken] 
        //public IActionResult Create([Bind("HistoryID,TaskID,ChangeType,ChangeDescription,ChangedAt,ChangedBy")] TaskHistory taskHistory) { 
        //    if (ModelState.IsValid) { taskHistory.ChangedAt = DateTime.Now; _context.Add(taskHistory); _context.SaveChanges(); 
        //        return RedirectToAction(nameof(Index)); 
        //    } 

        //    ViewBag.TaskID = new SelectList(_context.Tasks, "TaskID", "TaskName", taskHistory.TaskID); 
        //    return View(taskHistory); 
        //}


        public IActionResult Create()
        {
            ViewBag.TaskID = new SelectList(_context.Task, "TaskID", "TaskName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("HistoryID,TaskID,ChangeType,ChangeDescription,ChangedAt,ChangedBy")] TaskHistory taskHistory)
        { 
            if (ModelState.IsValid) { 
                taskHistory.ChangedAt = DateTime.Now; 
                _context.Add(taskHistory); _context.SaveChanges(); 
                return RedirectToAction(nameof(Index)); 
            } 
            
            ViewBag.TaskID = new SelectList(_context.Task, "TaskID", "TaskName", taskHistory.TaskID); return View(taskHistory); 
        }

        public IActionResult Edit(int id) { 
            var taskHistory = _context.TaskHistory.Find(id); 
            if (taskHistory == null) 
            { 
                return NotFound(); 
            } 
            
            ViewBag.TaskID = new SelectList(_context.Task, "TaskID", "TaskName", taskHistory.TaskID); 
            
            return View(taskHistory);
        }

        [HttpPost][ValidateAntiForgeryToken] 
        public IActionResult Edit(int id, [Bind("HistoryID,TaskID,ChangeType,ChangeDescription,ChangedAt,ChangedBy")] TaskHistory taskHistory) 
        { 
            if (id != taskHistory.HistoryID) { 
                return NotFound(); 
            } 
            
            if (ModelState.IsValid) { 
                try { 
                    taskHistory.ChangedAt = DateTime.Now; _context.Update(taskHistory); 
                    _context.SaveChanges(); 
                } 
                catch (Exception) { 
                    if (!TaskHistoryExists(taskHistory.HistoryID)) { 
                        return NotFound(); 
                    } 
                    else 
                    { 
                        throw; 
                    } 
                } 
                
                return RedirectToAction(nameof(Index)); 
            } 
            ViewBag.TaskID = new SelectList(_context.Task, "TaskID", "TaskName", taskHistory.TaskID); return View(taskHistory); 
        }


        //public IActionResult Edit(int id)
        //{
        //    var taskHistory = _context.TaskHistories.Find(id); if (taskHistory == null)
        //    {
        //        return NotFound();
        //    }

        //    ViewBag.TaskID = new SelectList(_context.Tasks, "TaskID", "TaskName", taskHistory.TaskID); return View(taskHistory);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit(int id, [Bind("HistoryID,TaskID,ChangeType,ChangeDescription,ChangedAt,ChangedBy")] TaskHistory taskHistory)
        //{
        //    if (id != taskHistory.HistoryID)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            taskHistory.ChangedAt = DateTime.Now; _context.Update(taskHistory);
        //            _context.SaveChanges();
        //        }
        //        catch (Exception)
        //        {
        //            if (!TaskHistoryExists(taskHistory.HistoryID))
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

        //    ViewBag.TaskID = new SelectList(_context.Tasks, "TaskID", "TaskName", taskHistory.TaskID);
        //    return View(taskHistory);
        //}

        //// GET: TaskHistory/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: TaskHistory/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create([Bind("HistoryID,TaskID,ChangeType,ChangeDescription,ChangedAt,ChangedBy")] TaskHistory taskHistory)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        taskHistory.ChangedAt = DateTime.Now;
        //        _context.Add(taskHistory);
        //        _context.SaveChanges();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(taskHistory);
        //}

        //// GET: TaskHistory/Edit/5
        //public IActionResult Edit(int id)
        //{
        //    var taskHistory = _context.TaskHistories.Find(id);
        //    if (taskHistory == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(taskHistory);
        //}

        //// POST: TaskHistory/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit(int id, [Bind("HistoryID,TaskID,ChangeType,ChangeDescription,ChangedAt,ChangedBy")] TaskHistory taskHistory)
        //{
        //    if (id != taskHistory.HistoryID)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(taskHistory);
        //            _context.SaveChanges();
        //        }
        //        catch (Exception)
        //        {
        //            if (!TaskHistoryExists(taskHistory.HistoryID))
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
        //    return View(taskHistory);
        //}

        // GET: TaskHistory/Delete/5
        public IActionResult Delete(int id)
        {
            var taskHistory = _context.TaskHistory.Find(id);
            if (taskHistory == null)
            {
                return NotFound();
            }
            return View(taskHistory);
        }

        // POST: TaskHistory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var taskHistory = _context.TaskHistory.Find(id);
            _context.TaskHistory.Remove(taskHistory);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskHistoryExists(int id)
        {
            return _context.TaskHistory.Any(e => e.HistoryID == id);
        }
    }
}
