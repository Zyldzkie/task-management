using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Models
{
    public class CodeTable : BaseModel
    {
        [Key]
        public int CodeTableID { get; set; }

        public string CodeTableType { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }
    }
}

