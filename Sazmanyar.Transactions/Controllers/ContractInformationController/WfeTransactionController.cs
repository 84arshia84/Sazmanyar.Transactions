using ApplicationService.DtoModels.WFEDto;
using ApplicationService.ServicesContract.WFEContract;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Sazmanyar.Transactions.Controllers.ContractInformationController
{
    public class WfeTransactionController : BaseController
    {
        private readonly IWfeTransactionExecutionRequestService _wfeTranactionService;
        public WfeTransactionController(IWfeTransactionExecutionRequestService wfeTranactionService)
        {
            _wfeTranactionService = wfeTranactionService;
        }
        [HttpGet("WaitForActionAccess")]
        public async Task<IActionResult> WaitForActionAccess()
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _wfeTranactionService.GetAllWaitingForAction(UserName);
            return Ok(result);
        }
        [HttpGet("WaitForActionAccessCount")]
        public async Task<IActionResult> WaitForActionAccessCount()
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _wfeTranactionService.GetAllWaitingForActionIds(UserName);
            return Ok(result);
        }
        [HttpPost("Send")]
        public async Task<IActionResult> Send([FromBody] List<Guid> obj)
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _wfeTranactionService.SendTransaction(obj, UserName);
            return Ok(new { message = result.message, isSuccess = result.isSucess });
        }
        [HttpPut("Approve")]
        public async Task<IActionResult> Approve([FromQuery] Guid id)
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _wfeTranactionService.ApproveTransaction(id, UserName);
            return Ok(new { message = result.message, isSuccess = result.isSucess });
        }
        [HttpPut("Reject")]
        public async Task<IActionResult> Reject([FromQuery] Guid id)
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _wfeTranactionService.RejectTransaction(id, UserName);
            return Ok(new { message = result.message, isSuccess = result.isSucess });
        }
        [HttpPost("Assign")]
        public async Task<IActionResult> Assign([FromBody] AssignUsersDto obj)
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _wfeTranactionService.AssignTransaction(obj.id, UserName, obj.users);
            return Ok(new { message = result.message, isSuccess = result.isSucess });
        }
        [HttpPut("Return")]
        public async Task<IActionResult> Return([FromQuery] Guid id)
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _wfeTranactionService.ReturnTransaction(id, UserName);
            return Ok(new { message = result.message, isSuccess = result.isSucess });
        }
        [HttpPost("AcceptableActions")]
        public async Task<IActionResult> AcceptableActions()
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _wfeTranactionService.GetAcceptableActionsForEntity(UserName);
            return Ok(result);
        }
        [HttpPost("GetAllUserCanSeen")]
        public async Task<IActionResult> GetAllUserCanSeen()
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _wfeTranactionService.GetAllUserCanSeen(UserName);
            return Ok(result);
        }
        [HttpPost("StageDetails")]
        public async Task<IActionResult> StageDetails([FromQuery] Guid id)
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _wfeTranactionService.GetTransactionStageDetails(id, UserName);
            return Ok(result);
        }
        //
        [HttpGet("GetAllWaitingForActionIds")]
        public async Task<IActionResult> GetAllWaitingForActionIds()
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _wfeTranactionService.GetEntitiesAwaitingUserAction(UserName);
            return Ok(result);
        }
        [HttpGet("GetAllWaitingForApprovalIds")]
        public async Task<IActionResult> GetAllWaitingForApprovalIds()
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _wfeTranactionService.GetEntitiesAwaitingApproval(UserName);
            return Ok(result);
        }
    }
}
