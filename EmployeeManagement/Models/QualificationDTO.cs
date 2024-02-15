using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Models
{
    public class QualificationDTO
    {
        
        public int Id { get; set; }
        public string QualificationName { get; set; }
        public string Institution { get; set; }
        public int YearOfPassing { get; set; }
        public double Percentage { get; set; }

        public string Stream { get; set; }
        public int EmployeeId { get; set; }

    }

}
