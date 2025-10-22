using AppCore.Entities.InvoiceInformations.InvoiceBaseInformations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.InvoiceDtos.InvoiceBaseCustomizationDtos
{
    public class InvoiceBaseCustomizationDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Width { get; set; }
        public bool IsHidden { get; set; }
        public Guid InvoiceBaseInformationId {  get; set; }
        public InvoiceBaseInformation InvoiceBaseInformation { get; set; }
    }
}
