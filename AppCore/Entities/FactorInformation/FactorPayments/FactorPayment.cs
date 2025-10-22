using AppCore.Entities.FactorInformation.Factors;
using AppCore.Entities.SettingEntities.Currencies;
using AppCore.Entities.SettingEntities.HowToPays;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.FactorInformation.FactorPayments
{
    /// <summary>
    /// پرداخت فاکتور
    /// </summary>
    public class FactorPayment
    {
        #region properties
        public Guid Id { get; set; }
        /// <summary>
        /// تاریخ پرداخت
        /// </summary>
        public DateTime PaymentDate { get; set; }
        /// <summary>
        /// مقدار پرداخت شده
        /// </summary>
        public decimal PaymentAmount { get; set; }
        /// <summary>
        /// نرخ تسعیر
        /// </summary>
        public decimal AccelerationRate { get; set; }
        public DateTime InsertDate { get; set; }
        public Guid InsertBy { get; set; }
        public Guid? DeleteBy { get; set; }
        public DateTime? DeleteDate { get; set; }
        public bool? IsDeleted { get; set; }
        #endregion

        #region relations
        /// <summary>
        /// نحوه پرداخت
        /// </summary>
        public Guid HowToPayId { get; set; }
        public HowToPay HowToPay { get; set; }
        /// <summary>
        /// ارز
        /// </summary>
        public Guid CurrencyId { get; set; }
        public Currency Currency { get; set; }
        public Guid FactorId { get; set; }
        public Factor Factor { get; set; }
        #endregion
    }
}
