using AppCore.Entities.ContractsInformation.ExecutionRequestServiceExplanations;
using AppCore.Entities.ContractsInformation.ServiceExplanations;
using AppCore.Entities.FactorInformation.FactorAmounts;
using AppCore.Entities.FactorInformation.FactorNettingProcessItems;
using AppCore.Entities.FactorInformation.FactorPayments;
using AppCore.Entities.FactorInformation.FactorServiceExplanations;
using AppCore.Entities.InvoiceInformations.InvoiceAmounts;
using AppCore.Entities.InvoiceInformations.NettingProcesses;
using AppCore.Entities.InvoiceInformations.OnAccountDepreciations;
using AppCore.Entities.InvoiceInformations.PrePaymentDepreciations;
using AppCore.Entities.SettingEntities.PublicSettingEntities.SettingEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.SettingEntities.Currencies
{
    /// <summary>
    /// ارز
    /// </summary>
    [Table("Currency", Schema = "TAM")]
    public class Currency :SettingEntity
    {
        #region relation
        public List<ServiceExplanation> ServiceExplanation { get; set; }
        public List<FactorServiceExplanation>? FactorServiceExplanations { get; set; }
        public List<FactorPayment>? FactorPayments { get; set; }
        public List<ExecutionRequestServiceExplanation> ExecutionRequestServiceExplanations { get; set; }
        public List<FactorNettingProcessItem>? FactorNettingProcessItems { get; set; }
        public List<NettingProcessItem>? InvoiceNettingProcessItems { get; set; }
        public List<InvoiceAmount>? InvoiceAmounts { get; set; }
        public List<FactorAmount>? FactorAmounts { get; set; }
        public List<PrePaymentDepreciation>? PrePaymentDepreciations { get; set; }
        public List<OnAccountDepreciation>? OnAccountDepreciations { get; set; }
        #endregion
    }
}
