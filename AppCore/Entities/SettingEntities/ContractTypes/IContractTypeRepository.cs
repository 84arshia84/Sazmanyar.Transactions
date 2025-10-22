using AppCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.SettingEntities.ContractTypes
{
    public interface IContractTypeRepository
    {
        public Task<bool> Add(ContractType contractType);
        public Task<bool> Update(ContractType contractType);
        public Task<bool> UpdateOfficeOnline(Guid contractTypeId, Guid? filenameId);
        public Task<bool> Delete(Guid contractType);
        public Task<List<ContractType>> GetAll();
        public Task<ContractType> Get(Guid contractType);





    }
}
