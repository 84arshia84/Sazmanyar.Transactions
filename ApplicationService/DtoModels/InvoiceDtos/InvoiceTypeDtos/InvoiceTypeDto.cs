using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.InvoiceDtos.InvoiceTypeDtos
{
    public class InvoiceTypeDto
    {
        public Guid Key { get; set; }
        public long Row { get; set; }
        public string Title { get; set; }
        public Guid? OfficeOnlineDocumentId { get; set; }
    }
}
