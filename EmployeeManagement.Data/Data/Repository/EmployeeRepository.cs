using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Data.Repository
{
    public class EmployeeRepository :IEmployeeRepository
    {
        private readonly EmployeeDBContext _dbContext;

        public EmployeeRepository(EmployeeDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
            var query = await _dbContext.Employees
                .Include(e => e.Qualifications)
                .Include(e => e.EmployeeDocuments)
                .ToListAsync();
            return query;
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            Employee employee = await _dbContext.Employees
                .Include(e => e.Qualifications)
                .Include(e => e.EmployeeDocuments)
                .FirstOrDefaultAsync(e => e.Id == id);
            return employee;
            

        }

        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
            _dbContext.Employees.Add(employee);
            await _dbContext.SaveChangesAsync();

            return employee;
        }

        public async Task<Employee> GetEmployeeByEmailAsync(string email)
        {
            Employee employee = await _dbContext.Employees
                .Include(e => e.Qualifications)
                .Include(e => e.EmployeeDocuments)
                .FirstOrDefaultAsync(e => e.Email == email);
            return employee;
        }
        public async Task<Employee> DeleteEmployeeAsync(int id)
        {
            var employee = await GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return employee;
            }
            _dbContext.Employees.Remove(employee);
            await _dbContext.SaveChangesAsync();

            return employee;

        }

        public async Task<Employee> UpdateEmployeeAsync(int id, Employee employee)
        {
            var employeeQuery = await GetEmployeeByIdAsync(id);
            if (employeeQuery == null)
            {
                return employeeQuery;
            }

            _dbContext.Entry(employeeQuery).CurrentValues.SetValues(employee);
            await _dbContext.SaveChangesAsync();

            return employeeQuery;
        }


    }
}
