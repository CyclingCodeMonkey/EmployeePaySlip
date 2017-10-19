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
        const double months = 12;
        int _incomeTax = -1;
        int _previousAnnualSalary = 0;

        readonly ITaxRateDataAccess _taxRateDataAccess;

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
            var grossIncome = annualSalary / months;
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
            // get the income tax rates for the current year
            // if nothing is returned then throw error
            var taxRates = new IncomeTaxRates();
            double incomeTax = 0.0;
            foreach (var taxBracket in taxRates.TaxBrackets)
            {
                if (annualSalary > taxBracket.Max)
                {
                    incomeTax += taxBracket.Amount;
                }
                else //if (annualSalary <=  taxBracket.max)
                { 
                    incomeTax += (annualSalary - taxBracket.Max) * taxBracket.Rate; 
                }
            }
            _incomeTax = (int)Math.Round(incomeTax, MidpointRounding.AwayFromZero);
            return _incomeTax;
        }

        public async Task<int> CalculateNetIncomeAsync(int annualSalary, 
                                                       int financialYear = 2018)
        {
            return CalculateGrossIncome(annualSalary) 
                - (await CalculateIncomeTaxAsync(annualSalary, financialYear));
        }

        public int CalculateSuper(double annualSalary, double superRate)
        {
            var super = CalculateGrossIncome(annualSalary) * superRate;
            return (int) Math.Round(super, MidpointRounding.AwayFromZero);
        }
    }
}
