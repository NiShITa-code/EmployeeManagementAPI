using System.Reflection.Metadata;

namespace EmployeeManagement.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
        public string Gender { get; set; }  
        public string Address { get; set; }

        public string Department { get; set; }

        public decimal Salary { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<Qualification> Qualifications { get; set; } = new List<Qualification>();
        public ICollection<EmployeeDocument> EmployeeDocuments { get; set; } = new List<EmployeeDocument>();

    }
}
