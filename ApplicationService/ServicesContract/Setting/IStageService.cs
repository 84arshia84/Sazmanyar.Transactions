using ApplicationService.DtoModels.SettingDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.Setting
{
    public interface IStageService
    {
        public Task<List<StageDto>> GetAll(Guid moduleId, string connectionString);
    }
}
