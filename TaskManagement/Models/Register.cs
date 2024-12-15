using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Models
{
    public class RegisterModel
    {
        [Required]
        [Display(Name = "Employee No")]
        public string EmployeeNo { get; set; }  

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public int RoleID { get; set; } 
     
        public DateTime CreatedAt { get; set; } = DateTime.Now; 
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; } = "System"; 
        public string UpdatedBy { get; set; } = "System"; 
    }
}