using AppCore.Entities.InvoiceInformations.InvoiceBaseInformations;
using ApplicationService.DtoModels.InvoiceDtos.InvoiceBaseInformationsDtos;
using ApplicationService.DtoModels.UserDtos;
using ApplicationService.DtoModels.WFEDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.InvoiceInformations
{
    public interface IInvoiceBaseInformationService
    {
        Task<(string message, bool isSuccess, Guid invoiceBaseInformationId)> Add(InvoiceBaseInformationInsertDto invoiceBaseInformation, LoginUserDto userDto, string connectionString);
        Task<(string message, bool isSuccess)> Delete(Guid id, LoginUserDto userDto);
        Task<(string message, bool isSuccess)> Update(InvoiceBaseInformationInsertDto invoiceBaseInformation, LoginUserDto userDto);
        // InvoiceBaseInformationGetDto => InvoiceBaseInformationFullGetDto
        Task<InvoiceBaseInformationFullGetDto> Get(Guid id);
        Task<GetContractCorespondentInformationsDto> GetContractCorespondentInformations(Guid contractId);
        Task<List<InvoiceBaseInformationGetAllDto>> GetAllForContract(Guid contractId, string fullQualifyName, string connectionString);
        Task<List<InvoiceBaseInformationGetAllDto>> GetAll(string fullQualifyName, string connectionString);
        Task<List<InvoiceBaseInformationGetAllDto>> GetAllMine(string fullQualifyName, string connectionString);
        Task<List<InvoiceBaseInformationGetAllDto>> GetAllWaitingForAction(string fullQualifyName, string connectionString);
        public Task<List<InvoiceBaseInformationGetAllDto>> GetAllWaitingForApprove(string fullQualifyName, string connectionString);

        Task<List<InvoiceBaseInformationGetAllDto>> SearchAll(string fullQualifyName, string connectionString, string? term);
        Task<List<InvoiceBaseInformationGetAllDto>> SearchMine(string fullQualifyName, string connectionString, string? term);
        Task<List<InvoiceBaseInformationGetAllDto>> SearchWaitForAction(string fullQualifyName, string connectionString, string? term);
        Task<List<InvoiceBaseInformationGetAllDto>> SearchByContract(Guid contractId,string fullQualifyName, string connectionString, string? term);
    }
}
