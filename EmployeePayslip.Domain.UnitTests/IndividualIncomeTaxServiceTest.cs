using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeePayslip.DataAccess.Interfaces;
using EmployeePayslip.Models;
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
        public void IndividualIncomeTaxService_CalculateMonthlyGrossIncome_ShouldReturnZero_Test()
        {
            var target = new IndividualIncomeTaxService(_taxRateDataAccess);
            var actual = target.CalculateMonthlyGrossIncome(0);
            Assert.AreEqual(0,actual);
        }

        [TestMethod]
        public void IndividualIncomeTaxService_CalculateMonthlyGrossIncome_ShouldReturn1001_Test()
        {
            var target = new IndividualIncomeTaxService(_taxRateDataAccess);
            var actual = target.CalculateMonthlyGrossIncome(12007);
            Assert.AreEqual(1001, actual);
        }

        [TestMethod]
        public void IndividualIncomeTaxService_CalculateMonthlySuper_ShouldReturnZero_Test()
        {
            var target = new IndividualIncomeTaxService(_taxRateDataAccess);
            var actual = target.CalculateMonthlySuper(18000, 0D);
            Assert.AreEqual(0, actual);
        }

        [TestMethod]
        public void IndividualIncomeTaxService_CalculateMonthlySuper_ShouldReturn1000_Test()
        {
            var target = new IndividualIncomeTaxService(_taxRateDataAccess);
            var actual = target.CalculateMonthlySuper(12000, 0.09D);
            Assert.AreEqual(90, actual);
        }

        [TestMethod]
        public async Task IndividualIncomeTaxService_CalculateMonthlyIncomeTaxAsync_ShouldReturn0Tax_Test()
        {
            var target = new IndividualIncomeTaxService(_taxRateDataAccess);
            var actual = await target.CalculateMonthlyIncomeTaxAsync(18200);
            Assert.AreEqual(0, actual);
        }

        [TestMethod]
        public async Task IndividualIncomeTaxService_CalculateMonthlyIncomeTaxAsync_IncomeTaxFor18236_Test()
        {
            var target = new IndividualIncomeTaxService(_taxRateDataAccess);
            var actual = await target.CalculateMonthlyIncomeTaxAsync(18236);
            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        public async Task IndividualIncomeTaxService_CalculateMonthlyIncomeTaxAsync_IncomeTaxFor38000_Test()
        {
            var target = new IndividualIncomeTaxService(_taxRateDataAccess);
            var actual = await target.CalculateMonthlyIncomeTaxAsync(38000);
            Assert.AreEqual(325, actual);
        }

        [TestMethod]
        public async Task IndividualIncomeTaxService_CalculateMonthlyIncomeTaxAsync_IncomeTaxFor60500_Test()
        {
            var target = new IndividualIncomeTaxService(_taxRateDataAccess);
            var actual = await target.CalculateMonthlyIncomeTaxAsync(60500);
            Assert.AreEqual(934, actual);
        }

        [TestMethod]
        public async Task IndividualIncomeTaxService_CalculateMonthlyIncomeTaxAsync_SalaryOver180000_Test()
        {
            var target = new IndividualIncomeTaxService(_taxRateDataAccess);
            var actual = await target.CalculateMonthlyIncomeTaxAsync(200000);
            Assert.AreEqual(5296, actual);
        }
        
        [TestMethod]
        public async Task IndividualIncomeTaxService_CalculateMonthlyIncomeTaxAsync_SalaryFor119999_Test()
        {
            var target = new IndividualIncomeTaxService(_taxRateDataAccess);
            var actual = await target.CalculateMonthlyIncomeTaxAsync(119999);
            Assert.AreEqual(2696, actual);
        }
        
        [TestMethod]
        public async Task IndividualIncomeTaxService_CalculateMonthlyIncomeTaxAsync_SalaryFor120000_Test()
        {
            var target = new IndividualIncomeTaxService(_taxRateDataAccess);
            var actual = await target.CalculateMonthlyIncomeTaxAsync(120000);
            Assert.AreEqual(2696, actual);
        }

        [TestMethod]
        public async Task IndividualIncomeTaxService_CalculateMonthlyIncomeTaxAsync_SalaryFor120002_Test()
        {
            var target = new IndividualIncomeTaxService(_taxRateDataAccess);
            var actual = await target.CalculateMonthlyIncomeTaxAsync(120002);
            Assert.AreEqual(2696, actual);
        }

        [TestMethod]
        public async Task IndividualIncomeTaxService_CalculateMonthlyNetIncomeAsync_SalaryOver12000_Test()
        {
            var target = new IndividualIncomeTaxService(_taxRateDataAccess);
            var grossIncome = target.CalculateMonthlyGrossIncome(12000);
            var netIncome = await target.CalculateMonthlyNetIncomeAsync(12000);
            Assert.AreEqual(1000, grossIncome);
            Assert.AreEqual(1000, netIncome);
        }

        [TestMethod]
        public async Task IndividualIncomeTaxService_CalculateMonthlyNetIncomeAsync_SalaryOver180000_Test()
        {
            var target = new IndividualIncomeTaxService(_taxRateDataAccess);
            var netIncome = await target.CalculateMonthlyNetIncomeAsync(200000);
            var incomeTax = await target.CalculateMonthlyIncomeTaxAsync(200000);
            Assert.AreEqual(11371, netIncome);
            Assert.AreEqual(5296, incomeTax);
        }
    }
}
