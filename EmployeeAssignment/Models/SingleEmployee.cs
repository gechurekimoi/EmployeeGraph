using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeAssignment.Models
{
    public class SingleEmployee
    {
        private string id;

        public string Id
        {
            get { return id; }
            set { id = value; }
        }



        private string managerId;

        public string ManagerId
        {
            get { return managerId; }
            set { managerId = value; }
        }



        private long salary;

        public long Salary
        {
            get { return salary; }
            set { salary = value; }
        }


        private string salaryString;

        public string SalaryString
        {
            get { return salaryString; }
            set { salaryString = value; }
        }


    }
}
