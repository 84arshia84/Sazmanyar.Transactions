using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.InvoiceDtos.NettingProcessDtos
{
    public class RequestAmountGetDto
    {
        public decimal RequestAmount { get; set; }
        public Guid? Currency { get; set; }
    }
}
