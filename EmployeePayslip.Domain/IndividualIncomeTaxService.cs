using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeePayslip.DataAccess;
using EmployeePayslip.DataAccess.Interfaces;
using EmployeePayslip.Domain.Interfaces;
using EmployeePayslip.Models;

namespace EmployeePayslip.Domain
{
    public class IndividualIncomeTaxService : IIndividualIncomeTaxService
    {
        private const double Months = 12;
        private int _incomeTax = -1;
        private int _previousAnnualSalary = 0;

        private readonly ITaxRateDataAccess _taxRateDataAccess;

        public IndividualIncomeTaxService()
        {
            _taxRateDataAccess = new TaxRateDataAccess();   
        }

        public IndividualIncomeTaxService(ITaxRateDataAccess taxRateDataAccess)
        {
            _taxRateDataAccess = taxRateDataAccess;
        }

        public int CalculateGrossIncome(double annualSalary)
        {
            var grossIncome = annualSalary / Months;
            return (int) Math.Round(grossIncome, MidpointRounding.AwayFromZero);
        }

        public async Task<int> CalculateIncomeTaxAsync(int annualSalary, 
                                                       int financialYear = 2018)
        {
            if (_incomeTax >= 0 && annualSalary == _previousAnnualSalary)
            {
                return _incomeTax;
            }
            _incomeTax = 0;
            _previousAnnualSalary = annualSalary;
            
            var taxRates = await _taxRateDataAccess.GetIndividualIncomeTaxRatesAsync(financialYear);
            // if nothing is returned then throw error

            var incomeTax = CalculateIncomeTax(annualSalary, taxRates);
            _incomeTax = (int)Math.Round(incomeTax, MidpointRounding.AwayFromZero);
            return _incomeTax;
        }

        private static double CalculateIncomeTax(int annualSalary, IncomeTaxRates taxRates)
        {
            var incomeTax = 0.0D;
            foreach (var taxBracket in taxRates.TaxBrackets)
            {
                if (annualSalary > taxBracket.Max)
                {
                    incomeTax += taxBracket.Amount;
                }
                if (taxBracket.Min < annualSalary && annualSalary <=  taxBracket.Max)
                {
                    incomeTax += (annualSalary - taxBracket.Min) * taxBracket.Rate;
                }
            }
            return incomeTax;
        }

        public async Task<int> CalculateNetIncomeAsync(int annualSalary, 
                                                       int financialYear = 2018)
        {
            var incomeTax = await CalculateIncomeTaxAsync(annualSalary, financialYear);
            return annualSalary - incomeTax;
        }

        public int CalculateSuper(double annualSalary, double superRate)
        {
            var super = CalculateGrossIncome(annualSalary) * superRate;
            return (int) Math.Round(super, MidpointRounding.AwayFromZero);
        }
    }
}
