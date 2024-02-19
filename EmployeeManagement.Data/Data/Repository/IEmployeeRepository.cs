using EmployeeManagement.Models;

namespace EmployeeManagement.Data.Repository
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetEmployeesAsync();

        Task<Employee> GetEmployeeByIdAsync(int id);


        Task<Employee> GetEmployeeByEmailAsync(string email);

        Task<Employee> AddEmployeeAsync(Employee employee);

        Task<Employee> DeleteEmployeeAsync(int id);

        Task<Employee> UpdateEmployeeAsync(int id, Employee employee);





    }
}
