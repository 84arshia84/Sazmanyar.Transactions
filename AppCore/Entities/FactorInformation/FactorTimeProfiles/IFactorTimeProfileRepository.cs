using AppCore.Entities.FactorInformation.FactorFinancialDetailes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.FactorInformation.FactorTimeProfiles
{
    public interface IFactorTimeProfileRepository
    {
        public Task<bool> Add(FactorTimeProfile factorTimeProfile);
        public Task<FactorTimeProfile> Get(Guid factorId);
        public Task<bool> Update(FactorTimeProfile factorTimeProfile);
    }
}
