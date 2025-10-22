using ApplicationService.DtoModels.WFEDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.WFEContract
{
    public interface IWfeTransactionExecutionRequestService
    {
        public Task<GetFirstStageDto> GetFirstStage(string userFullQualify);
        public Task InsertDefaultApprovers(string userFullQualify, Guid transaction );
        public Task<List<Guid>> GetAllWaitingForActionIds(string userFullQualify);
        public Task<List<WFEGetCurentEntitiesForUserDto>> GetAllWaitingForAction(string userFullQualify);
        public Task<List<Guid>> GetAllUserCanSeenIds(string userFullQualify);
        public Task<List<WFEGetCurentEntitiesForUserDto>> GetAllUserCanSeen(string userFullQualify);
        public Task<List<WFEGetCurentEntitiesForUserDto>> GetAcceptableActionsForEntity(string userFullQualify);
        public Task<(string message, bool isSucess)> SendTransaction(List<Guid> transactionId, string userFullQualify);
        public Task<(string message, bool isSucess)> ApproveTransaction(Guid transactionId, string userFullQualify);
        public Task<(string message, bool isSucess)> RejectTransaction(Guid transactionId, string userFullQualify);
        public Task<(string message, bool isSucess)> AssignTransaction(Guid transactionId, string userFullQualify, List<Guid> userIds);
        public Task<(string message, bool isSucess)> ReturnTransaction(Guid transactionId, string userFullQualify);
        public Task<List<StageDetailsDto>> GetTransactionStageDetails(Guid transactionId, string userFullQualify);
        public Task<List<Guid>> GetEntitiesAwaitingUserAction(string userFullQualify);
        public Task<List<Guid>> GetEntitiesAwaitingApproval(string userFullQualify);
    }
}
