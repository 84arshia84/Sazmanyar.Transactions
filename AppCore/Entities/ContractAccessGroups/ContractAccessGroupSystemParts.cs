using AppCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.ContractAccessGroups
{
    /// <summary>
    /// رابط پارامتر های گروه دسترسی و بخش ها سامانه
    /// </summary>
    public class ContractAccessGroupSystemParts
    {
        #region properties
        public Guid Id { get; set; }
        public SystemParts SystemParts { get; set; }
        public AccessGroupProperties AccessGroupProperties { get; set; }
        #endregion
        #region relations
        public Guid ContractAccessGroupId { get; set; }
        public ContractAccessGroup ContractAccessGroup { get; set; }
        #endregion
    }
}
