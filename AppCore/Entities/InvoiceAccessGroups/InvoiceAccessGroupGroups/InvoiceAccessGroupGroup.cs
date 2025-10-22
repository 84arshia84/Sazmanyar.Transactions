using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroups;

namespace AppCore.Entities.InvoiceAccessGroups.AccessGroupGroups
{
    public class InvoiceAccessGroupGroup
    {
        public Guid Id { get; set; }
        public Guid ParentGroupId { get; set; }
        public Guid GroupId { get; set; }
        public InvoiceAccessGroup AccessGroup { get; set; }
        public InvoiceAccessGroup AccessGroupParent { get; set; }
    }
}
