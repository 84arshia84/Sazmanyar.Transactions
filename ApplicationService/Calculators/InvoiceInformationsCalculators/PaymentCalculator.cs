using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Entities.InvoiceInformations.Payments;
using AppCore.Entities.InvoiceInformations.ServiceExplanationFinancials;

namespace ApplicationService.Calculators.InvoiceInformationsCalculators
{
    internal static class PaymentCalculator
    {

        public static decimal CalculatePaymentPrice(List<Payment> allPayments)
        {
            decimal result = 0;
            result = allPayments.Sum(p => p.PaymentAmount);
            return result;
        }
    }
}
