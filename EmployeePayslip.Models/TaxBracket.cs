using System;
namespace EmployeePayslip.Models
{
    public class TaxBracket
    {
        public int Min { get; set; }
        public int Max { get; set; }
        public double Rate { get; set; }
        public double Amount
        {
            get
            {
                return (Max - Min) * Rate;
            }
        }
    }
}
