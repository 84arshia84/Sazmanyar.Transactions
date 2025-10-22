using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroups;
using AppCore.Entities.InvoicesInformations.InvoiceType;
using AppCore.Entities.SettingEntities.Organizationalunits;

namespace AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroupOrganizationUnits
{
    public class InvoiceAccessGroupOrganizationUnit
    {
        public Guid Id { get; set; }
        public Guid OrganizationUnitId { get; set; }
        public Guid InvoiceAccessGroupId { get; set; }
        public InvoiceAccessGroup InvoiceAccessGroup { get; set; }
        public Organizationalunit OrganizationalUnit { get; set; }
    }
}
