using ApplicationService.DtoModels.FactorDtos.FactorFinancialDetaile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.FactorInformation
{
    public interface IFactorFinancialDetaileService
    {
        public Task<bool> Add(FactorFinancialDetaileAddDto factorFinancialDetaile,Guid financialId, Guid factorId);
        public Task<FactorFinancialDetaileGetDto> Get(Guid factorId);
        public Task<bool> Update(FactorFinancialDetaileUpdateDto factorFinancialDetaile);
    }
}
