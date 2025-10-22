using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Entities.InvoiceAccessGroups.AccessGroupGroups;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroupContractTypes;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroupInvoiceTypes;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroupOrganizationUnits;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroupRoleOfOrganizations;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroupUsers;

namespace AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroups
{
    public interface IInvoiceAccessGroupRepository
    {
        public Task Add(InvoiceAccessGroup invoiceAccessGroup, List<InvoiceAccessGroupInvoiceType> invoiceAccessGroupInvoiceTypes, List<InvoiceAccessGroupContractType> invoiceAccessGroupContractTypes
            , List<InvoiceAccessGroupOrganizationUnit> invoiceAccessGroupOrganizationUnits, List<InvoiceAccessGroupRoleOfOrganization> invoiceAccessGroupRoleOfOrganizations
            , List<InvoiceAccessGroupGroup> invoiceAccessGroupGroups, List<InvoiceAccessGroupUser> invoiceAccessGroupUser, InvoiceAccessGroupPermissions.InvoiceAccessGroupPermissions invoiceAccessGroupPermissions
            ,InvoiceAccessGroupProperties.InvoiceAccessGroupProperties invoiceaAccessGroupProperties);
        public Task<(string message, bool isSuccess)> Delete(Guid Id);
        public Task<List<InvoiceAccessGroup>> GetAll();
        public Task<InvoiceAccessGroup> GetById(Guid Id);
        public Task<List<Guid>> GetInvoicesUserHaveAccess(List<Guid> contractTypeIds, List<Guid> orgUnitIds, List<Guid> roleOfOrgIds, List<Guid> invoiceTypeIds);
        public Task<List<Guid>> GetContractTypeUserHaveAccess(List<Guid> accessGroupId);
        public Task<List<Guid>> GetOrganizationUserHaveAccess(List<Guid> accessGroupId);
        public Task<List<Guid>> GetRoleOfOrganizationUserHaveAccess(List<Guid> accessGroupId);
        public Task<List<Guid>> GetInvoiceTypeserHaveAccess(List<Guid> accessGroupId);
        public Task<List<InvoiceAccessGroupPermissions.InvoiceAccessGroupPermissions>> GetUserPermissions(Guid id);
        public Task<List<InvoiceAccessGroupProperties.InvoiceAccessGroupProperties>> GetUserSaveAccess(Guid id);
        public Task<List<InvoiceAccessGroupProperties.InvoiceAccessGroupProperties>> GetUserViewAccess(Guid id);
        public Task<List<InvoiceAccessGroupProperties.InvoiceAccessGroupProperties>> GetUserEditAccess(Guid id);
        public Task<List<InvoiceAccessGroupProperties.InvoiceAccessGroupProperties>> GetUserDeleteAccess(Guid id);
    }


}
