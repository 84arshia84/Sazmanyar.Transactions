using ApplicationService.DtoModels.SettingDtos;
using AppCore.Entities.SettingEntities.CorespondentReals;
using AppCore.Entities.SettingEntities.CreditSources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.Setting
{
    public interface ICreditSourceService
    {
        public Task<Tuple<string, bool>> Add(CreditSourceDto creditSource);
        public Task<List<CreditSourceDto>> GetAll();
        public Task<Tuple<string, bool>> Update(CreditSourceDto creditSource);
        public Task<Tuple<string, bool>> Delete(Guid creditSource);
    }
}
