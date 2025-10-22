using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroups;
using AppCore.Entities.SettingEntities.Organizationalunits;

namespace AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroupUsers
{
    public class InvoiceAccessGroupUser
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid AccessGroupId { get; set; }
        public InvoiceAccessGroup AccessGroup { get; set; }
    }
}
