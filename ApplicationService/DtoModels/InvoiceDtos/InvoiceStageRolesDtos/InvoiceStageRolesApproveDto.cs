using AppCore.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.InvoiceDtos.InvoiceStageRolesDtos
{
    public class InvoiceStageRolesApproveDto
    {
        public string TempFile { get; set; }
        public string name {get;set;}
        public ICollection<User> Users { get; set;}
    }
}
