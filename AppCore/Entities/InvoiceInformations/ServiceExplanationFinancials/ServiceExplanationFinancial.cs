using AppCore.Entities.InvoiceInformations.InvoiceBaseInformations;
using AppCore.Entities.SettingEntities.Currencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.InvoiceInformations.ServiceExplanationFinancials
{
    public class ServiceExplanationFinancial
    {
        #region properties
        public Guid Id { get; set; }
        public Guid ServiceExplanationId { get; set; }
        /// <summary>
        /// حجم درخواستی
        /// </summary>
        public decimal? RequestedVolume { get; set; }
        /// <summary>
        /// درصد درخواستی
        /// </summary>
        public decimal RequestedPercent{ get; set; }
        /// <summary>
        /// مبلغ درخواستی
        /// </summary>
        public decimal RequestedPrice { get; set; }
        /// <summary>
        /// حجم تایید شده
        /// </summary>
        public decimal? ApprovedVolume { get; set; }
        /// <summary>
        /// درصد تایید شده
        /// </summary>
        public decimal ApprovedPercent { get; set; }
        /// <summary>
        /// مبلغ تایید شده
        /// </summary>
        public decimal ApprovedPrice { get; set; }
        #endregion
        #region relations
        public Guid? CurrencyId { get; set; }
        public Currency? Currency {  get; set; }
        public Guid InvoiceBaseInformationId { get; set; }
        public InvoiceBaseInformation InvoiceBaseInformation { get; set; }
        #endregion
    }
}
