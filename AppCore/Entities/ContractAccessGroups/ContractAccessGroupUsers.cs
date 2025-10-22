using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.ContractAccessGroups
{
    /// <summary>
    /// رابط کاربر با گروه دسترسی معاملات
    /// </summary>
    public class ContractAccessGroupUsers
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid AccessGroupId { get; set; }

        public ContractAccessGroup AccessGroup { get; set; }
    }
}
