

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeePayslip.DataAccess.Interfaces;
using EmployeePayslip.Models;

namespace EmployeePayslip.DataAccess
{
    public class TaxRateDataAccess : ITaxRateDataAccess
    {
        public async Task<IncomeTaxRates> GetIndividualIncomeTaxRatesAsync(int year, string country = "AU")
        {
            if (string.IsNullOrWhiteSpace(country) || country.ToUpper() != "AU")
            {
                throw new NotImplementedException("Income Tax Rates implemented for Australia only.");
            }


        }


        private IList<IncomeTaxRates> GetIncomeTaxRates()
        {
            // data sourced from https://www.ato.gov.au/Rates/Individual-income-tax-for-prior-years/

            var incomeTaxRatesList = new List<IncomeTaxRates>();
            var rates = new IncomeTaxRates
            {
                FinancialYear = 2013,
                TaxRates = new List<TaxRate>
                {
                    new TaxRate {Rate = 0,    TaxableIncomeStart = 0,     TaxableIncomeEnd = 18200},
                    new TaxRate {Rate = 0.19, TaxableIncomeStart = 18201, TaxableIncomeEnd = 37000},
                    new TaxRate {Rate = 0.325,TaxableIncomeStart = 37001, TaxableIncomeEnd = 87000},
                    new TaxRate {Rate = 0.37, TaxableIncomeStart = 87001, TaxableIncomeEnd = 180000},
                    new TaxRate {Rate = 0.45, TaxableIncomeStart = 180001,TaxableIncomeEnd = 999999999}
                }
            };
            incomeTaxRatesList.Add(rates);
            rates = new IncomeTaxRates
            {
                FinancialYear = 2012,
                TaxRates = new List<TaxRate>
                {
                    new TaxRate {Rate = 0,    TaxableIncomeStart = 0,     TaxableIncomeEnd = 6000},
                    new TaxRate {Rate = 0.15, TaxableIncomeStart = 6001,  TaxableIncomeEnd = 37000},
                    new TaxRate {Rate = 0.30, TaxableIncomeStart = 37001, TaxableIncomeEnd = 80000},
                    new TaxRate {Rate = 0.37, TaxableIncomeStart = 80001, TaxableIncomeEnd = 180000},
                    new TaxRate {Rate = 0.45, TaxableIncomeStart = 180001,TaxableIncomeEnd = 999999999}
                }
            };
            incomeTaxRatesList.Add(rates);

            rates = new IncomeTaxRates
            {
                FinancialYear = 2010,
                TaxRates = new List<TaxRate>
                {
                    new TaxRate {Rate = 0,    TaxableIncomeStart = 0,     TaxableIncomeEnd = 6000},
                    new TaxRate {Rate = 0.15, TaxableIncomeStart = 6001,  TaxableIncomeEnd = 35000},
                    new TaxRate {Rate = 0.30, TaxableIncomeStart = 35001, TaxableIncomeEnd = 80000},
                    new TaxRate {Rate = 0.38, TaxableIncomeStart = 80001, TaxableIncomeEnd = 180000},
                    new TaxRate {Rate = 0.45, TaxableIncomeStart = 180001,TaxableIncomeEnd = 999999999}
                }
            };
            incomeTaxRatesList.Add(rates);
            rates = new IncomeTaxRates
            {
                FinancialYear = 2009,
                TaxRates = new List<TaxRate>
                {
                    new TaxRate {Rate = 0,    TaxableIncomeStart = 0,     TaxableIncomeEnd = 6000},
                    new TaxRate {Rate = 0.15, TaxableIncomeStart = 6001,  TaxableIncomeEnd = 34000},
                    new TaxRate {Rate = 0.30, TaxableIncomeStart = 34001, TaxableIncomeEnd = 80000},
                    new TaxRate {Rate = 0.40, TaxableIncomeStart = 80001, TaxableIncomeEnd = 180000},
                    new TaxRate {Rate = 0.45, TaxableIncomeStart = 180001,TaxableIncomeEnd = 999999999}
                }
            };
            incomeTaxRatesList.Add(rates);
            rates = new IncomeTaxRates
            {
                FinancialYear = 2008,
                TaxRates = new List<TaxRate>
                {
                    new TaxRate {Rate = 0,    TaxableIncomeStart = 0,     TaxableIncomeEnd = 6000},
                    new TaxRate {Rate = 0.15, TaxableIncomeStart =  6001, TaxableIncomeEnd = 25000},
                    new TaxRate {Rate = 0.30, TaxableIncomeStart = 25001, TaxableIncomeEnd = 80000},
                    new TaxRate {Rate = 0.40, TaxableIncomeStart = 80001, TaxableIncomeEnd = 180000},
                    new TaxRate {Rate = 0.45, TaxableIncomeStart = 180001,TaxableIncomeEnd = 999999999}
                }
            };
            incomeTaxRatesList.Add(rates);

            return incomeTaxRatesList;
        }
    }
}
