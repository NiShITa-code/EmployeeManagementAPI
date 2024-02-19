using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Data.Repository
{
    public class EmployeeDocumentRepository : IEmployeeDocumentRepository
    {
        private readonly EmployeeDBContext _context;

        public EmployeeDocumentRepository(EmployeeDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EmployeeDocument>> GetDocumentsAsync()
        {
            return await _context.EmployeeDocuments.ToListAsync();
        }

        public async Task<IEnumerable<EmployeeDocument>> GetDocumentsByEmployeeIdAsync(int employeeId)
        {
            return await _context.EmployeeDocuments.Where(d => d.EmployeeId == employeeId).ToListAsync();
        }

        public async Task AddDocumentsAsync(IEnumerable<EmployeeDocument> documents)
        {
            if (documents == null)
            {
                throw new ArgumentNullException(nameof(documents));
            }
            try
            {
                _context.EmployeeDocuments.AddRange(documents);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("Error creating new document records");
            }
        }

        public async Task UpdateDocumentsAsync(IEnumerable<EmployeeDocument> documents)
        {
            if (documents == null)
            {
                throw new ArgumentNullException(nameof(documents));
            }
            try
            {
                _context.EmployeeDocuments.UpdateRange(documents);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("Error updating document records");
            }
        }

        public async Task DeleteDocumentsByEmployeeIdAsync(int employeeId)
        {
            var documents = await _context.EmployeeDocuments.Where(d => d.EmployeeId == employeeId).ToListAsync();
            if (documents != null && documents.Any())
            {
                _context.EmployeeDocuments.RemoveRange(documents);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<EmployeeDocument>> GetDocumentsByIdsAsync(List<int> ids)
        {
            return await _context.EmployeeDocuments.Where(d => ids.Contains(d.Id)).ToListAsync();
        }

        public async Task UpdateDocumentsAsync(List<EmployeeDocument> documents)
        {
            _context.EmployeeDocuments.UpdateRange(documents);
            await _context.SaveChangesAsync();
        }
    }
}