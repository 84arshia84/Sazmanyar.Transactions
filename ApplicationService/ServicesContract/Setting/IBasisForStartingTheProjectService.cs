using ApplicationService.DtoModels.SettingDtos;
using AppCore.Entities.SettingEntities.BasisForStartingTheProjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.Setting
{
    public interface IBasisForStartingTheProjectService
    {
        public Task<Tuple<string, bool>> Add(BasisForStartingTheProjectDto basisForStartingTheProject);
        public Task<List<BasisForStartingTheProjectDto>> GetAll();
        public Task<Tuple<string, bool>> Update(BasisForStartingTheProjectDto basisForStartingTheProject);
        public Task<Tuple<string, bool>> Delete(Guid basisForStartingTheProject);
    }
}
