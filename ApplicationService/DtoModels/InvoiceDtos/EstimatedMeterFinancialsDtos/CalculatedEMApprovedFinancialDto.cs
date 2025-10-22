using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.InvoiceDtos.EstimatedMeterFinancialsDtos
{
    public class CalculatedEMApprovedFinancialDto
    {
        public decimal? CalculatedApprovedVolume { get; set; }
        public decimal CalculatedApprovedPercent { get; set; }
        public decimal CalculatedApprovedPrice { get; set; }
    }
}
