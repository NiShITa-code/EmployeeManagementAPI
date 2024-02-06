using Microsoft.AspNetCore.Identity;

namespace EmployeeManagement.Models
{
    public class ApplicationUser: IdentityUser
    {
        public bool isActive { get; set; }
        
        
    }
}
