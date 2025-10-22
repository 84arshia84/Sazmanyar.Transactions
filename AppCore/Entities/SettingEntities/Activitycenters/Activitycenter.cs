using AppCore.Entities.ContractsInformation.ServiceExplanations;
using AppCore.Entities.FactorInformation.Factors;
using AppCore.Entities.FactorInformation.FactorServiceExplanations;
using AppCore.Entities.SettingEntities.PublicSettingEntities.SettingEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.SettingEntities.Activitycenters
{
    /// <summary>
    /// مرکز هزینه
    /// </summary>
    [Table("Activitycenters", Schema = "TAM")]
    public class Activitycenter : SettingEntity
    {
        #region properties
        /// <summary>
        /// کد
        /// </summary>
        public string? Code { get; set; }

        #endregion
        #region relation
        #endregion

    }
}
