using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.InvoiceDtos.NettingProcessDtos
{
    public class ApprovedAmountGetDto
    {
        public decimal ApproveAmount { get; set; }
        public Guid? Currency { get; set; }
    }
}
