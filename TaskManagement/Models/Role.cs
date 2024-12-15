using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Models
{
    public class Role : BaseModel
    {
        [Key]
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        //public bool Active { get; set; }
        //public DateTime CreatedAt { get; set; }
        //public DateTime? UpdatedAt { get; set; }
        //public string CreatedBy { get; set; }
        //public string UpdatedBy { get; set; }
        

        public ICollection<User> Users { get; set; }
    }
}

