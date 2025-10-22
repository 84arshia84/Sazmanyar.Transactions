using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.InvoiceDtos.ServiceExplanationFinancialsDtos
{
    public class SetSEApprovedFinancialDto
    {
        public decimal? ApprovedVolume { get; set; }
        public decimal? ApprovedPercent { get; set; }
        public decimal? ApprovedPrice { get; set; }
        public Guid ServiceExplanationFinancailId { get; set; }
        public decimal? ProgramVolume { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
