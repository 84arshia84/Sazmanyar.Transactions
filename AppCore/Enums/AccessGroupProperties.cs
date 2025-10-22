using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Enums
{
    /// <summary>
    /// دسترسی ها
    /// </summary>
    public enum AccessGroupProperties
    {
        SaveContractType = 1,
        ViewContractType = 2,
        EditContractType = 3,
        DeleteContractType = 4 ,

        SaveOrganizationUnit = 5,
        ViewOrganizationUnit = 6,
        EditOrganizationUnit = 7,
        DeleteOrganizationUnit = 8,

        SaveRoleOrganization = 9,
        ViewRoleOrganization = 10,
        EditRoleOrganization = 11,
        DeleteRoleOrganization = 12,

        ViewInvoiceType = 13,
        EditInvoiceType = 14,
        DeleteInvoiceType = 15,
        SaveInvoiceType = 16,

    }
}
