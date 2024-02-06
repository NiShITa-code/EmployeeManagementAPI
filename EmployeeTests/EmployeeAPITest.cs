using EmployeeManagement.Controllers;
using EmployeeManagement.Data;
using EmployeeManagement.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace EmployeeTests
{
    [TestClass]
    public class EmployeeAPITest
    {
        private EmployeeController _employeeController;
        private EmployeeRepository _employeeRepository;
        private EmployeeDBContext _dbContext;
        [TestInitialize]
        public void Setup()
        {
            _employeeRepository = new EmployeeRepository(_dbContext);
            _employeeController = new EmployeeController(_employeeRepository);
           
        }

        [TestMethod]
        public void TestGetEmployee()
        {
            
        }
    }
}