using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.InvoiceInformations.InvoiceBaseCustomization
{
    public interface IInoviceBaseCustomization
    {
        public Task<InvoiceBaseCustomization> Get(Guid id);
        public Task Update(InvoiceBaseCustomization entity);
    }
}
