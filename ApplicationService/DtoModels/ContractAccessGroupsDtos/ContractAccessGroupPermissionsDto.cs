using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.ContractAccessGroupsDtos
{
    public class ContractAccessGroupPermissionsDto
    {
        public Guid Id { get; set; }
        public bool TransactionExecutionRequest { get; set; }
        public bool Contract { get; set; }
        public bool ContractAddendum { get; set; }
        public bool AccessGroupSettings { get; set; }
        public bool BaseSettings { get; set; }
        public bool PriceListSettings { get; set; }
        public bool WorkFlow { get; set; }
    }
}
