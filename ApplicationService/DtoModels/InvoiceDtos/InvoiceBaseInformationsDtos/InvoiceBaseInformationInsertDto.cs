using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.InvoiceDtos.InvoiceBaseInformationsDtos
{
    public class InvoiceBaseInformationInsertDto
    {
        public Guid Key { get; set; }
        public string InvoiceTitle { get; set; }
        public DateTime? LeadingToDate { get; set; }
        public DateTime SendDate { get; set; }
        public string? Description { get; set; }
        public string InvoiceCode { get; set; }
        public int? InvoiceNumber { get; set; }
        public Guid ContractId { get; set; }
        public Guid InvoiceTypeId { get; set; }
        public Guid? TransActionTypeId { get; set; }
        public Guid? ContractAddendumId { get; set; }
        public Guid? AccountId { get; set; }
    }
}
