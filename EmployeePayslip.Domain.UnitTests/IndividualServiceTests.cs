using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeePayslip.Domain.Interfaces;
using EmployeePayslip.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace EmployeePayslip.Domain.UnitTests
{
    // TODO create and mock out the file.open see 
    //  https://stackoverflow.com/questions/11141314/mocking-a-using-with-a-filestream
    

    [TestClass]
    public class IndividualServiceTests
    {
        private IIndividualIncomeTaxService _individualIncomeTaxService;

        [TestInitialize]
        public void Setup()
        {
            _individualIncomeTaxService = NSubstitute.Substitute.For<IIndividualIncomeTaxService>();
        }

        [TestMethod]
        public async Task CalcululateIndividualPayslipsAsync_EmptyEmployeeList_ShouldReturnEmptyList_Test()
        {
            var employees = new List<Employee>();
            
            var target = new IndividualService(_individualIncomeTaxService);
            var actual = await target.CalcululateIndividualPayslipsAsync(employees);
            Assert.IsFalse(actual.Any());
        }

        [TestMethod]
        public async Task CalcululateIndividualPayslipsAsync_SingleEmployeeList_ShouldReturnEmptyList_Test()
        {
            var employees = new List<Employee>
            {
                new Employee
                {
                    AnnualSalary = 60000,
                    FirstName = "John",
                    LastName = "Smith",
                    SuperRate = 0.10D
                }
            };
            _individualIncomeTaxService.CalculateMonthlyGrossIncome(Arg.Any<double>()).Returns(5000);
            _individualIncomeTaxService.CalculateMonthlyIncomeTaxAsync(Arg.Any<int>()).Returns(500);
            _individualIncomeTaxService.CalculateMonthlySuper(Arg.Any<double>(), Arg.Any<double>()).Returns(500);

            var target = new IndividualService(_individualIncomeTaxService);
            var actual = await target.CalcululateIndividualPayslipsAsync(employees);
            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual(5000, actual[0].GrossIncome);
            Assert.AreEqual(500, actual[0].IncomeTax);
            Assert.AreEqual(500, actual[0].Super);
        }

        [TestMethod]
        public void CalcululateIndividualPayslipsAsync_InvalidAnnualSalary_ShouldThrowArgumentOutOfRangeException_Test()
        {
            var employees = new List<Employee>
            {
                new Employee
                {
                    AnnualSalary = -1,
                    FirstName = "John",
                    LastName = "Smith",
                    SuperRate = 0.10D
                }
            };
            var target = new IndividualService(_individualIncomeTaxService);
            Action act = () => target.CalcululateIndividualPayslipsAsync(employees).GetAwaiter().GetResult();
            act.ShouldThrow<ArgumentOutOfRangeException>().WithMessage("Specified argument was out of the range of valid values.\r\nParameter name: AnnualSalary");
        }

        [TestMethod]
        public void CalcululateIndividualPayslipsAsync_InvalidSuperRate_ShouldThrowArgumentOutOfRangeException_Test()
        {
            var employees = new List<Employee>
            {
                new Employee
                {
                    AnnualSalary = 60000,
                    FirstName = "John",
                    LastName = "Smith",
                    SuperRate = 1.00D
                }
            };
            var target = new IndividualService(_individualIncomeTaxService);
            Action act = () => target.CalcululateIndividualPayslipsAsync(employees).GetAwaiter().GetResult();
            act.ShouldThrow<ArgumentOutOfRangeException>().WithMessage("Specified argument was out of the range of valid values.\r\nParameter name: SuperRate");
        }

        [TestMethod]
        public void LoadFromFileAsync_InvalidFile_ShouldThrowArgumentException_Test()
        {
            var target = new IndividualService(_individualIncomeTaxService);
            Action act = () => target.LoadFromFileAsync("C:\\data\\employees.csv").GetAwaiter().GetResult();
            act.ShouldThrow<ArgumentException>().WithMessage("Employee Payslip file does not exist");
        }

        [TestMethod]
        public void LoadFromFileAndCalculatePayslipAsync_InvalidFile_ShouldThrowArgumentException_Test()
        {
            var target = new IndividualService(_individualIncomeTaxService);
            Action act = () => target.LoadFromFileAndCalculatePayslipAsync("C:\\data\\employees.csv").GetAwaiter().GetResult();
            act.ShouldThrow<ArgumentException>().WithMessage("Employee Payslip file does not exist");
        }
    }
}
