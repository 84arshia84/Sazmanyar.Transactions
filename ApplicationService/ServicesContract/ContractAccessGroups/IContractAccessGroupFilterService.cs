using AppCore.Entities.ContractsInformation.ContractAddendums;
using AppCore.Entities.ContractsInformation.Contratcs;
using AppCore.Entities.ContractsInformation.TransactionExecutionRequests;
using AppCore.Entities.SettingEntities.ContractTypes;
using AppCore.Entities.SettingEntities.Organizationalunits;
using AppCore.Entities.SettingEntities.RoleOfOrganizations;
using AppCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.ContractAccessGroups
{
    public interface IContractAccessGroupFilterService
    {
        public Task<List<TransactionExecutionRequest>> FilterTransActionExecutionRequest(List<TransactionExecutionRequest> contracts, List<Guid> FlowIds, Guid userId);
        public Task<List<Contract>> FilterContracts(List<Contract> contracts, List<Guid> FlowIds, Guid userId);
        public Task<List<ContractAddendum>> FilterContractAddendums(List<ContractAddendum> contractAddendams, List<Guid> FlowIds, Guid userId);
        public Task<List<ContractType>> FilterContractType(List<ContractType> contractTypes, Guid userId, SystemParts part, AccessGroupProperties property, int mode);
        public Task<List<Organizationalunit>> FilterOrganizationalunit(List<Organizationalunit> organizationalunit, Guid userId, SystemParts part, AccessGroupProperties property, int mode);
        public Task<List<RoleOfOrganization>> FilterRoleOfOrganization(List<RoleOfOrganization> roleOfOrganization, Guid userId, SystemParts part, AccessGroupProperties property, int mode);
        public Task<(List<Guid> accessGroupForContractType, List<Guid> accessGroupForOrganizationUnit, List<Guid> accessGroupForRoleOfOrganization)> GetAccessGroupIds(Guid userId, SystemParts part, AccessGroupProperties property, int mode);
    }
}
