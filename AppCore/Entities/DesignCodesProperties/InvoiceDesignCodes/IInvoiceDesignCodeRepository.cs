using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.DesignCodesProperties.InvoiceDesignCodes
{
    public interface IInvoiceDesignCodeRepository
    {
        public Task Add(InvoiceDesignCode contractDesignCode);
        public Task AddParameters(List<InvoiceDesignCodeParameterRel> parameters);
        public Task Update(InvoiceDesignCode invoiceDesignCode);
        public Task UpdateCounter(Guid id);
        public Task Delete(Guid id);
        public Task DeleteParameters(Guid id);
        public Task<InvoiceDesignCode> Get(Guid id);
        public Task<List<InvoiceDesignCode>> GetAll();
        public Task<List<InvoiceDesignCodeParameterRel>> GetAllParameters(Guid id);
        public Task<InvoiceDesignCode> GetByParameter(Guid contractTypeId, Guid organizationUnitId, Guid roleOfOrganizationId,Guid inovieTypeId);
        public Task<List<InvoiceDesignCodeParameterRel>> GetAllParameters();
    }
}
