using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.SettingEntities.RoleOfOrganizations
{
    public interface IRoleOfOrganizationRepository
    {
        public Task<bool> Add(RoleOfOrganization roleOfOrganization);
        public Task<bool> Update(RoleOfOrganization roleOfOrganization);
        public Task<bool> Delete(Guid roleOfOrganization);
        public Task<List<RoleOfOrganization>> GetAll();
        public Task<RoleOfOrganization> Get(Guid roleOfOrganization);
    }
}
