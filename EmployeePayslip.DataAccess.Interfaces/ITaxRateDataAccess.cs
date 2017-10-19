using System.Threading.Tasks;
using EmployeePayslip.Models;

namespace EmployeePayslip.DataAccess.Interfaces
{
    public interface ITaxRateDataAccess
    {
        Task<IncomeTaxRates> GetIndividualIncomeTaxRatesAsync(int year, string country = "AU");
    }
}
