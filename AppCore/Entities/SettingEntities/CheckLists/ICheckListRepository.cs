using AppCore.Entities.SettingEntities.ContractTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.SettingEntities.CheckLists
{
    public interface ICheckListRepository
    {
        public Task<bool> Add(CheckList checkList);
        public Task<bool> Update(CheckList checkList);
        public Task<bool> Delete(Guid checkList,Guid contractTypeGuid);
        public Task<List<CheckList>> GetAll(Guid contractType);
        public Task<CheckList> Get(Guid checkList);
    }
}
