using System;
using System.Collections.Generic;
using EmployeePayslip.Models;
using Microsoft.Extensions.CommandLineUtils;

namespace EmployeePayslip.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Welcome to Employee Payslip Evlauator");

            CommandLineApplication commandLineApplication =
                new CommandLineApplication(throwOnUnexpectedArg: false);
            CommandOption filenameOption = commandLineApplication.Option(
                "-f | --file <filename>", "the name of the comma separated file to " +
                                          "containing individual employee payslips " +
                                          "to evaluate", CommandOptionType.SingleValue);
            commandLineApplication.HelpOption("-? | -h | --help");
            commandLineApplication.OnExecute(() =>
            {
                if (filenameOption.HasValue())
                {
                    System.Console.WriteLine("filename=" + filenameOption.Value());
                }
                return 0;
            });
            commandLineApplication.Execute(args);
            System.Console.ReadLine();
        }

        private static void DisplayIndividualEmployeePayslips(IList<Person> persons)
        {
            System.Console.WriteLine("Output (Name, Pay Perion, Gross Income, Income Tax, Net Income, SUper)");
            foreach (var person in persons)
            {
                System.Console.WriteLine($"{person.Name}, {person.PayPeriod}, {person.GrossIncome}, {person.IncomeTax}, {person.NetIncome}, {person.Super}");
            }
        }

        private static void ReadFile()
        {
            // check that the file exists
            // try to open the file
            // read the file parse and return some objects
        }
    }
}
