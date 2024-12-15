using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Models
{
    public class Project : BaseModel
    {
        [Key]
        public int ProjectID { get; set; }

        [DisplayName("Project Name")]
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Status { get; set; }
        public string Department { get; set; }
        public string? SubmittedBy { get; set; }
        public string? RejectedBy { get; set; }
        public string? ApprovedBy { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        //public string Ratings { get; set; }


        public ICollection<Task>? Tasks { get; set; }
    }
}
