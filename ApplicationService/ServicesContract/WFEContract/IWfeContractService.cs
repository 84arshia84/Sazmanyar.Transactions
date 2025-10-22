using ApplicationService.DtoModels.ContractDtos;
using ApplicationService.DtoModels.WFEDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.WFEContract
{
    public interface IWfeContractService
    {
        public Task<GetFirstStageDto> GetFirstStage(string userFullQualify);
        public Task InsertDefaultApprovers (string userFullQualify,Guid contractId);
        public Task<List<Guid>> GetAllWaitingForActionIds(string userFullQualify);
        public Task<List<WFEGetCurentEntitiesForUserDto>> GetAllWaitingForAction(string userFullQualify);
        public Task<List<Guid>> GetAllUserCanSeenIds(string userFullQualify);
        public Task<List<WFEGetCurentEntitiesForUserDto>> GetAllUserCanSeen(string userFullQualify);
        public Task<List<Guid>> GetAllUserCanSeenIdsInInvoice(string userFullQualify);
        public Task<List<WFEGetCurentEntitiesForUserDto>> GetAcceptableActionsForEntity(string userFullQualify);

        public Task<(string message,bool isSucess)> SendContract(List<Guid> contractIds, string userFullQualify);
        public Task<(string message, bool isSucess)> ApproveContract(Guid contractId, string userFullQualify);
        public Task<(string message, bool isSucess)> RejectContract(Guid contractId, string userFullQualify);
        public Task<(string message, bool isSucess)> AssignContract(Guid contractId, string userFullQualify,List<Guid> userIds);
        public Task<(string message, bool isSucess)> ReturnContract(Guid contractId, string userFullQualify);
        public Task<List<StageDetailsDto>> GetContractStageDetails(Guid contractId, string userFullQualify);
        public Task<List<Guid>> GetEntitiesAwaitingApproval(string userFullQualify);
        public Task<List<Guid>> GetEntitiesAwaitingUserAction(string userFullQualify);




    }
}
