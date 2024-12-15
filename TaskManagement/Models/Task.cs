using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Models
{
    public class Task : BaseModel
    {
        [Key]
        public int TaskID { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }
        public int ProjectID { get; set; }
        public Project? Project { get; set; }
        public string? Status { get; set; }
        public string Priority { get; set; }
        //public string? Department { get; set; }
        public DateTime DueDate { get; set; }

        //public ICollection<TaskAssignment> TaskAssignments { get; set; }
        //public ICollection<TaskHistory> TaskHistories { get; set; }
    }
}
