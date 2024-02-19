using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Data.Repository
{
    public class QualificationRepository : IQualificationRepository
    {
        private readonly EmployeeDBContext _context;

        public QualificationRepository(EmployeeDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Qualification>> GetQualificationsAsync()
        {
            return await _context.Qualifications.ToListAsync();
        }

        public async Task<IEnumerable<Qualification>> GetQualificationsByEmployeeIdAsync(int employeeId)
        {
            return await _context.Qualifications.Where(q => q.EmployeeId == employeeId).ToListAsync();
        }

        public async Task AddQualificationsAsync(IEnumerable<Qualification> qualifications)
        {
            _context.Qualifications.AddRange(qualifications);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateQualificationsAsync(IEnumerable<Qualification> qualifications)
        {
            _context.Qualifications.UpdateRange(qualifications);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteQualificationsByEmployeeIdAsync(int employeeId)
        {
            var qualifications = await _context.Qualifications.Where(q => q.EmployeeId == employeeId).ToListAsync();
            if (qualifications != null && qualifications.Any())
            {
                _context.Qualifications.RemoveRange(qualifications);
                await _context.SaveChangesAsync();
            }
        }
    }
}
