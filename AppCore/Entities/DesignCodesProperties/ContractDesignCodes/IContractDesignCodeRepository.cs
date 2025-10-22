using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.DesignCodesProperties.ContractDesignCodes
{
    public interface IContractDesignCodeRepository
    {
        public Task Add(ContractDesignCode contractDesignCode);
        public Task AddParameters(List<ContractDesignCodeParameterRel> parameters);
        public Task Update(ContractDesignCode contractDesignCode);
        public Task UpdateCounter(Guid id);
        public Task Delete(Guid id);
        public Task DeleteParameters(Guid id);
        public Task<ContractDesignCode> Get(Guid id);
        public Task<ContractDesignCode> GetByParameter(Guid contractTypeId,Guid organizationUnitId,Guid roleOfOrganizationId);
        public Task<List<ContractDesignCode>> GetAll();
        public Task<List<ContractDesignCodeParameterRel>> GetAllParameters(Guid id);
        public Task<List<ContractDesignCodeParameterRel>> GetAllParameters();
    }
}
