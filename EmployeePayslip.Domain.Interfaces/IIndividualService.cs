using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeePayslip.Models;

namespace EmployeePayslip.Domain.Interfaces
{
    public interface IIndividualService
    {
        Task<IList<Employee>> LoadFromFileAndCalculatePayslipAsync(string filename);
        Task<IList<Employee>> LoadFromFileAsync(string filename);
        Task<IList<Employee>> CalcululateIndividualPayslipsAsync(IList<Employee> employees);
    }
}