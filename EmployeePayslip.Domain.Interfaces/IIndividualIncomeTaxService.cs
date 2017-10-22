using System.Threading.Tasks;

namespace EmployeePayslip.Domain.Interfaces
{
    public interface IIndividualIncomeTaxService
    {
        int CalculateMonthlyGrossIncome(double annualSalary);
        Task<int> CalculateMonthlyIncomeTaxAsync(int annualSalary, int financialYear = 2018);
        Task<int> CalculateMonthlyNetIncomeAsync(int annualSalary, int financialYear = 2018);
        int CalculateMonthlySuper(double annualSalary, double superRate);
    }
}
