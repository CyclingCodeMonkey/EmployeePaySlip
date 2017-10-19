﻿using System;

namespace EmployeePayslip.Models
{
    public class IncomeTaxRates
    {
        public int FinancialYear { get; set; }
        public IList<TaxRate> TaxRates { get; set; }
    }


}