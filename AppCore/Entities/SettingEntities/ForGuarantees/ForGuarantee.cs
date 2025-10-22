using AppCore.Entities.ContractsInformation.ContractGuarantees;
using AppCore.Entities.SettingEntities.PublicSettingEntities.SettingEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.SettingEntities.ForGuarantees
{
    /// <summary>
    /// بابت (تضمین )
    /// </summary>
    [Table("ForGuarantee", Schema = "TAM")]
    public class ForGuarantee : SettingEntity
    {
        #region relation 
        public List<ContractGuarantee>? ContractGuarantees { get; set; }
        #endregion
    }
}
