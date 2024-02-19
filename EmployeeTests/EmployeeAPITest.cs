using EmployeeManagement.Controllers;
using EmployeeManagement.Data;
using EmployeeManagement.Data.Repository;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EmployeeTests
{
    [TestClass]
    public class EmployeeAPITest
    {
        private EmployeeController _employeeController;
        private EmployeeRepository _employeeRepository;
        private EmployeeDBContext _dbContext;
        private IWebHostEnvironment _environment;
        private ILogger<EmployeeController> _logger;
        private UserManager<ApplicationUser> _userManager;
        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<EmployeeDBContext>()
        .UseInMemoryDatabase(databaseName: "TestEmployeeDatabase")
        .Options;
            _dbContext = new EmployeeDBContext(options);
            _dbContext.Employees.Add(new Employee
            {
                Id = 1,
                FirstName = "James",
                LastName = "Dowe",
                Email = "james.dowe@example.com",
                Gender = "Male",
                Address = "456 Main St",
                Department = "Marketing",
                Salary = 70000,
                IsActive = true
            });
            _dbContext.Employees.Add(new Employee
            {
                Id = 2,
                FirstName = "Jane",
                LastName = "Doe",
                Email = "jane.doe@example.com",
                Gender = "Female",
                Address = "456 Main St",
                Department = "Marketing",
                Salary = 70000,
                IsActive = true
            });
            _dbContext.Employees.Add(new Employee
            {
                Id = 3,
                FirstName = "Bob",
                LastName = "Smith",
                Email = "bob.smith@example.com",
                Gender = "Male",
                Address = "789 Main St",
                Department = "Sales",
                Salary = 80000,
                IsActive = true
            });
            _dbContext.SaveChanges();
            _employeeRepository = new EmployeeRepository(_dbContext);
            _employeeController = new EmployeeController(_employeeRepository, _environment, _dbContext, _logger, _userManager);

        }
        [TestMethod]
        public void TestGetEmployeeById()
        {
            var actionResult = _employeeController.GetEmployee(1).Result;
            Assert.IsNotNull(actionResult);
            var okResult = actionResult as OkObjectResult;
            Assert.IsNotNull(okResult);
            var employee = okResult.Value as Employee;
            Assert.IsNotNull(employee);
            Assert.AreEqual(1, employee.Id);
        }

        [TestMethod]
        public void TestGetEmployees()
        {
            var actionResult = _employeeController.GetEmployees().Result;
            Assert.IsNotNull(actionResult);
            var okResult = actionResult as OkObjectResult;
            Assert.IsNotNull(okResult);
            var employees = okResult.Value as List<Employee>;
            Assert.IsNotNull(employees);
            Assert.AreEqual(3, employees.Count);
        }

        [TestMethod]
        public void TestDeleteEmployee()
        {
            var actionResult = _employeeController.DeleteEmployee(1).Result;
            Assert.IsNotNull(actionResult);
            var okResult = actionResult as OkObjectResult;
            Assert.IsNotNull(okResult);
            var employee = okResult.Value as Employee;
            Assert.IsNotNull(employee);
            Assert.AreEqual(1, employee.Id);
        }


    }
}




//        [TestMethod]
//        public void TestDeleteEmployee()
//        {
//            var result = _employeeController.DeleteEmployee(1).Result;
//            Assert.IsNotNull(result);
//            Assert.AreEqual(1, result.Value.Id);
//        }

//        [TestMethod]
//        public void TestPutEmployee()
//        {
//            var updatedEmployee = new Employee { Id = 1, Name = "UpdatedName" };
//            var result = _employeeController.PutEmployee(1, updatedEmployee).Result;
//            Assert.IsNotNull(result);
//            Assert.AreEqual("UpdatedName", result.Value.Name);
//        }
//    }
//}