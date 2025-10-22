using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Entities.ContractAccessGroups;

namespace AppCore.Entities.FactorAccessGroup
{
    public class FactorAccessGroupUsers
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid AccessGroupId { get; set; }
        public FactorAccessGroup factorAccessGroup { get; set; }
    }
}
