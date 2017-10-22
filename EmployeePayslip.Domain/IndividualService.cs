using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using EmployeePayslip.Domain.Interfaces;
using EmployeePayslip.Models;

namespace EmployeePayslip.Domain
{
    public class IndividualService
    {
        private readonly IIndividualIncomeTaxService _individualIncomeTaxService;

        public IndividualService()
        {
            _individualIncomeTaxService = new IndividualIncomeTaxService();
        }
        
        public IndividualService(IIndividualIncomeTaxService individualIncomeTaxService)
        {
            _individualIncomeTaxService = individualIncomeTaxService;
        }

        public async Task<IList<Employee>> LoadFromFileAndCalculatePayslipAsync(string filename)
        {
            var employees = await LoadFromFileAsync(filename);
            
            // validate the employees

            employees = await CalcululateIndividualPayslipsAsync(employees);
            return employees;
        }

        public async Task<IList<Employee>> LoadFromFileAsync(string filename)
        {
            // check that the file exists
            if (!File.Exists(filename))
                throw new ArgumentException("Employee Payslip file does not exist");

            // try to open the file
            var fileStream = new FileStream(filename, FileMode.Open);
            using (var reader = new StreamReader(fileStream))
            {
                var employees = new List<Employee>();
                while (!reader.EndOfStream)
                {
                    var employeeLine = await reader.ReadLineAsync();
                    if (string.IsNullOrWhiteSpace(employeeLine)) continue;

                    var employee = ConvertToEmployee(employeeLine);
                    if (employee != null)
                    {
                        employees.Add(employee);
                    }
                }
                return employees;
            }
        }

        public async Task<IList<Employee>> CalcululateIndividualPayslipsAsync(IList<Employee> employees)
        {
            foreach (var employee in employees)
            {
                var annualSalary = employee.AnnualSalary;
                employee.GrossIncome = _individualIncomeTaxService.CalculateMonthlyGrossIncome(annualSalary);
                employee.IncomeTax = await _individualIncomeTaxService.CalculateMonthlyIncomeTaxAsync(annualSalary);
                employee.Super = _individualIncomeTaxService.CalculateMonthlySuper(annualSalary, employee.SuperRate);
                employee.NetIncome = await _individualIncomeTaxService.CalculateMonthlyNetIncomeAsync(annualSalary);
            }
            
            return employees;
        }

        private Employee ConvertToEmployee(string employeeLine)
        {
            if (string.IsNullOrWhiteSpace(employeeLine))
                return null;
            
            var parts = employeeLine.Split(",");
            if (parts.Length < 5 || parts.Length == 0)
            {
                throw new ArgumentException("Parsing CSV return unexpected number of elements");
            }
            int.TryParse(parts[2], out var annualSalary);

            var employee = new Employee
            {
                FirstName = parts[0].Trim(),
                LastName = parts[1].Trim(),
                AnnualSalary = annualSalary,
                SuperRate = GetSuperRate(parts[3]),
                PayPeriod = parts[4].Trim()
            };

            return employee;
        }

        private double GetSuperRate(string rate)
        {
            if (string.IsNullOrWhiteSpace(rate)            )
                return 0d;
            rate = rate.Replace("%", "");
            double.TryParse(rate, out var superRate);

            return superRate / 100.0D;
        }

    }
}
