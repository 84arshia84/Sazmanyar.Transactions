using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Entities.ContractAccessGroups;

namespace AppCore.Entities.FactorAccessGroup
{

    /// <summary>
    /// رابط کاربر ها با هم و با گروه دسترسی فاکتور
    /// </summary>
    public class FactorAccessGroupGroups
    {
        public Guid Id { get; set; }
        public Guid ParentGroupId { get; set; }
        public Guid GroupId { get; set; }
        public FactorAccessGroup factorAccessGroup { get; set; }
        public FactorAccessGroup AccessGroupParent { get; set; }
    }
}
