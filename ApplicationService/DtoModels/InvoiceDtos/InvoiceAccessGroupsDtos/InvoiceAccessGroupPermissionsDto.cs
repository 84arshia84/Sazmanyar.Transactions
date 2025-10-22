using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.InvoiceDtos.InvoiceAccessGroupsDtos
{
    public class InvoiceAccessGroupPermissionsDto
    {
        public bool Invoice { get; set; }
        public bool AccessGroupSettings { get; set; }
        public bool BaseSettings { get; set; }
        public bool WorkFlow { get; set; }
    }
}
