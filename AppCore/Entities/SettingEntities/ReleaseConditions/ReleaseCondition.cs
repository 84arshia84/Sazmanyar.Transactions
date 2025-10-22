using AppCore.Entities.ContractsInformation.ContractGuarantees;
using AppCore.Entities.SettingEntities.PublicSettingEntities.SettingEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.SettingEntities.ReleaseConditions
{
    /// <summary>
    /// شرط آزادسازی
    /// </summary>
    [Table("ReleaseCondition", Schema = "TAM")]
    public class ReleaseCondition : SettingEntity
    {
        #region relation 
        public List<ContractGuarantee>? ContractGuarantees { get; set; }
        #endregion 
    }
}
