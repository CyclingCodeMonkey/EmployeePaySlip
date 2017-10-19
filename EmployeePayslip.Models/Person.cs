using System;
namespace EmployeePayslip.Models
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int AnnualSalary { get; set; }
        public double SuperRate { get; set; }
        public int Super { get; set; }
        public int GrossSalary { get; set; }
        public int IncomeTax { get; set; }
        public string PayPeriod { get; set; }
    }
}
