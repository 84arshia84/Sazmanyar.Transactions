using AppCore.Entities.ContractsInformation.ServiceExplanations;
using AppCore.Entities.FactorInformation.FactorServiceExplanations;
using AppCore.Entities.SettingEntities.PublicSettingEntities.SettingEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.SettingEntities.UnitOfMeasurements
{
    /// <summary>
    /// واحد اندازه گیری
    /// </summary>
    [Table("UnitOfMeasurement", Schema = "TAM")]
    public class UnitOfMeasurement : SettingEntity
    {
        #region properties
        #endregion
        #region relation
        public List<FactorServiceExplanation>? FactorServiceExplanations { get; set; }
        #endregion
    }
}
