using AppCore.Entities.SettingEntities.BasisFortheEndOftheProjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.SettingEntities.BasisFortheEndOftheProjects
{
    public interface IBasisFortheEndOftheProjectRepository
    {
        public Task<Tuple<string, bool>> Add(BasisFortheEndOftheProject basisFortheEndOftheProject);
        public Task<BasisFortheEndOftheProject> Get(Guid id);
        public Task<List<BasisFortheEndOftheProject>> GetAll();
        public Task<Tuple<string, bool>> Update(BasisFortheEndOftheProject basisFortheEndOftheProject);
        public Task<Tuple<string, bool>> Delete(Guid id);
    }
}
