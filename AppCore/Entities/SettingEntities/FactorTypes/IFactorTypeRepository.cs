using AppCore.Entities.SettingEntities.ContractTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.SettingEntities.FactorTypes
{
    public interface IFactorTypeRepository
    {
        public Task<bool> Add(FactorType factorType);
        public Task<bool> Update(FactorType factorType);
        public Task<bool> UpdateOfficeOnline(Guid factorTypeId, Guid filenameId);
        public Task<bool> Delete(Guid factorType);
        public Task<List<FactorType>> GetAll();
        public Task<FactorType> Get(Guid factorType);
    }
}
