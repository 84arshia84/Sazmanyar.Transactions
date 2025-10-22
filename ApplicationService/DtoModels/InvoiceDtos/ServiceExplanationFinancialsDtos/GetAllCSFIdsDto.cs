using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.InvoiceDtos.ServiceExplanationFinancialsDtos
{
    public class GetAllCSFIdsDto
    {
        public Guid ContractId { get; set; }
        public Guid InvoiceBaseInformationId { get; set; }
    }
}
