using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.InvoiceInformations.InvoiceBaseCustomization
{
    public class InvoiceBaseCustomization
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public bool IsHidden { get; set; }
        public string Title { get; set; }
        public string Width { get; set; }
    }
}
