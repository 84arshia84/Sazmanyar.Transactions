using AppCore.Entities.SettingEntities.BasisForStartingTheProjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.SettingEntities.BasisForStartingTheProjects
{
    public interface IBasisForStartingTheProjectRepository
    {
        #region crud
        public Task<Tuple<string, bool>> Add(BasisForStartingTheProject basisForStartingTheProject);
        public Task<BasisForStartingTheProject> Get(Guid id);
        public Task<List<BasisForStartingTheProject>> GetAll();
        public Task<Tuple<string, bool>> Update(BasisForStartingTheProject basisForStartingTheProject);
        public Task<Tuple<string, bool>> Delete(Guid id);
        #endregion
    }
}
