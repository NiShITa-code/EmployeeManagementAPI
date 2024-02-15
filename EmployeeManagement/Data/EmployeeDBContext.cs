using EmployeeManagement.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace EmployeeManagement.Data
{

    public class EmployeeDBContext : IdentityDbContext<ApplicationUser>
    {
        public EmployeeDBContext(DbContextOptions<EmployeeDBContext> options) : base(options)
        {

        }
        
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Qualification> Qualifications { get; set; }
        public DbSet<EmployeeDocument> EmployeeDocuments { get; set; }
    }
}
