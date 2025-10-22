using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.InvoiceDtos.InvoiceDataForTemplateDtos
{
    public class InvoiceServiceExplanationTemplateTotalAmountsDto
    {
        public string SumTotalAmount { get; set; }
        public string SumDiscountAmount { get; set; }
        public string SumTotalAmountWithDiscountAmount { get; set; }
        public string SumTax { get; set; }
        public string SumOf_SumOf_TotalAmountWithDiscount_And_Tax { get; set; }
        public string TotalAmountInLetters { get; set; }
    }
}
