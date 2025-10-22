using AppCore.Entities.DesignCodesProperties.FactorDesignCodes;
using AppCore.Entities.DesignCodesProperties.InvoiceDesignCodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.DesignCodesProperties.TransActionExecutionDesignCodes
{
    public interface ITransActionExecutionDesignCodeRepository
    {
        public Task Add(TransActionExecutionDesignCode contractDesignCode);
        public Task AddParameters(List<TransActionExecutionDesignCodeParameterRel> parameters);
        public Task Update(TransActionExecutionDesignCode transactionDesignCode);
        public Task UpdateCounter(Guid id);
        public Task Delete(Guid id);
        public Task DeleteParameters(Guid id);
        public Task<TransActionExecutionDesignCode> Get(Guid id);
        public Task<List<TransActionExecutionDesignCode>> GetAll();
        public Task<List<TransActionExecutionDesignCodeParameterRel>> GetAllParameters(Guid id);
        public Task<TransActionExecutionDesignCode> GetByParameter(Guid contractTypeId, Guid organizationUnitId);
        public Task<List<TransActionExecutionDesignCodeParameterRel>> GetAllParameters();
    }
}
