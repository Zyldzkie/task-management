using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Models
{
    public class History
    {
        [Key]
        public int HistoryID { get; set; }
        public int TaskID { get; set; }
        public string Action { get; set; }
        public DateTime ActionDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public bool Active { get; set; }
    }
}
