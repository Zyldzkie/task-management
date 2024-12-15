using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Data;
using TaskManagement.Models;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Authentication;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TaskManagement.Controllers
{
    public class ProjectController : Controller
    {
        private readonly TaskManagementContext _context;

        public ProjectController(TaskManagementContext context)
        {
            _context = context;
        }

        // GET: Project
        public IActionResult Index(DateTime? startCreatedDate, DateTime? endCreatedDate, string statusFilter = "All")
        {
            var roleName = HttpContext.Session.GetString("RoleName");
            var userName = HttpContext.Session.GetString("UserName");

            var projects = _context.Project.ToList();

            if (roleName == "User")
            {
                projects = projects.Where(x => x.CreatedBy == userName).ToList();
            }
            else if (roleName == "Manager")
            {
                projects = projects.Where(x => x.Status == "For Approval" || x.Status == "Approved").ToList();
            }
            else if (roleName == "Staff")
            {
                // Only show "Approved" projects for staff and those that have ended (EndDate < current date)
                projects = projects.Where(x => x.Status == "Approved" && x.EndDate < DateTime.Now).ToList();
            }

            //projects.Where(t => (statusFilter == "All" || t.Status.Equals(statusFilter, StringComparison.OrdinalIgnoreCase)) &&
            //(statusFilter == "All" || t.Status.Equals(statusFilter, StringComparison.OrdinalIgnoreCase)));


            projects = projects.Where(t => (statusFilter == "All" || t.Status.Equals(statusFilter, StringComparison.OrdinalIgnoreCase))).ToList();

            if (startCreatedDate.HasValue && endCreatedDate.HasValue)
            {
                //var startCreatedDatTemp = startCreatedDate ?? DateTime.Now;
                //var endCreatedDateTemp = startCreatedDate ?? DateTime.Now;

                projects = projects.Where(p => p.CreatedAt >= startCreatedDate && p.CreatedAt <= endCreatedDate).ToList();
            }


            var project = new TaskManagement.Models.Project();

            var taskStatusOption = _context.CodeTable.Where(x => x.CodeTableType == "ProjectStatus").ToList();
            taskStatusOption.Insert(0, new CodeTable { Value = "", Name = "-- Select Status --" });
            ViewBag.StatusFilter = new SelectList(taskStatusOption, "Value", "Name", project.Status);

            //ViewBag.PriorityFilter = priorityFilter; 
            //ViewBag.StatusFilter = statusFilter;

            return View(projects);
        }

        //public IActionResult Index()
        //{
        //    var roleName = HttpContext.Session.GetString("RoleName");
        //    var userName = HttpContext.Session.GetString("UserName");

        //    var projects = _context.Project.ToList();

        //    if (roleName == "User")
        //    {
        //        projects = projects.Where(x => x.CreatedBy == userName).ToList();
        //    }
        //    else if (roleName == "Manager")
        //    {
        //        projects = projects.Where(x => x.Status == "For Approval" || x.Status == "Approved").ToList();
        //    }
        //    else if (roleName == "Staff")
        //    {
        //        // Only show "Approved" projects for staff and those that have ended (EndDate < current date)
        //        projects = projects.Where(x => x.Status == "Approved" && x.EndDate < DateTime.Now).ToList();
        //    }

        //    return View(projects);
        //}

        // GET: Project/Details/5
        public IActionResult Details(int id)
        {
            var project = _context.Project.Include(p => p.Tasks).SingleOrDefault(p => p.ProjectID == id);

            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        // GET: Project/Create
        [CustomAuthorizeAttribute(RoleType.User)]
        public IActionResult Create()
        {
            var project = new TaskManagement.Models.Project();
            var departmentOption = _context.CodeTable.Where(x => x.CodeTableType == "Department").ToList();
            departmentOption.Insert(0, new CodeTable { Value = "", Name = "-- Select Department --" });
            ViewBag.Department = new SelectList(departmentOption, "Value", "Name", project.Department);


            return View();
        }

        // POST: Project/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorizeAttribute(RoleType.User)]
        public IActionResult Create([Bind("ProjectID,ProjectName,Description,Department,StartDate,EndDate,Active,CreatedAt,UpdatedAt,CreatedBy,UpdatedBy")] Project project)
        {
            if (ModelState.IsValid)
            {
                project.CreatedAt = DateTime.Now;
                project.Status = "Draft";
                project.Active = true;
                _context.Add(project);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(project);
        }

        // GET: Project/Edit/5
        public IActionResult Edit(int id)
        {
            var project = _context.Project.Find(id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        // POST: Project/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorizeAttribute(RoleType.User)]
        public IActionResult Edit(int id, [Bind("ProjectID,ProjectName,Description,Department,StartDate,EndDate,Active,CreatedAt,UpdatedAt,CreatedBy,UpdatedBy")] Project project)
        {
            if (id != project.ProjectID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    project.UpdatedAt = DateTime.Now;
                    _context.Update(project);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    if (!ProjectExists(project.ProjectID))
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
            return View(project);
        }

        // GET: Project/Delete/5
        [CustomAuthorizeAttribute(RoleType.User)]
        public IActionResult Delete(int id)
        {
            var project = _context.Project.Find(id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        // POST: Project/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorizeAttribute(RoleType.User)]
        public IActionResult DeleteConfirmed(int id)
        {
            var project = _context.Project.Find(id);
            _context.Project.Remove(project);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // POST: Project/ForApproval/5
        [HttpPost]
        [CustomAuthorizeAttribute(RoleType.Manager)]
        public IActionResult ForApproval(int id)
        {
            var project = _context.Project.Find(id);
            var userName = HttpContext.Session.GetString("UserName");

            if (project != null)
            {
                project.Status = "For Approval";
                project.SubmittedBy = userName;
                project.UpdatedBy = userName;
                project.UpdatedAt = DateTime.Now;
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }


        // POST: Project/Approve/5
        [HttpPost]
        [CustomAuthorizeAttribute(RoleType.Manager)]
        public IActionResult Approve(int id)
        {
            var project = _context.Project.Find(id);
            var userName = HttpContext.Session.GetString("UserName");

            if (project != null)
            {
                project.Status = "Approved";
                project.ApprovedBy = userName;
                project.UpdatedBy = userName;
                project.UpdatedAt = DateTime.Now;
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }



        // POST: Project/Reject/5
        [HttpPost]
        [CustomAuthorizeAttribute(RoleType.Manager)]
        public IActionResult Reject(int id)
        {
            var project = _context.Project.Find(id);
            var userName = HttpContext.Session.GetString("UserName");

            if (project != null)
            {
                project.Status = "Rejected";
                project.RejectedBy = userName;
                project.UpdatedBy = userName;
                project.UpdatedAt = DateTime.Now;
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectExists(int id)
        {
            return _context.Project.Any(e => e.ProjectID == id);
        }

        [HttpPost("LoadProjectTable")]
        [CustomAuthorizeAttribute(RoleType.Admin, RoleType.User, RoleType.Manager, RoleType.Staff)]
        public async Task<IActionResult> LoadProjectTable()
        {
            var orderCriteria = "ProjectID";
            var orderAscendingDirection = true;

            var data = _context.Project
                .AsQueryable();

            var totalResultsCount = data.Count();

            var resultData = new { recordsFilter = totalResultsCount, recordTotal = totalResultsCount, data = data };

            var result = new ContentResult
            {
                Content = JsonConvert.SerializeObject(resultData),
                ContentType = "application/json",
            };

            return result;
        }
    }
}
