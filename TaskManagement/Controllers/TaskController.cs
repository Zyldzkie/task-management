using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TaskManagement.Data;
using TaskManagement.Models;
using Task = TaskManagement.Models.Task;

namespace TaskManagement.Controllers
{
    public class TaskController : Controller
    {
        private readonly TaskManagementContext _context;

        public TaskController(TaskManagementContext context)
        {
            _context = context;
        }

        // GET: Task
        public IActionResult Index()
        {
            var tasks = _context.Task.Include(x => x.Project).ToList();
            return View(tasks);
        }

        // GET: Task/Details/5
        public IActionResult Details(int id)
        {
            var task = _context.Task.Find(id);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        // GET: Task/Create
        public IActionResult Create()
        {
            var task = new TaskManagement.Models.Task();

            var taskStatusOption = _context.CodeTable.Where(x => x.CodeTableType == "TaskStatus").ToList();
            taskStatusOption.Insert(0, new CodeTable { Value = "", Name = "-- Select Status --" });
            ViewBag.Status = new SelectList(taskStatusOption, "Value", "Name", task.Status);

            var priorityOption = _context.CodeTable.Where(x => x.CodeTableType == "Priority").ToList();
            priorityOption.Insert(0, new CodeTable { Value = "", Name = "-- Select Status --" });
            ViewBag.Priority = new SelectList(priorityOption, "Value", "Name", task.Priority);

            var projectOption = _context.Project.Where(x => x.Status == "Draft" || x.Status == "Rejected").Select(x => new
            {
                x.ProjectID,
                ProjectName = String.Concat(x.ProjectID, " - ", x.ProjectName)
            })
            .ToList();

            projectOption.Insert(0, new { ProjectID = 0, ProjectName = "-- Select Project --" });
            ViewBag.Projects = new SelectList(projectOption, "ProjectID", "ProjectName", task.Status);
            return View();

            //var departmentOption = _context.CodeTable.Where(x => x.CodeTableType == "Department").ToList();
            //departmentOption.Insert(0, new CodeTable { Value = "", Name = "-- Select Department --" });
            //ViewBag.Department = new SelectList(departmentOption, "Value", "Name");


        }

        // POST: Task/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("TaskID,TaskName,Description,ProjectID,AssignedTo,Status,Priority,Department,DueDate,Active,CreatedAt,UpdatedAt,CreatedBy,UpdatedBy")] Task task)
        {
            if (ModelState.IsValid)
            {
                task.CreatedAt = DateTime.Now;
                task.Active = true;
                _context.Add(task);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(task);
        }

        // GET: Task/Edit/5
        public IActionResult Edit(int id)
        {
            var task = _context.Task.Find(id);
            if (task == null)
            {
                return NotFound();
            }
            var taskStatusOption = _context.CodeTable.Where(x => x.CodeTableType == "TaskStatus").ToList();
            taskStatusOption.Insert(0, new CodeTable { Value = "", Name = "-- Select Status --" });
            ViewBag.Status = new SelectList(taskStatusOption, "Value", "Name", task.Status);

            var priorityOption = _context.CodeTable.Where(x => x.CodeTableType == "Priority").ToList();
            priorityOption.Insert(0, new CodeTable { Value = "", Name = "-- Select Status --" });
            ViewBag.Priority = new SelectList(priorityOption, "Value", "Name", task.Priority);

            var projectOption = _context.Project.ToList();
            projectOption.Insert(0, new Project { ProjectID = 0, ProjectName = "-- Select Project --" });
            ViewBag.Projects = new SelectList(projectOption, "ProjectID", "ProjectName", task.Status);
            return View(task);

            //var departmentOption = _context.CodeTable.Where(x => x.CodeTableType == "Department").ToList();
            //departmentOption.Insert(0, new CodeTable { Value = "", Name = "-- Select Department --" });
            //ViewBag.Department = new SelectList(departmentOption, "Value", "Name", task.Department);
        }

        // POST: Task/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("TaskID,TaskName,Description,ProjectID,AssignedTo,Status,Priority,DueDate,Active,CreatedAt,UpdatedAt,CreatedBy,UpdatedBy")] Task task)
        {
            if (id != task.TaskID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    task.UpdatedAt = DateTime.Now;
                    _context.Update(task);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    if (!TaskExists(task.TaskID))
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
            return View(task);
        }

        // GET: Task/Delete/5
        public IActionResult Delete(int id)
        {
            var task = _context.Task.Find(id);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        // POST: Task/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var task = _context.Task.Find(id);
            _context.Task.Remove(task);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskExists(int id)
        {
            return _context.Task.Any(e => e.TaskID == id);
        }

        [HttpPost("LoadData")]
        public async Task<IActionResult> LoadData()
        {
            var orderCriteria = "TaskID";
            var orderAscendingDirection = true;

            var data = _context.Task.Include(t => t.Project).AsQueryable();

            var totalResultsCount = data.Count();


            var resultData = new { recordsFilter = totalResultsCount, recordTotal = totalResultsCount, data = data };

            var result = new ContentResult
            {
                Content = JsonConvert.SerializeObject(resultData),
                ContentType = "application/json",
            };

            return result;
        }

        public async Task<IActionResult> CreateOrEdit(int id = 0, int projectID = 0)
        {
            var taskModel = new TaskManagement.Models.Task();

            var taskStatusOption = _context.CodeTable.Where(x => x.CodeTableType == "TaskStatus").ToList();
            taskStatusOption.Insert(0, new CodeTable { Value = "", Name = "-- Select Status --" });
            ViewBag.Status = new SelectList(taskStatusOption, "Value", "Name", taskModel.Status);

            var priorityOption = _context.CodeTable.Where(x => x.CodeTableType == "Priority").ToList();
            priorityOption.Insert(0, new CodeTable { Value = "", Name = "-- Select Status --" });
            ViewBag.Priority = new SelectList(priorityOption, "Value", "Name", taskModel.Priority);

            var projectOption = _context.Project.Where(x => x.Status == "Draft" || x.Status == "Rejected").Select(x => new
            {
                x.ProjectID,
                ProjectName = String.Concat(x.ProjectID, " - ", x.ProjectName)
            })
            .ToList();

            projectOption.Insert(0, new { ProjectID = 0, ProjectName = "-- Select Project --" });
            ViewBag.Projects = new SelectList(projectOption, "ProjectID", "ProjectName", taskModel.Status);

            if (id == 0)
            {

                var task = new TaskManagement.Models.Task();
                task.ProjectID = projectID;
                return View(task);
            }
            else
            {
                var task = await _context.Task.FindAsync(id);
                if (task == null)
                    return NotFound();

                return View(task);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrEdit(Task task)
        {
            if (ModelState.IsValid)
            {
                var userName = HttpContext.Session.GetString("UserName");

                if (task.TaskID == 0)
                {
                    //task.CreatedBy = userName;
                    //task.CreatedAt = DateTime.Now;    
                    _context.Add(task);
                }
                else
                {
                    task.UpdatedBy = userName;
                    task.UpdatedAt = DateTime.Now;
                    _context.Update(task);
                }

                await _context.SaveChangesAsync();

            }

            return RedirectToAction("Details", "Project", new { id = task.ProjectID }); // Redirect to the project list
            //return View(task);
        }
    }
}
