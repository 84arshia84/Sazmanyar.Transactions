using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.InvoiceDtos.NettingProcessDtos
{
    public class NettedAmountGetDto
    {
        public decimal NettedAmount { get; set; }
        public Guid? Currency { get; set; }
    }
}
