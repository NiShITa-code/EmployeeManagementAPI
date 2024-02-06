using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace EmployeeTest
{
    [TestClass]
    public class EmployeeApiTests
    {
        private EmployeeController employeeController;
        [TestInitialize]
        public void Setup()
        {
            employeeController = new EmployeeController();

        }

        [TestMethod]
        public void TestInser()
        {

        }
    }
}
