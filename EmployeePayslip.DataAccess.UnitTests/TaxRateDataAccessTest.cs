using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EmployeePayslip.DataAccess.UnitTests
{
    [TestClass]
    public class TaxRateDataAccessTest
    {
        [TestMethod]
        public void GetIndividualIncomeTaxRatesAsync_CountryIsInvalid_ShouldThrowNotImplementedException_Test()
        {
            var target = new TaxRateDataAccess();

            Action act = () => target.GetIndividualIncomeTaxRatesAsync(2000, "NZ").GetAwaiter().GetResult();
            act.ShouldThrow<NotImplementedException>().WithMessage("Income Tax Rates implemented for Australia only.");
        }

        [TestMethod]
        public async Task GetIndividualIncomeTaxRatesAsync_ShouldReturn2013FinancialYearTaxBrackets_Test()
        {
            var target = new TaxRateDataAccess();
            var actual = await target.GetIndividualIncomeTaxRatesAsync(2018);

            Assert.AreEqual(2013, actual.FinancialYear);
            Assert.AreEqual(18200, actual.TaxBrackets[0].Max);
            Assert.AreEqual(0.325, actual.TaxBrackets[2].Rate);
        }
    }
}
