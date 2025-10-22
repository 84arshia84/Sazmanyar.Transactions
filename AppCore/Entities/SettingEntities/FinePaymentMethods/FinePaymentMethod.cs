using AppCore.Entities.ContractsInformation.ExecutionRequestServiceExplanations;
using AppCore.Entities.ContractsInformation.ServiceExplanations;
using AppCore.Entities.SettingEntities.PublicSettingEntities.SettingEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.SettingEntities.FinePaymentMethods
{
    /// <summary>
    /// روش های پرداخت جریمه
    /// </summary>
    [Table("FinePaymentMethod", Schema = "TAM")]
    public class FinePaymentMethod : SettingEntity
    {
        #region relation
        public List<ServiceExplanation> ServiceExplanation { get; set; }
        public List<ExecutionRequestServiceExplanation> ExecutionRequestServiceExplanations { get; set; }

        #endregion
    }
}
