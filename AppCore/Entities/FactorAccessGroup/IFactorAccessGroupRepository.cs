using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Entities.ContractAccessGroups;
using AppCore.Enums;

namespace AppCore.Entities.FactorAccessGroup
{
    public interface IFactorAccessGroupRepository
    {
        public Task AddAccessGroup(FactorAccessGroup factorAccessGroup);
        public Task AddAccessGroupGroups(List<FactorAccessGroupGroups> factorAccessGroupGroups);
        public Task AddAccessGroupUsers(List<FactorAccessGroupUsers> factorAccessGroupUsers);
        public Task AddAccessGroupProperties(FactorAccessGroupProperties factorAccessGroupProperties);
        public Task AddAccessGroupRoleOfOrganizations(List<FactorAccessGroupRoleOfOrganizations> factorAccessGroupRoleOfOrganizations);
        public Task AddAccessGroupFactorTypes(List<FactorAccessGroupFactorType> factorAccessGroupFactorTypes);
        public Task AddAccessGroupOrganizationUnits(List<FactorAccessGroupOrganizationUnits> factorAccessGroupOrganizationUnits);
        public Task AddAccessGroupPermissions(FactorAccessGroupPermissions factorAccessGroupPermissions);
     
        public Task<bool> DeleteCore(Guid id);
        public Task DeleteGroups(Guid id);
        public Task DeleteFactorTypes(Guid guid);
        public Task DeleteOrganizationUnits(Guid id);
        public Task DeleteUsers(Guid id);
       
        public Task DeleteProperties(Guid id);
        public Task DeleteRoles(Guid id);
        public Task DeletePermmisions(Guid id);
        public Task<bool> Update(FactorAccessGroup factorAccessGroup);
        public Task<FactorAccessGroup> Get(Guid id);
        public Task<List<FactorAccessGroup>> GetAll();

        public Task<List<FactorAccessGroupPermissions>> GetUserPermissions(Guid id);
        public Task<List<FactorAccessGroupProperties>> GetUserSaveAccess(Guid id);
        public Task<List<FactorAccessGroupProperties>> GetUserViewAccess(Guid id);
        public Task<List<FactorAccessGroupProperties>> GetUserEditAccess(Guid id);
        public Task<List<FactorAccessGroupProperties>> GetUserDeleteAccess(Guid id);

        public Task<List<Guid>> GetFactorTypeUserHaveAccess(List<Guid> accessGroupIds);
        public Task<List<Guid>> GetOrganizationUserHaveAccess(List<Guid> accessGroupIds);
        public Task<List<Guid>> GetRoleOfOrganizationUserHaveAccess(List<Guid> accessGroupIds);
        public Task<(List<FactorAccessGroupPermissions> permissions, List<FactorAccessGroupProperties> properties)> GetUserPermissionsAndSaveAccess(Guid id);
        public Task<List<Guid>> GetFactorUserHaveAccess(List<Guid> factorTypeIds, List<Guid> organizationUnitsIds, List<Guid> roleOfOrganizationsIds);
    }
}
