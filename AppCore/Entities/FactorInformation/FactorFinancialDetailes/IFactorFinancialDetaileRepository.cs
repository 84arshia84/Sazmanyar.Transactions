using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.FactorInformation.FactorFinancialDetailes
{
    public interface IFactorFinancialDetaileRepository
    {
        public Task<bool> Add(FactorFinancialDetaile factorFinancialDetaile);
        public Task<FactorFinancialDetaile> Get(Guid factorId);
        public Task<bool> Update(FactorFinancialDetaile factorFinancialDetaile);
    }
}
