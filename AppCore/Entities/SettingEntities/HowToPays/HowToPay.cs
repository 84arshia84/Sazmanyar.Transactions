using AppCore.Entities.FactorInformation.FactorPayments;
using AppCore.Entities.SettingEntities.PublicSettingEntities.SettingEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.SettingEntities.HowToPays
{
    /// <summary>
    /// نحوه پرداخت
    /// </summary>
    [Table("HowToPay", Schema = "TAM")]
    public class HowToPay : SettingEntity
    {
        #region relation 
        public List<FactorPayment>? FactorPayments { get; set; }
        #endregion
    }
}
