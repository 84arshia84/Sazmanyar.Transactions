using ApplicationService.DtoModels.DesignCodeDtos.InvoiceDesignCodeDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.DesignCode
{
    public interface IInvoiceDesignCodeService
    {
        public Task<(string message, bool isSuccess)> Add(InvoiceDesignCodeAddDto invoiceDesignCode);
        public Task<(string message, bool isSuccess)> Update(InvoiceDesignCodeUpdateDto invoiceDesignCode);
        public Task UpdateCounter(Guid id);
        public Task<(string message, bool isSuccess)> Delete(Guid id);
        public Task<List<InvoiceDesignCodeGetDto>> GetAll();
        public Task<List<InvoiceDesignCodeGetParameterDto>> GetAllParameter();
        public Task<List<InvoiceDesignCodeGetParameterDto>> GetAllParameterById(Guid id);
        public Task<(string designcode, Guid designCodeId)> GenerateDesignCode(InvoiceDesignCodeSearchParameterDto parameterDto);
    }
}
