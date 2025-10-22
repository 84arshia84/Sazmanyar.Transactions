using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.InvoiceDtos.NettingProcessDtos
{
    public class UpdateNettingProcessItemDto
    {
        public Guid Key { get; set; }
        public string Title { get; set; }
        public decimal Percentage { get; set; }
        public decimal Amount { get; set; }
        public Guid? InvoiceId { get; set; }
        public Guid? CurrencyId { get; set; }
        public int NettingProcessType { get; set; }

    }
}
