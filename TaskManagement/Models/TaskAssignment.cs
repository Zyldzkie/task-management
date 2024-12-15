using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Models
{
    public class TaskAssignment : BaseModel
    {
        [Key]
        public int AssignmentID { get; set; }
        public int TaskID { get; set; }
        public Task? Task { get; set; }
        public int UserID { get; set; }
        public User? User { get; set; }
        public DateTime AssignedDate { get; set; }
        //public bool Active { get; set; }
        //public DateTime CreatedAt { get; set; }
        //public DateTime? UpdatedAt { get; set; }
        //public string CreatedBy { get; set; }
        //public string UpdatedBy { get; set; }
    }
}
