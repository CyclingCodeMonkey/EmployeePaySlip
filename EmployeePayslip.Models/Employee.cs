namespace EmployeePayslip.Models
{
    public class Employee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int AnnualSalary { get; set; }
        public double SuperRate { get; set; }
        public int Super { get; set; }
        public int GrossIncome { get; set; }
        public int IncomeTax { get; set; }
        public string PayPeriod { get; set; }
        public int NetIncome { get; set; }
        public string Name => $"{FirstName} {LastName}".Trim();
    }
}
