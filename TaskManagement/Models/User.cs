using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Models
{
    public class User : BaseModel
    {
        [Key]
        public int UserID { get; set; }
        //public string? EmployeeNo { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public int RoleID { get; set; }
        public Role? Role { get; set; }

        [Required]
        //public ICollection<Task> Tasks { get; set; }
        public ICollection<TaskAssignment> TaskAssignments { get; set; }
    }
}

