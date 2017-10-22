using System;
using System.Collections.Generic;
using System.IO;
using EmployeePayslip.Domain;
using EmployeePayslip.Models;
using Microsoft.Extensions.CommandLineUtils;

namespace EmployeePayslip.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var commandLineApplication =
                new CommandLineApplication(throwOnUnexpectedArg: false)
                {
                    Description =
                        "Employee Payslip Evalator processes employee annual salaries from an input file to generate their payslips",
                    FullName = "Employee Payslip Evalator"
                };
            var filenameOption = commandLineApplication.Option(
                "-f | --file <filename>", "the name of the comma separated file to " +
                                          "containing individual employee payslips " +
                                          "to evaluate", CommandOptionType.SingleValue);
            commandLineApplication.HelpOption("-? | -h | --help");
            commandLineApplication.OnExecute(() =>
            {
                if (filenameOption.HasValue())
                {
                    System.Console.WriteLine($"Welcome to {commandLineApplication.Description}");
                    var individualService = new IndividualService();
                    var employees = individualService.LoadFromFileAndCalculatePayslipAsync(filenameOption.Value()).Result;
                    
                    DisplayIndividualEmployeePayslips(employees);
                }
                return 0;
            });
            commandLineApplication.Execute(args);
        }

        private static void DisplayIndividualEmployeePayslips(IList<Employee> persons)
        {
            System.Console.WriteLine("Output (Name, Pay Perion, Gross Income, Income Tax, Net Income, Super)");
            foreach (var person in persons)
            {
                System.Console.WriteLine($"{person.Name}, {person.PayPeriod}, {person.GrossIncome}, {person.IncomeTax}, {person.NetIncome}, {person.Super}");
            }
        }
    }
}
