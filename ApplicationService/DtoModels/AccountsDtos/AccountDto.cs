using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.AccountsDtos
{
    public class AccountDto
    {
        public Guid Id { get; set; }
        public string AccountNumber { get; set; }
        public string Bank { get; set; }
        public string Branch { get; set; }
        public bool IsActive { get; set; }
        public string Sheba { get; set; }
        public Guid OrganizationInformationId { get; set; } = Guid.Empty;
    }
}
