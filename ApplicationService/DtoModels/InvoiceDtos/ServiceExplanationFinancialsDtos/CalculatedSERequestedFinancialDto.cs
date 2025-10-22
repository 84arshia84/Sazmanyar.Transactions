using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.InvoiceDtos.ServiceExplanationFinancialsDtos
{
    public class CalculatedSERequestedFinancialDto
    {
        public decimal? CalculatedRequestedVolume { get; set; }
        public decimal CalculatedRequestedPercent { get; set; }
        public decimal CalculatedRequestedPrice { get; set; }
    }
}
