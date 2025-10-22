using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.InvoiceDtos.PaymentDtos
{
    public class AddPaymentDto
    {
        public Guid Key { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal PaymentAmount { get; set; }
        public decimal ExchangeRate { get; set; }
        public Guid HowToPayId { get; set; }
        public Guid CurrencyId { get; set; }
        public Guid InvoiceBaseInformationId { get; set; }

    }
}
