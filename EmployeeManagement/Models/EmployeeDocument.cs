using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Models
{
    public class EmployeeDocument
    {
        
            public int Id { get; set; }
            public string FileName { get; set; }
            public string FilePath { get; set; }
            public int EmployeeId { get; set; }
            public string Remarks { get; set; }

            [ForeignKey("EmployeeId")]
            public Employee Employee { get; set; }
        
    }
}
