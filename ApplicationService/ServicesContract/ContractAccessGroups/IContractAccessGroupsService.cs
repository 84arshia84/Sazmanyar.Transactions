using AppCore.Entities.ContractAccessGroups;
using AppCore.Enums;
using ApplicationService.DtoModels.ContractAccessGroupsDtos;
using ApplicationService.DtoModels.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.ContractAccessGroups
{
    public interface IContractAccessGroupsService
    {
        public Task<(string message, bool isSuccess)> AddAccessGroup(AllAccessGroupsDto contractAccessGroup);
        public Task<(string message, bool isSuccess)> AddAccessGroupGroups(List<Guid> contractAccessGroupGroups, Guid CoreId);
        public Task<(string message, bool isSuccess)> AddAccessGroupUsers(List<Guid> contractAccessGroups, Guid CoreId);
        public Task<(string message, bool isSuccess)> AddAccessGroupProperties(ContractAccessGroupPropertiesDto contractAccessGroupProperties, Guid CoreId);
        public Task<(string message, bool isSuccess)> AddAccessGroupRoleOfOrganizations(List<Guid> contractAccessGroupRoles, Guid CoreId);
        public Task<(string message, bool isSuccess)> AddAccessGroupContractTypes(List<Guid> contractAccessGroupContractTypes, Guid CoreId);
        public Task<(string message, bool isSuccess)> AddAccessGroupOrganizationUnits(List<Guid> contractAccessGroupOrganizations, Guid CoreId);
        public Task<(string message, bool isSuccess)> AddAccessGroupPermissions(ContractAccessGroupPermissionsDto contractAccessGroupPermissions, Guid CoreId);
        public Task<(string message, bool isSuccess)> AddAccessGroupSystemPart(List<ContractAccessGroupSystemPartDto> contractAccessGroupSystemPartDto, Guid CoreId);
        public Task<(string message, bool isSuccess)> Delete(Guid id);
        public Task<UserAccessOnEditAndDelete> UserAccessOnEditAndDelete(Guid userId, Guid partEntityId, int part);
        public Task<(string message,bool isSuccess)> Update(AllAccessGroupsDto contractAccessGroup);
        public Task<AllAccessGroupsDto> Get(Guid id);
        public Task<List<ContractAccessGroupDto>> GetContractAccessGroupsById(Guid userId);
        public Task<List<ContractAccessGroupDto>> GetAll();
    }
}
