using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroups;

namespace AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroupPermissions
{
    public class InvoiceAccessGroupPermissions
    {
        public Guid Id { get; set; }
        public bool Invoice { get; set; }
        public bool AccessGroupSettings { get; set; }
        public bool BaseSettings { get; set; }
        public bool WorkFlow { get; set; }
        public Guid InvoiceAccessGroupId { get; set; }
        public InvoiceAccessGroup InvoiceAccessGroup { get; set; }
    }
}
