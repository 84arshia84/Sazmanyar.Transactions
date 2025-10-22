using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Entities.SettingEntities.HowToPays;

namespace ApplicationService.DtoModels.InvoiceDtos.PaymentDtos
{
    public class GetAllPaymentDto
    {
        public Guid Key { get; set; }
        public long Row { get; set; }
        public Guid InvoiceBaseInformationId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal PaymentAmount { get; set; }
        public decimal ExchangeRate { get; set; }
        public decimal CurrencyAmountEquivalent { get; set; }
        public Guid HowToPayId { get; set; }
        public string HowToPayTitle { get; set; }
        public Guid CurrencyId { get; set; }
        public string CurrencyTitle { get; set; }
    }
}
