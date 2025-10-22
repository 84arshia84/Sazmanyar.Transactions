using ApplicationService.DtoModels.SettingDtos;
using AppCore.Entities.SettingEntities.ReasonForTerminations;
using AppCore.Entities.SettingEntities.ReleaseConditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.Setting
{
    public interface IReleaseConditionService
    {
        public Task<Tuple<string, bool>> Add(ReleaseConditionDto releaseCondition);
        public Task<List<ReleaseConditionDto>> GetAll();
        public Task<Tuple<string, bool>> Update(ReleaseConditionDto releaseCondition);
        public Task<Tuple<string, bool>> Delete(Guid releaseCondition);
    }
}
