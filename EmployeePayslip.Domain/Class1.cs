﻿using System;

namespace EmployeePayslip.Domain
{
    public class Class1
    {
    	const double months = 12;
    	private int _incomeTax = -1;
    	private int _previousAnnualSalary = 0;

    	public int CalculateGrossIncome(double annualSalary)
    	{
    		var grossIncome = annualSalary / months;
			return Math.Round(grossIncome, MidpointRounding.AwayFromZero);
    	}

    	public async Task<int> CalculateIncomeTaxAsync(int annualSalary, int financialYear = 2018)
    	{
    		if (_incomeTax >= 0 && annualSalary == _previousAnnualSalary)
    		{
    			return _incomeTax;
    		}
    		_incomeTax = 0;
    		_previousAnnualSalary = annualSalary;
    		// get the income tax rates for the current year
    		// if nothing is returned then throw error
    		// foreach(var taxBracket in taxBrackets)
    		// { 
    		// 	if (annualSalary > taxBracket.max)
    		//  {  _incomeTax += (taxBracket.Max - taxBracket.Min) * rate
    		//  }
    		//  else //if (annualSalary <=  taxBracket.max)
    		//  {  _incomeTax += (annualSalary - taxBracket.max) * rate} 
    		// }
    		// return _incomeTax;
    	}

    	public async Task<int> CalculateNetIncomeAsync(int annualSalary, int financialYear = 2018)
    	{
    		return grossIncome - (await CalculateIncomeTaxAsync(annualSalary, financialYear));
    	}

    	public int CalculateSuper(double annualSalary, double superRate)
    	{
    		var super = CalculateGrossIncome(annualSalary) * superRate;
    		return Math.Round(super, MidpointRounding.AwayFromZero);
    	}
    }
}
