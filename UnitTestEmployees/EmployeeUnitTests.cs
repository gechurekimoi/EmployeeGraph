using EmployeeAssignment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestEmployees
{
    [TestClass]
    public class EmployeeUnitTests
    {
        public Employee hierarchy { get; set; }


        [TestInitialize]
        public void TestInitialize()
        {
            // hierarchy = new Hierarchy(@"C:\Users\gechu\source\repos\EmployeeHierarchy\UnitTestEmployees\test1.csv");
        }


        [TestMethod]
        public void SalariesOfEmployeesAreIntegers()
        {
            hierarchy = new Employee(@"C:\Users\gechu\source\repos\EmployeeHierarchy\EmployeeConsoleApplication\testSalariesAllIntegers.csv");

            Assert.IsTrue(hierarchy.CheckIfAllSalariesAreIntegers());


        }

        [TestMethod]
        public void SalariesOfEmployeesAreNOTIntegers()
        {
            hierarchy = new Employee(@"C:\Users\gechu\source\repos\EmployeeHierarchy\EmployeeConsoleApplication\testSalariesNotIntegers.csv");

            Assert.IsFalse(hierarchy.CheckIfAllSalariesAreIntegers());

        }

        [TestMethod]
        public void NoneOfTheEmployeesReportToMoreThanOneManager()
        {
            hierarchy = new Employee(@"C:\Users\gechu\source\repos\EmployeeHierarchy\EmployeeConsoleApplication\testNoEmployeeReportsToMoreThanOneManager.csv");

            Assert.IsFalse(hierarchy.DoesOneEmployeeReportToMoreThanOneManager());
        }

        [TestMethod]
        public void SomeOfTheEmployeesReportToMoreThanOneManager()
        {
            hierarchy = new Employee(@"C:\Users\gechu\source\repos\EmployeeHierarchy\EmployeeConsoleApplication\testSomeEmployeeReportsToMoreThanOneManager.csv");

            Assert.IsTrue(hierarchy.DoesOneEmployeeReportToMoreThanOneManager());
        }

        [TestMethod]
        public void OnlyOneCEOExists()
        {
            hierarchy = new Employee(@"C:\Users\gechu\source\repos\EmployeeHierarchy\EmployeeConsoleApplication\testSomeEmployeeReportsToMoreThanOneManager.csv");

            Assert.IsFalse(hierarchy.IsThereMoreThanOneCeo());
        }


        [TestMethod]
        public void MoreThanOneCEOExists()
        {
            hierarchy = new Employee(@"C:\Users\gechu\source\repos\EmployeeHierarchy\EmployeeConsoleApplication\testMoreThanOneCEO.csv");

            Assert.IsTrue(hierarchy.IsThereMoreThanOneCeo());
        }


        [TestMethod]
        public void ManagerIsAnEmployee()
        {
            hierarchy = new Employee(@"C:\Users\gechu\source\repos\EmployeeHierarchy\EmployeeConsoleApplication\testAllManagersAreEmployees.csv");

            Assert.IsTrue(hierarchy.AllManagersAreEmployees());
        }


        [TestMethod]
        public void SomeManagerAreNotEmployees()
        {
            hierarchy = new Employee(@"C:\Users\gechu\source\repos\EmployeeHierarchy\EmployeeConsoleApplication\testSomeManagersAreEmployees.csv");

            Assert.IsFalse(hierarchy.AllManagersAreEmployees());
        }


    }
}
