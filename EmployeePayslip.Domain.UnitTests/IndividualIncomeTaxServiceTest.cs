using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeePayslip.DataAccess.Interfaces;
using EmployeePayslip.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace EmployeePayslip.Domain.UnitTests
{
	[TestClass]
    public class IndividualIncomeTaxServiceTest
    {
        private ITaxRateDataAccess _taxRateDataAccess;

        [TestInitialize]
        public void Setup()
        {
            _taxRateDataAccess = Substitute.For<ITaxRateDataAccess>();
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
            _taxRateDataAccess.GetIndividualIncomeTaxRatesAsync(2018).ReturnsForAnyArgs(rates);
        }
        
        [TestMethod]
        public void IndividualIncomeTaxService_CalculateGrossIncome_ShouldReturnZero_Test()
        {
            var target = new IndividualIncomeTaxService(_taxRateDataAccess);
            var actual = target.CalculateGrossIncome(0);
            Assert.AreEqual(0,actual);
        }

        [TestMethod]
        public void IndividualIncomeTaxService_CalculateGrossIncome_ShouldReturn1001_Test()
        {
            var target = new IndividualIncomeTaxService(_taxRateDataAccess);
            var actual = target.CalculateGrossIncome(12007);
            Assert.AreEqual(1001, actual);
        }

        [TestMethod]
        public async Task IndividualIncomeTaxService_CalculateIncomeTaxAsync_ShouldReturn0Tax_Test()
        {
            var target = new IndividualIncomeTaxService(_taxRateDataAccess);
            var actual = await target.CalculateIncomeTaxAsync(18200);
            Assert.AreEqual(0, actual);
        }

        [TestMethod]
        public async Task IndividualIncomeTaxService_CalculateIncomeTaxAsync_ShouldReturn3897Tax_Test()
        {
            var target = new IndividualIncomeTaxService(_taxRateDataAccess);
            var actual = await target.CalculateIncomeTaxAsync(38000);
            Assert.AreEqual(3897, actual);
        }

        [TestMethod]
        public async Task IndividualIncomeTaxService_CalculateIncomeTaxAsync_ShouldReturn11210Tax_Test()
        {
            var target = new IndividualIncomeTaxService(_taxRateDataAccess);
            var actual = await target.CalculateIncomeTaxAsync(60500);
            Assert.AreEqual(11210, actual);
        }

        [TestMethod]
        public async Task IndividualIncomeTaxService_CalculateIncomeTaxAsync_SalaryOver180000_Test()
        {
            var target = new IndividualIncomeTaxService(_taxRateDataAccess);
            var actual = await target.CalculateIncomeTaxAsync(200000);
            Assert.AreEqual(63232, actual);
        }
    }
}
