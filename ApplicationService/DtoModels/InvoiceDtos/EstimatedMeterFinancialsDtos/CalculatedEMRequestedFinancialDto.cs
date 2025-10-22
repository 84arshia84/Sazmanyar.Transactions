using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.InvoiceDtos.EstimatedMeterFinancialsDtos
{
    public class CalculatedEMRequestedFinancialDto
    {
        public decimal? CalculatedRequestedVolume { get; set; }
        public decimal CalculatedRequestedPercent { get; set; }
        public decimal CalculatedRequestedPrice { get; set; }
    }
}
