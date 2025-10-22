using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationService.DtoModels.ContractAccessGroupsDtos;
using ApplicationService.DtoModels.FactorAccessGroupDto;
using ApplicationService.DtoModels.UserDtos;

namespace ApplicationService.ServicesContract.FactorAccessGroups
{
    public interface IFactorAccessGroupsService
    {
        public Task<(string message, bool isSuccess)> AddAccessGroup(AllFactorAccessGroupsDto factorAccessGroup);
        public Task<(string message, bool isSuccess)> AddAccessGroupGroups(List<Guid> factorAccessGroupGroups, Guid CoreId);
        public Task<(string message, bool isSuccess)> AddAccessGroupUsers(List<Guid> factorAccessGroups, Guid CoreId);
        public Task<(string message, bool isSuccess)> AddAccessGroupProperties(FactorAccessGroupPropertiesDto factorAccessGroupProperties, Guid CoreId);
        public Task<(string message, bool isSuccess)> AddAccessGroupRoleOfOrganizations(List<Guid> factorAccessGroupRoles, Guid CoreId);
        public Task<(string message, bool isSuccess)> AddAccessGroupFactorTypes(List<Guid> factorAccessGroupFactorTypes, Guid CoreId);
        public Task<(string message, bool isSuccess)> AddAccessGroupOrganizationUnits(List<Guid> factorAccessGroupOrganizations, Guid CoreId);
        public Task<(string message, bool isSuccess)> AddAccessGroupPermissions(FactorAccessGroupPermissionsDto factorAccessGroupPermissions, Guid CoreId);
        
        public Task<(string message, bool isSuccess)> Delete(Guid id);
        public Task<UserAccessOnEditAndDelete> UserAccessOnEditAndDelete(Guid userId, Guid partEntityId);
        public Task<(string message, bool isSuccess)> Update(AllFactorAccessGroupsDto factorAccessGroup);
        public Task<AllFactorAccessGroupsDto> Get(Guid id);
        public Task<List<FactorAccessGroupDto>> GetAll();
    }
}
