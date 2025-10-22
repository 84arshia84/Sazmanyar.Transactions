using AppCore.Entities.ContractsInformation.Contratcs;
using AppCore.Entities.FactorInformation.Factors;
using AppCore.Entities.SettingEntities.PublicSettingEntities.SettingEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.SettingEntities.CreditSources
{
    /// <summary>
    /// محل تامین اعتبار
    /// </summary>
    [Table("CreditSource", Schema = "TAM")]
    public class CreditSource : SettingEntity
    {
        /// <summary>
        /// کد محل تامین
        /// </summary>
        public string? CreditSourceCode { get; set; }
        public Guid? ParentID { get; set; }
        #region relation
        public List<Contract>? Contract { get; set; }
        public List<Factor>? Factors { get; set; }
        #endregion
    }
}
