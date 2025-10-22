using AppCore.Entities.ContractsInformation.ContractGuarantees;
using AppCore.Entities.SettingEntities.PublicSettingEntities.SettingEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.SettingEntities.TypeOfGuarantees
{
    /// <summary>
    /// نوع ضمانت
    /// </summary>
    public class TypeOfGuarantee:SettingEntity
    {
        #region relation
        public List<ContractGuarantee>? ContractGuarantees { get; set; }
        #endregion
    }
}
