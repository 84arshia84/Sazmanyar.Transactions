using AppCore.Entities.InvoiceInformations.NettingProcesses;
using ApplicationService.DtoModels.ContractDtos;
using ApplicationService.DtoModels.InvoiceDtos.InvoiceBaseInformationsDtos;
using ApplicationService.DtoModels.InvoiceDtos.NettingProcessDtos;
using ApplicationService.DtoModels.InvoiceDtos.ServiceExplanationFinancialsDtos;
using ApplicationService.DtoModels.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.InvoiceInformations
{
    public interface INettingProcessItemService
    {
        Task<(string message, bool isSuccess)> AddNettingProcessItem(AddNettingProcessItemDto addNettingProcessItemDto, LoginUserDto userDto);
        Task AddDefaultNettingProcess(CUDSERequestedFinancialDto cUDSERequestedFinancialDto,ServiceExplanationDto serviceExplanation, LoginUserDto userDto);
        Task<(string message, bool isSuccess)> UpdateNettingProcessItem(UpdateNettingProcessItemDto updateNettingProcessItemDto, LoginUserDto userDto);
        Task<(string message, bool isSuccess)> DeleteNettingProcessItem(Guid nettingProcessItemId, LoginUserDto userDto);
        Task<List<GetAllNettingProcessItemsDto>> GetAllNettingProcessItems(Guid invoiceBaseInformationId);
        Task<List<RequestAmountGetDto>> GetInvoiceRequestedPriceSummation(Guid invoiceBaseInformationId);
        Task<List<ApprovedAmountGetDto>> GetInvoiceApprovedPriceSummation(Guid invoiceBaseInformationId);
        Task<List<RequestAmountGetDto>> GetContractInvoicesRequestedPriceSummation( Guid invoiceBaseInformationId);
        Task<List<ApprovedAmountGetDto>> GetContractInvoicesApprovedPriceSummation( Guid invoiceBaseInformationId);
        Task<List<NettedAmountGetDto>> GetInvoiceNetPrice(Guid invoiceBaseInformationId);
        Task<List<NettedAmountGetDto>> GetNetPriceUnitlThisInvoice(Guid invoiceBaseInformationId);
    }
}
