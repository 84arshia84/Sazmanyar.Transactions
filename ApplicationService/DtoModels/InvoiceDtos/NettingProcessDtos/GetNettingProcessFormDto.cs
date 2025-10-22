using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.InvoiceDtos.NettingProcessDtos
{
    public class GetNettingProcessFormDto
    {
        public Guid Key { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal ApprovedGrossAmount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal ApprovedNetAmount { get; set; }
        public decimal GrossAmountSummation { get; set; }
        public decimal ApprovedGrossAmountSummation { get; set; }
        public decimal NetAmountSummation { get; set; }
        public decimal ApprovedNetAmountSummation { get; set; }
        public List<GetAllNettingProcessItemsDto> NettingProcessItems { get; set; }
    }
}
