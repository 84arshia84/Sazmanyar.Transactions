using ApplicationService.DtoModels.SettingDtos;
using AppCore.Entities.SettingEntities.BasisFortheEndOftheProjects;
using AppCore.Entities.SettingEntities.CorespondentLegals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.Setting
{
    public interface ICorespondentLegalService
    {
        public Task<Tuple<string, bool>> Add(CorespondentLegalDto corespondentLegal);
        public Task<List<CorespondentLegalDto>> GetAll();
        public Task<Tuple<string, bool>> Update(CorespondentLegalDto corespondentLegal);
        public Task<Tuple<string, bool>> Delete(Guid corespondentLegal);
    }
}
