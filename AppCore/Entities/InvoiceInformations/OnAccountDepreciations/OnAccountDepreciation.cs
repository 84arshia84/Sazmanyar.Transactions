using AppCore.Entities.SettingEntities.Currencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.InvoiceInformations.OnAccountDepreciations
{
    /// <summary>
    /// استهلاک علی الحساب
    /// </summary>
    public class OnAccountDepreciation
    {
        #region properties
        public Guid Id { get; set; }
        public Guid ServiceExplenationId { get; set; }
        public Guid ServiceExplenationFinancialId { get; set; }
        public Guid InvoiceId { get; set; }
        public bool IsDeleted { get; set; }
        /// <summary>
        /// تاریخ استهلاک
        /// </summary>
        public DateTime DepreciationDate { get; set; }
        /// <summary>
        /// مبلغ پیشنهادی جهت استهلاک 
        /// </summary>
        public decimal SuggestedDepreciationAmount { get; set; }
        /// <summary>
        /// مبلغ مورد تایید جهت استهلاک
        /// </summary>
        public decimal ApprovedDepreciationAmount { get; set; }
        #endregion
        #region relation
        public Guid? CurrencyId { get; set; }
        public Currency? Currency { get; set; }
        #endregion
    }
}
