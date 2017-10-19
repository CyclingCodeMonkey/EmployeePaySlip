using System.Threading.Tasks;

namespace EmployeePayslip.Domain.Interfaces
{
    public interface IIndividualIncomeTaxService
    {
        int CalculateGrossIncome(double annualSalary);
        Task<int> CalculateIncomeTaxAsync(int annualSalary, int financialYear = 2018);
        Task<int> CalculateNetIncomeAsync(int annualSalary, int financialYear = 2018);
        int CalculateSuper(double annualSalary, double superRate);
    }
}
