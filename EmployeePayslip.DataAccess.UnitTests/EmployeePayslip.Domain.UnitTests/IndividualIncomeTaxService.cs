using System;
using Xunit;

namespace EmployeePayslip.Domain.UnitTests
{
    public class IndividualIncomeTaxServiceTest
    {
        [Fact]
        public void CalculateGrossIncome_ForZero_ShouldReturnZeroTest()
        {
            var target = new IndividualIncomeTaxService();

        }
    }
}


