using Microsoft.EntityFrameworkCore;
using TaskManagement.Models;
using Task = TaskManagement.Models.Task;

namespace TaskManagement.Data
{
    public class TaskManagementContext : DbContext
    {
        internal object feedback;

        public TaskManagementContext(DbContextOptions<TaskManagementContext> options)
            : base(options)
        {
        }

        public DbSet<Role> Role { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<TaskManagement.Models.Task> Task { get; set; }
        public DbSet<TaskAssignment> TaskAssignment { get; set; }
        public DbSet<TaskHistory> TaskHistory { get; set; }
        public DbSet<CodeTable> CodeTable { get; set; }
        //public DbSet<Feedback> Feedbacks { get; set; }
       
    }
}
