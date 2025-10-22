using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.ContractAccessGroups
{
    /// <summary>
    /// رابط کاربر ها با هم و با گروه دسترسی معاملات
    /// </summary>
    public class ContractAccessGroupGroups
    {
        public Guid Id { get; set; }
        public Guid ParentGroupId { get; set; }
        public Guid GroupId { get; set; }
        public ContractAccessGroup AccessGroup { get; set; }
        public ContractAccessGroup AccessGroupParent { get; set; }
    }
}
