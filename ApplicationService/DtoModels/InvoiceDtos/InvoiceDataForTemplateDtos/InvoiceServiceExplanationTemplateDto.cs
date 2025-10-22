using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.InvoiceDtos.InvoiceDataForTemplateDtos
{
    public class InvoiceServiceExplanationTemplateDto
    {
        public string Title { get; set; }
        public string Count { get; set; }
        public string UnitOfMesurment { get; set; }
        public string UnitAmount { get; set; }
        public string TotalAmount { get; set; }
        public string DiscountAmount { get; set; }
        public string TotalAmountWithDiscountAmount { get; set; }
        public string Tax { get; set; }
        public string SumOf_TotalAmountWithDiscount_And_Tax { get; set; }
    }
}
