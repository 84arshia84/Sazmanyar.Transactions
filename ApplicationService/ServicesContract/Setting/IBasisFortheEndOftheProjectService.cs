using ApplicationService.DtoModels.SettingDtos;
using AppCore.Entities.SettingEntities.BasisFortheEndOftheProjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.Setting
{
    public interface IBasisFortheEndOftheProjectService
    {
        public Task<Tuple<string, bool>> Add(BasisFortheEndOftheProjectDto basisFortheEndOftheProject);
        public Task<List<BasisFortheEndOftheProjectDto>> GetAll();
        public Task<Tuple<string, bool>> Update(BasisFortheEndOftheProjectDto basisFortheEndOftheProject);
        public Task<Tuple<string, bool>> Delete(Guid basisFortheEndOftheProject);
    }
}
