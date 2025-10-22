using ApplicationService.DtoModels.SettingDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.Setting
{
    public interface ICheckListService
    {
        public Task<(string message, bool isSuccess)> Add(CheckListDto checkList);
        public Task<(string message, bool isSuccess)> Update(CheckListDto checkList);
        public Task<(string message, bool isSuccess)> Delete(Guid checkList, Guid contractTypeGuid);
        public Task<List<CheckListDto>> GetAll(Guid contractType);
        public Task<CheckListDto> Get(Guid checkList);
        public Task<List<LookUpTableInsideDto>> GetLookUpTreeByCheckListId(Guid checkListId);

    }
}
