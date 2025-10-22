using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.SettingEntities.Roles
{
    public interface IRoleRepository
    {
        public Task<List<Role>> GetAll();
    }
}
