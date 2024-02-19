using EmployeeManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagement.Data.Repository
{
    public interface IQualificationRepository
    {
            Task<IEnumerable<Qualification>> GetQualificationsAsync();
            Task<IEnumerable<Qualification>> GetQualificationsByEmployeeIdAsync(int employeeId);
            Task AddQualificationsAsync(IEnumerable<Qualification> qualifications);
            Task UpdateQualificationsAsync(IEnumerable<Qualification> qualifications);
            Task DeleteQualificationsByEmployeeIdAsync(int employeeId);
    }
    

}

