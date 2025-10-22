using ApplicationService.DtoModels.SettingDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.Setting
{
    public interface IStagesRolesService
    {
        public Task<(string message, bool isSuccss)> Add(StagesRolesDto stageRoles);
        public Task<(string message, bool isSuccss)> Update(StagesRolesDto stageRoles);
        public Task<(string message, bool isSuccss)> Delete(Guid stageRolesId);
        public Task<List<StagesRolesDto>> GetAll();
        public Task<StagesRolesDto> Get(Guid stageRolesId);
    }
}
