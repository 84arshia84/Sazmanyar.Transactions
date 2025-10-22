using ApplicationService.DtoModels.FactorDtos.FactorStageRoles;
using ApplicationService.DtoModels.WFEDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.WFEFactor
{
    public interface IWfeFactorService
    {
        public Task<List<Guid>> GetAllWaitingForActionIds(string userFullQualify);
        public Task<List<Guid>> GetAllUserCanSeenIds(string userFullQualify);
        public Task<List<WFEGetCurentEntitiesForUserDto>> GetAllUserCanSeen(string userFullQualify);
        public Task<GetFirstStageDto> GetFirstStage(string userFullQualify);
        public Task InsertDefaultApprovers(string userFullQualify, Guid factorId);
        public Task<List<WFEGetCurentEntitiesForUserDto>> GetAllWaitingForAction(string userFullQualify);
        public Task<List<WFEGetCurentEntitiesForUserDto>> GetAcceptableActionsForEntity(string userFullQualify);
        public Task<(string message, bool isSucess)> SendFactor(List<Guid> factorIds, string userFullQualify);
        public Task<(string message, bool isSucess)> ApproveFactor(Guid factorId, string userFullQualify);
        public Task<(string message, bool isSucess)> RejectFactor(Guid factorId, string userFullQualify);
        public Task<(string message, bool isSucess)> AssignFactor(Guid factorId, string userFullQualify, List<Guid> userIds);
        public Task<(string message, bool isSucess)> ReturnFactor(Guid factorId, string userFullQualify);
        public Task<List<StageDetailsDto>> GetFactorStageDetails(Guid factorId, string userFullQualify);
        public Task<List<Guid>> GetEntitiesAwaitingApproval(string userFullQualify);
        public Task<List<Guid>> GetEntitiesAwaitingUserAction(string userFullQualify);


    }
}
