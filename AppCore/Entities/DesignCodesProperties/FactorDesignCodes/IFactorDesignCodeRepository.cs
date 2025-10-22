using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.DesignCodesProperties.FactorDesignCodes
{
    public interface IFactorDesignCodeRepository
    {
        public Task Add(FactorDesignCode contractDesignCode);
        public Task AddParameters(List<FactorDesignCodeParameterRel> parameters);
        public Task Update(FactorDesignCode factorDesignCode);
        public Task UpdateCounter(Guid id);
        public Task Delete(Guid id);
        public Task DeleteParameters(Guid id);
        public Task<FactorDesignCode> Get(Guid id);
        public Task<List<FactorDesignCode>> GetAll();
        public Task<List<FactorDesignCodeParameterRel>> GetAllParameters(Guid id);
        public Task<FactorDesignCode> GetByParameter(Guid factorTypeId, Guid organizationUnitId, Guid roleOfOrganizationId);
        public Task<List<FactorDesignCodeParameterRel>> GetAllParameters();
    }
}
