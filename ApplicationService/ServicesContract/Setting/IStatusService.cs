using AppCore.Entities.SettingEntities.Statuses;
using ApplicationService.DtoModels.SettingDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.Setting
{
    public interface IStatusService
    {
        public Task<(string message, bool isSuccess)> Add(StatusDto status);
        public Task<(string message, bool isSuccess)> Update(StatusDto status);
        public Task<(string message, bool isSuccess)> Delete(Guid statusId);
        public Task<List<StatusDto>> GetAll();
        public Task<StatusDto> GetByContractId(Guid contractId);
        public Task<StatusDto> Get(Guid statusId);
    }
}
