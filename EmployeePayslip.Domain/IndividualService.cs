using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EmployeePayslip.Domain.Interfaces;
using EmployeePayslip.Models;

namespace EmployeePayslip.Domain
{
    public class IndividualService
    {
        private readonly IIndividualIncomeTaxService _individualIncomeTaxService;

        public IndividualService()
        {
            _individualIncomeTaxService = new IndividualIncomeTaxService();
        }
        
        public IndividualService(IIndividualIncomeTaxService individualIncomeTaxService)
        {
            _individualIncomeTaxService = individualIncomeTaxService;
        }

        public async Task<IList<Person>> CalcululateIndividualPayslipsAsync(IList<Person> persons)
        {
            await Task.Delay(1);
            return null;
        }
    }
}
