using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroupInvoiceTypes;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroupOrganizationUnits;

namespace AppCore.Entities.InvoicesInformations.InvoiceType
{
    public class InvoiceType
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid? OfficeOnlineDocumentId { get; set; }
        public ICollection<InvoiceAccessGroupInvoiceType> InvoiceAccessGroupInvoiceTypes { get; set; }
    }
}
