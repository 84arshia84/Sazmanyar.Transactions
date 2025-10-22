using ApplicationService.DtoModels.ContractDtos;
using ApplicationService.DtoModels.FactorDtos.FactorStageRoles;
using ApplicationService.DtoModels.InvoiceDtos.InvoiceStageRolesDtos;
using ApplicationService.DtoModels.ReminderDtos;
using ApplicationService.DtoModels.WFEDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.WFEInvoice
{
    public interface IWfeInvoiceService
    {
        public Task<GetFirstStageDto> GetFirstStage(string userFullQualify);
        public Task InsertDefaultApprovers(string userFullQualify, Guid invoiceId);
        public Task<List<Guid>> GetAllWaitingForActionIds(string userFullQualify);
        public Task<List<WFEGetCurentEntitiesForUserDto>> GetAllWaitingForAction(string userFullQualify);
        public Task<List<Guid>> GetAllUserCanSeenIds(string userFullQualify);
        public Task<List<WFEGetCurentEntitiesForUserDto>> GetAcceptableActionsForEntity(string userFullQualify);
        public Task<(string message, bool isSucess)> SendInvoice(List<Guid> invoiceIds, string userFullQualify);
        public Task<(string message, bool isSucess)> ApproveInvoice(Guid invoiceId, string userFullQualify,bool checkPaymentIsComplet,int stage);
        public Task<(string message, bool isSucess)> RejectInvoice(Guid invoiceId, string userFullQualify);
        public Task<(string message, bool isSucess)> AssignInvoice(Guid continvoiceIdractId, string userFullQualify, List<Guid> userIds);
        public Task<(string message, bool isSucess)> ReturnInvoice(Guid invoiceId, string userFullQualify);
        public Task<List<StageDetailsDto>> GetInvoiceStageDetails(Guid invoiceId, string userFullQualify);
        public Task<bool> WaitForMe(Guid invoiceId, string userFullQualify);
        public Task<List<InvoiceStageRolseDto>> InvoiceStageRoles(Guid invoiceId, string userFullQualify);
        public Task<List<Guid>> InvoiceGetEntitiesAwaitingUserAction(string userFullQualify);
        public Task<List<Guid>> InvoiceGetEntitiesAwaitingApproval(string userFullQualify);
        public Task<List<WFEGetCurentEntitiesForUserDto>> InvoiceGetEntitiesAwaitingApprovalDto(string userFullQualify);
        

    }
}
