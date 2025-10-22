using AppCore.Entities.SettingEntities.ContractTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.ContractAccessGroups
{
    /// <summary>
    /// رابط نوع معامله و گروه دسترسی معاملات
    /// </summary>
    public class ContractAccessGroupContractType
    {
        #region properties
        public Guid Id { get; set; }
        public Guid ContractTypeId { get; set; }
        public Guid ContractAccessGroupId { get; set; }
        #endregion
        #region relations
        public ContractAccessGroup ContractAccessGroup { get; set; }
        public ContractType ContractType { get; set; }
        #endregion
    }
}
