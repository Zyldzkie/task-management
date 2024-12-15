using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Models
{
    public class TaskHistory
    {
        [Key]
        public int HistoryID { get; set; }
        public int TaskID { get; set; }
        public Task Task { get; set; }
        public string ChangeType { get; set; }
        public string ChangeDescription { get; set; }
        public DateTime ChangedAt { get; set; }
        public string ChangedBy { get; set; }
    }
}
