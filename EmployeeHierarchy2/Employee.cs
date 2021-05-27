using CsvHelper;
using EmployeeHierarchy2.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace EmployeeHierarchy2
{
    public class Employee
    {
        public Dictionary<string, List<string>> ManagersJuniorEmployees = new Dictionary<string, List<string>>();


        private List<SingleEmployee> employees;
        public List<SingleEmployee> Employees
        {
            get { return employees; }
            set { employees = value; }
        }



        public Employee(string path)
        {
            AddAllEmloyeeToEmployeeList(path);
          
            foreach (var employee in Employees)
            {
                AddEmployeeToGraph(employee.Id, employee.ManagerId);
            }
        }


        public void AddAllEmloyeeToEmployeeList(string path)
        {
            var streamReader = File.OpenText(path);
            var csvReader = new CsvReader(streamReader, CultureInfo.CurrentCulture);

            Employees = new List<SingleEmployee>();

            while (csvReader.Read())
            {
                SingleEmployee singleEmployee = new SingleEmployee()
                {
                    Id = csvReader.GetField(0),
                    ManagerId = csvReader.GetField(1),
                    SalaryString = csvReader.GetField(2)
                };

                Employees.Add(singleEmployee);
            }

        }


        public bool CheckIfAllSalariesAreIntegers()
        {
            bool IsSalaryInteger = true;

            foreach (var employee in Employees)
            {
                int salary = 0;
                IsSalaryInteger = int.TryParse(employee.SalaryString, out salary);

                if (IsSalaryInteger == false)
                {

                    //throw exception
                    Console.WriteLine("Not All Salaries are Integers");
                    break;
                }
            }

            return IsSalaryInteger;
        }

        public bool IsThereMoreThanOneCeo()
        {
            var countNoOfCeos = Employees.Where(p => p.ManagerId == null || p.ManagerId.Trim() == "").Count();

            if (countNoOfCeos > 1)
            {
                //throw exception
                Console.WriteLine("There is More than one CEO");
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AllManagersAreEmployees()
        {
            bool AreAllManagersEmployees = true;

            //take all managers except the ceo
            var allManagersId = Employees.Where(p => p.ManagerId != null || p.ManagerId.Trim() != "").Select(p => p.ManagerId).ToList();

            foreach (var manager in allManagersId)
            {
                if (!Employees.Any(p => p.Id == manager))
                {
                    AreAllManagersEmployees = false;
                    Console.WriteLine("Some managers are not employees");
                    break;
                }
            }

            return AreAllManagersEmployees;
        }

        public void AddEmployeeToGraph(string EmployeeId, string ManagerId)
        {
            AddEmployeeToGraph(ManagerId);
            AddEmployeeToGraph(EmployeeId);

            ManagersJuniorEmployees[ManagerId].Add(EmployeeId);

        }

        public void AddEmployeeToGraph(string EmployeeId)
        {
            if (!ManagersJuniorEmployees.ContainsKey(EmployeeId))
            {
                ManagersJuniorEmployees.Add(EmployeeId, new List<string>());
            }

        }

        public bool CheckForCircularReference()
        {
            return false;
        }

        public bool DoesOneEmployeeReportToMoreThanOneManager()
        {
            bool DoesOneEmployeeReportToMoreThanOneManager = false;

            foreach (var employee in Employees)
            {
                if (Employees.Where(p => p.Id == employee.Id).Count() > 1)
                {
                    var allEmployesWithSameId = Employees.Where(p => p.Id == employee.Id).ToList();

                    foreach (var sameId in allEmployesWithSameId)
                    {
                        if (sameId.ManagerId != employee.ManagerId)
                        {
                            DoesOneEmployeeReportToMoreThanOneManager = true;
                            Console.WriteLine("Some employees report to more than one manager");
                            break;
                        }
                    }
                }

                if (DoesOneEmployeeReportToMoreThanOneManager)
                {
                    break;
                }
            }

            return DoesOneEmployeeReportToMoreThanOneManager;
        }

        public long SalaryBudgetForManager(string EmployeeId)
        {
            var allJuniorEmployees = ManagersJuniorEmployees.Where(p => p.Key == EmployeeId).Select(p => p.Value).FirstOrDefault();

            var sumOfSalaries = Employees.Where(p => allJuniorEmployees.Contains(p.Id)).Sum(p => p.Salary);

            return sumOfSalaries;
        }

    }
}
