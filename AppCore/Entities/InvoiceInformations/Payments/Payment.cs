using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Entities.InvoiceInformations.InvoiceBaseInformations;
using AppCore.Entities.SettingEntities.Currencies;
using AppCore.Entities.SettingEntities.HowToPays;

namespace AppCore.Entities.InvoiceInformations.Payments
{
    /// <summary>
    /// پرداخت 
    /// </summary>
    public class Payment
    {
        #region properties
        public Guid Id { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal PaymentAmount { get; set; }
        public decimal ExchangeRate { get; set; }
        public DateTime InsertDate { get; set; }
        public Guid InsertBy { get; set; }
        public Guid? DeleteBy { get; set; }
        public DateTime? DeleteDate { get; set; }
        #endregion
        #region relations
        public Guid InvoiceBaseInformationId { get; set; }
        public InvoiceBaseInformation InvoiceBaseInformation { get; set; }
        public Guid HowToPayId { get; set; }
        public HowToPay HowToPay { get; set; }
        public Guid CurrencyId { get; set; }
        public Currency Currency { get; set; }
        #endregion
    }
}
