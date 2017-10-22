using System;
using System.Collections.Generic;
using System.Linq;
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
            await Task.Delay(15);       // to simulate database access
            var incomeTaxBackets = GetIncomeTaxBrackets();
            
            return incomeTaxBackets.FirstOrDefault();
        }

        private IList<IncomeTaxRates> GetIncomeTaxBrackets()
        {
            // data sourced from https://www.ato.gov.au/Rates/Individual-income-tax-for-prior-years/

            var incomeTaxRatesList = new List<IncomeTaxRates>();
            var rates = new IncomeTaxRates
            {
                FinancialYear = 2013,
                TaxBrackets = new List<TaxBracket>
                {
                    new TaxBracket {Rate = 0,    Min = 0,     Max = 18200},
                    new TaxBracket {Rate = 0.19, Min = 18200, Max = 37000},
                    new TaxBracket {Rate = 0.325,Min = 37000, Max = 80000},
                    new TaxBracket {Rate = 0.37, Min = 80000, Max = 180000},
                    new TaxBracket {Rate = 0.45, Min = 180000,Max = 999999999}
                }
            };
            incomeTaxRatesList.Add(rates);
            rates = new IncomeTaxRates
            {
                FinancialYear = 2012,
                TaxBrackets = new List<TaxBracket>
                {
                    new TaxBracket {Rate = 0,    Min = 0,     Max = 6000},
                    new TaxBracket {Rate = 0.15, Min = 6000,  Max = 37000},
                    new TaxBracket {Rate = 0.30, Min = 37000, Max = 80000},
                    new TaxBracket {Rate = 0.37, Min = 80000, Max = 180000},
                    new TaxBracket {Rate = 0.45, Min = 180000,Max = 999999999}
                }
            };
            incomeTaxRatesList.Add(rates);

            rates = new IncomeTaxRates
            {
                FinancialYear = 2010,
                TaxBrackets = new List<TaxBracket>
                {
                    new TaxBracket {Rate = 0,    Min = 0,     Max = 6000},
                    new TaxBracket {Rate = 0.15, Min = 6000,  Max = 35000},
                    new TaxBracket {Rate = 0.30, Min = 35000, Max = 80000},
                    new TaxBracket {Rate = 0.38, Min = 80000, Max = 180000},
                    new TaxBracket {Rate = 0.45, Min = 180000,Max = 999999999}
                }
            };
            incomeTaxRatesList.Add(rates);
            rates = new IncomeTaxRates
            {
                FinancialYear = 2009,
                TaxBrackets = new List<TaxBracket>
                {
                    new TaxBracket {Rate = 0,    Min = 0,     Max = 6000},
                    new TaxBracket {Rate = 0.15, Min = 6000,  Max = 34000},
                    new TaxBracket {Rate = 0.30, Min = 34000, Max = 80000},
                    new TaxBracket {Rate = 0.40, Min = 80000, Max = 180000},
                    new TaxBracket {Rate = 0.45, Min = 180000,Max = 999999999}
                }
            };
            incomeTaxRatesList.Add(rates);
            rates = new IncomeTaxRates
            {
                FinancialYear = 2008,
                TaxBrackets = new List<TaxBracket>
                {
                    new TaxBracket {Rate = 0,    Min = 0,     Max = 6000},
                    new TaxBracket {Rate = 0.15, Min =  6000, Max = 25000},
                    new TaxBracket {Rate = 0.30, Min = 25000, Max = 80000},
                    new TaxBracket {Rate = 0.40, Min = 80000, Max = 180000},
                    new TaxBracket {Rate = 0.45, Min = 180000,Max = 999999999}
                }
            };
            incomeTaxRatesList.Add(rates);

            return incomeTaxRatesList;
        }
    }
}
