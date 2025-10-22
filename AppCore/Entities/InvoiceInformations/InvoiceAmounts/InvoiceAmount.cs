using AppCore.Entities.InvoiceInformations.InvoiceBaseInformations;
using AppCore.Entities.SettingEntities.Currencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.InvoiceInformations.InvoiceAmounts
{
    /// <summary>
    /// مبالغ صورت وضعیت
    /// </summary>
    public class InvoiceAmount
    {
        #region properties
        public Guid Id { get; set; }
        /// <summary>
        /// جمع مبلغ درخواستی صورت وضعیت
        /// </summary>
        public decimal RequestedAmount { get; set; }
        /// <summary>
        /// جمع مبلغ تاییدی صورت وضعیت
        /// </summary>
        public decimal ApprovedAmount { get; set; }
        /// <summary>
        /// جمع مبلغ خالص شده صورت وضعیت
        /// </summary>
        public decimal NettingAmount {  get; set; }
        #endregion

        #region relation 
        public Guid? CurrencyId { get; set; }
        public Currency? Currency { get; set; }
        public Guid InvocieBaseInformationId { get; set; }
        public InvoiceBaseInformation InvoiceBaseInformation { get; set; }
        #endregion
    }
}
