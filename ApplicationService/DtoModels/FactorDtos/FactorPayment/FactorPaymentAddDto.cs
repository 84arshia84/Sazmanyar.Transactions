using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.FactorDtos.FactorPayment
{
    public class FactorPaymentAddDto
    {
        public Guid FactorId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal PaymentAmount { get; set; }
        public decimal AccelerationRate { get; set; }
        public Guid HowToPayId { get; set; }
        public Guid CurrencyId { get; set; }
    }
}
