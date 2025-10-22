using AppCore.Entities.ContractsInformation.ContractAddendums;
using AppCore.Entities.ContractsInformation.Contratcs;
using AppCore.Entities.ContractsInformation.TransactionExecutionRequests;
using AppCore.Entities.InvoiceInformations.InvoiceBaseInformations;
using AppCore.Entities.InvoicesInformations.InvoiceType;
using AppCore.Entities.SettingEntities.ContractTypes;
using AppCore.Entities.SettingEntities.Organizationalunits;
using AppCore.Entities.SettingEntities.RoleOfOrganizations;
using AppCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.InvoiceInformations
{
    public interface IInvoiceAccessGroupFilterService
    {
        public Task<List<InvoiceType>> FilterInvoiceType(List<InvoiceType> invoiceType, Guid userId, SystemParts part, AccessGroupProperties property, int mode);
        public Task<List<InvoiceBaseInformation>> FilterInvoice(List<InvoiceBaseInformation> invoice, List<Guid> FlowIds, Guid userId);
        public Task<(List<Guid> accessGroupForContractType, List<Guid> accessGroupForOrganizationUnit, List<Guid> accessGroupForRoleOfOrganization,List<Guid> invoiceType)> GetAccessGroupIds(Guid userId, SystemParts part, AccessGroupProperties property, int mode);
        public Task<List<Contract>> FilterContractsInInvoice(List<Contract> contracts, Guid userId);
    }
}
