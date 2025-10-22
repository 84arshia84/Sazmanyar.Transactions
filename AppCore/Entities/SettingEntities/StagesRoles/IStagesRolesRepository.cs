using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.SettingEntities.StagesRoles
{
    public interface IStagesRolesRepository
    {
        public Task<(string message, bool isSuccss)> Add(StagesRoles stagesRoles);
        public Task<(string message, bool isSuccss)> Update(StagesRoles stagesRoles);
        public Task<(string message, bool isSuccss)> Delete(Guid stagesRolesId);
        public Task<List<StagesRoles>> GetAll();
        public Task<StagesRoles> Get(Guid stagesRolesId);
    }
}
