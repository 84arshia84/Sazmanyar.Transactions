using ApplicationService.DtoModels.SettingDtos;
using AppCore.Entities.SettingEntities.CorespondentLegals;
using AppCore.Entities.SettingEntities.CorespondentReals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.Setting
{
    public interface ICorespondentRealService
    {
        public Task<Tuple<string, bool>> Add(CorespondentRealDto corespondentReal);
        public Task<List<CorespondentRealDto>> GetAll();
        public Task<Tuple<string, bool>> Update(CorespondentRealDto corespondentReal);
        public Task<Tuple<string, bool>> Delete(Guid corespondentReal);
    }
}
