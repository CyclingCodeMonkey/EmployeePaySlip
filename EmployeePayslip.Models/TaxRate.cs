using System;
namespace EmployeePayslip.Models
{
    public class TaxRate
    {
        public int TaxableIncomeStart { get; set; }
        public int TaxableIncomeEnd { get; set; }
        public double Rate { get; set; }
    }

}
