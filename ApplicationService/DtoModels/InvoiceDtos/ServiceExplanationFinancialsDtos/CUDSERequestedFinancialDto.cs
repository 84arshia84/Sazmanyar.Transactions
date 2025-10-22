using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.InvoiceDtos.ServiceExplanationFinancialsDtos
{
    public class CUDSERequestedFinancialDto
    {
        public Guid Key { get; set; }
        public decimal? RequestedVolume { get; set; }
        public decimal? RequestedPercent { get; set; }
        public decimal? RequestedPrice { get; set; }
        public Guid ServiceExplanationId { get; set; }
        public Guid InvoiceBaseInformationId { get; set; }
        public Guid? CurrencyId { get; set; }
        public decimal? ProgramVolume { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsFinancial { get; set; }
        public bool IsDelete { get; set; }
    }
}
