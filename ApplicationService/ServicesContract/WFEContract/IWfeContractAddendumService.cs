using ApplicationService.DtoModels.WFEDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.WFEContract
{
    public interface IWfeContractAddendumService
    {
        public Task<GetFirstStageDto> GetFirstStage(string userFullQualify);
        public Task InsertDefaultApprovers(string userFullQualify, Guid contractId);
        public Task<List<Guid>> GetAllWaitingForActionIds(string userFullQualify);
        public Task<List<WFEGetCurentEntitiesForUserDto>> GetAllWaitingForAction(string userFullQualify);
        public Task<List<Guid>> GetAllUserCanSeenIds(string userFullQualify);
        public Task<List<WFEGetCurentEntitiesForUserDto>> GetAllUserCanSeen(string userFullQualify);
        public Task<List<WFEGetCurentEntitiesForUserDto>> GetAcceptableActionsForEntity(string userFullQualify);
        public Task<(string message, bool isSucess)> SendAddendumContract(List<Guid> contractIds, string userFullQualify);
        public Task<(string message, bool isSucess)> ApproveAddendumContract(Guid contractId, string userFullQualify);
        public Task<(string message, bool isSucess)> RejectAddendumContract(Guid contractId, string userFullQualify);
        public Task<(string message, bool isSucess)> AssignAddendumContract(Guid contractId, string userFullQualify, List<Guid> userIds);
        public Task<(string message, bool isSucess)> ReturnAddendumContract(Guid contractId, string userFullQualify);
        public Task<List<StageDetailsDto>> GetAddendumContractStageDetails(Guid contractId, string userFullQualify);
        public Task<List<Guid>> GetEntitiesAwaitingApproval(string userFullQualify);
        public Task<List<Guid>> GetEntitiesAwaitingUserAction(string userFullQualify);
    }
}
