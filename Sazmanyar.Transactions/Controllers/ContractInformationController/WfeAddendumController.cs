using ApplicationService.DtoModels.WFEDto;
using ApplicationService.ServicesContract.WFEContract;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Sazmanyar.Transactions.Controllers.ContractInformationController
{
    public class WfeAddendumController : BaseController
    {
        private readonly IWfeContractAddendumService _wfeContractAddendumService;
        public WfeAddendumController(IWfeContractAddendumService wfeContractAddendumService)
        {
            _wfeContractAddendumService = wfeContractAddendumService;
        }
        [HttpGet("WaitForActionAccess")]
        public async Task<IActionResult> WaitForActionAccess()
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _wfeContractAddendumService.GetAllWaitingForAction(UserName);
            return Ok(result);
        }
        [HttpGet("WaitForActionAccessCount")]
        public async Task<IActionResult> WaitForActionAccessCount()
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _wfeContractAddendumService.GetAllWaitingForActionIds(UserName);
            return Ok(result);
        }
        [HttpPost("Send")]
        public async Task<IActionResult> Send([FromBody] List<Guid> obj)
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _wfeContractAddendumService.SendAddendumContract(obj, UserName);
            return Ok(new { message = result.message, isSuccess = result.isSucess });
        }
        [HttpPut("Approve")]
        public async Task<IActionResult> Approve([FromQuery] Guid id)
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _wfeContractAddendumService.ApproveAddendumContract(id, UserName);
            return Ok(new { message = result.message, isSuccess = result.isSucess });
        }
        [HttpPut("Reject")]
        public async Task<IActionResult> Reject([FromQuery] Guid id)
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _wfeContractAddendumService.RejectAddendumContract(id, UserName);
            return Ok(new { message = result.message, isSuccess = result.isSucess });
        }
        [HttpPost("Assign")]
        public async Task<IActionResult> Assign([FromBody] AssignUsersDto obj)
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _wfeContractAddendumService.AssignAddendumContract(obj.id, UserName, obj.users);
            return Ok(new { message = result.message, isSuccess = result.isSucess });
        }
        [HttpPut("Return")]
        public async Task<IActionResult> Return([FromQuery] Guid id)
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _wfeContractAddendumService.ReturnAddendumContract(id, UserName);
            return Ok(new { message = result.message, isSuccess = result.isSucess });
        }
        [HttpPost("AcceptableActions")]
        public async Task<IActionResult> AcceptableActions()
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _wfeContractAddendumService.GetAcceptableActionsForEntity(UserName);
            return Ok(result);
        }
        [HttpPost("GetAllUserCanSeen")]
        public async Task<IActionResult> GetAllUserCanSeen()
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _wfeContractAddendumService.GetAllUserCanSeen(UserName);
            return Ok(result);
        }
        [HttpPost("StageDetails")]
        public async Task<IActionResult> StageDetails([FromQuery] Guid id)
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _wfeContractAddendumService.GetAddendumContractStageDetails(id, UserName);
            return Ok(result);
        }
        [HttpGet("GetAllWaitingForActionIds")]
        public async Task<IActionResult> GetAllWaitingForActionIds()
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _wfeContractAddendumService.GetEntitiesAwaitingUserAction(UserName);
            return Ok(result);
        }
        [HttpGet("GetAllWaitingForApprovalIds")]
        public async Task<IActionResult> GetAllWaitingForApprovalIds()
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _wfeContractAddendumService.GetEntitiesAwaitingApproval(UserName);
            return Ok(result);
        }
    }
}
