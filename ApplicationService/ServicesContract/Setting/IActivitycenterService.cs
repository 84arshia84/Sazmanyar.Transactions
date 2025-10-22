using ApplicationService.DtoModels.SettingDtos;
using AppCore.Entities.SettingEntities.Activitycenters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.Setting
{
    public interface IActivitycenterService
    {
        public Task<Tuple<string, bool>> Add(ActivitycenterDto activitycenter);
        public Task<List<ActivitycenterDto>> GetAll();
        public Task<Tuple<string, bool>> Update(ActivitycenterDto activitycenter);
        public Task<Tuple<string, bool>> Delete(Guid activitycenter);
    }
}
