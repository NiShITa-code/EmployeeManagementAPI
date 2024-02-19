using EmployeeManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace EmployeeManagement.Data.Repository
{
    public interface IEmployeeDocumentRepository
    {
        Task<IEnumerable<EmployeeDocument>> GetDocumentsAsync();
        Task<IEnumerable<EmployeeDocument>> GetDocumentsByEmployeeIdAsync(int employeeId);
        Task AddDocumentsAsync(IEnumerable<EmployeeDocument> documents);
        Task UpdateDocumentsAsync(IEnumerable<EmployeeDocument> documents);
        Task DeleteDocumentsByEmployeeIdAsync(int employeeId);
        Task<IEnumerable<EmployeeDocument>> GetDocumentsByIdsAsync(List<int> ids);

        Task UpdateDocumentsAsync(List<EmployeeDocument> documents);
    }
}
