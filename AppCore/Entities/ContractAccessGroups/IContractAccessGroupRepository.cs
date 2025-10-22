using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.ContractAccessGroups
{
    public interface IContractAccessGroupRepository
    {
        public Task AddAccessGroup(ContractAccessGroup contractAccessGroup);
        public Task AddAccessGroupGroups(List<ContractAccessGroupGroups> contractAccessGroupGroups);
        public Task AddAccessGroupUsers(List<ContractAccessGroupUsers> contractAccessGroups);
        public Task AddAccessGroupProperties(ContractAccessGroupProperties contractAccessGroupProperties);
        public Task AddAccessGroupRoleOfOrganizations(List<ContractAccessGroupRoleOfOrganizations> contractAccessGroupRoles);
        public Task AddAccessGroupContractTypes(List<ContractAccessGroupContractType> contractAccessGroupContractTypes);
        public Task AddAccessGroupOrganizationUnits(List<ContractAccessGroupOrganizationUnits> contractAccessGroupOrganizations);
        public Task AddAccessGroupPermissions(ContractAccessGroupPermissions contractAccessGroupPermissions);
        public Task AddAccessGroupSystemPart(List<ContractAccessGroupSystemParts> contractAccessGroupSystemParts);
        public Task<bool> DeleteCore(Guid id);
        public Task DeleteGroups(Guid id);
        public Task DeleteContractTypes(Guid guid);
        public Task DeleteOrganizationUnits(Guid id);
        public Task DeleteUsers(Guid id);
        public Task DeleteSystemParts(Guid id);
        public Task DeleteProperties(Guid id);
        public Task DeleteRoles(Guid id); 
        public Task DeletePermmisions (Guid id);
        public Task<bool> Update(ContractAccessGroup contractAccessGroup);
        public Task<ContractAccessGroup> Get(Guid id);
        public Task<List<ContractAccessGroup>> GetAll();
        public Task<(List<ContractAccessGroupPermissions> permissions, List<ContractAccessGroupSystemParts> systemParts, List<ContractAccessGroupProperties> properties)> GetUserPermissionsAndSaveAccess(Guid id);
        public Task<(List<ContractAccessGroupSystemParts> systemParts, List<ContractAccessGroupProperties> properties)> GetUserSaveAccess(Guid id);
        public Task<(List<ContractAccessGroupSystemParts> systemParts, List<ContractAccessGroupProperties> properties)> GetUserViewAccess(Guid id);
        public Task<(List<ContractAccessGroupSystemParts> systemParts, List<ContractAccessGroupProperties> properties)> GetUserEditAccess(Guid id);
        public Task<(List<ContractAccessGroupSystemParts> systemParts, List<ContractAccessGroupProperties> properties)> GetUserDeleteAccess(Guid id);
        public Task<List<Guid>> GetContractsUserHaveAccess(List<Guid> contractTypeIds, List<Guid> orgUnitIds, List<Guid> roleOfOrgIds);
        public Task<List<Guid>> GetContractAddendumsUserHaveAccess(List<Guid> contractTypeIds, List<Guid> orgUnitIds, List<Guid> roleOfOrgIds);
        public Task<List<Guid>> GetTransactionsUserHaveAccess(List<Guid> contractTypeIds, List<Guid> orgUnitIds);
        public Task<List<Guid>> GetContractTypeUserHaveAccess(List<Guid> accessGroupId);
        public Task<List<Guid>> GetOrganizationUserHaveAccess(List<Guid> accessGroupId);
        public Task<List<ContractAccessGroup>> GetAccessGroupsByUserId(Guid userId);
        public Task<List<Guid>> GetRoleOfOrganizationUserHaveAccess(List<Guid> accessGroupId);

    }
}
