using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationService.DtoModels.InvoiceDtos.NettingProcessDtos;
using ApplicationService.DtoModels.InvoiceDtos.PaymentDtos;
using ApplicationService.DtoModels.UserDtos;

namespace ApplicationService.ServicesContract.InvoiceInformations
{
    public interface IPaymentService
    {
        Task<(string message, bool isSuccess)> Add(AddPaymentDto addPaymentDto, LoginUserDto userDto);
        Task<(string message, bool isSuccess)> Update(UpdatePaymentDto updatePaymentDto, LoginUserDto userDto);
        Task<(string message, bool isSuccess)> Delete(Guid paymentId, LoginUserDto userDto);
        Task<List<GetAllPaymentDto>> GetAll(Guid invoiceBaseInformationId);
        Task<List<NettedAmountGetDto>> GetInvoicePaidPrice(Guid invoiceBaseInformationId);
        Task<List<NettedAmountGetDto>> GetContractPaidPrice(Guid contractId);
        Task<List<NettedAmountGetDto>> GetAllBeforThisInvoiceBaseInformationId(Guid invoiceBaseInformationId);
    }
}
