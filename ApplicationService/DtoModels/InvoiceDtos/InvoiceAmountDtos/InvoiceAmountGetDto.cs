using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.InvoiceDtos.InvoiceAmountDtos
{
    public class InvoiceAmountGetDto
    {
        public decimal Amount { get; set; }
        public string CurrencyTitle { get; set; }    
    }
}
