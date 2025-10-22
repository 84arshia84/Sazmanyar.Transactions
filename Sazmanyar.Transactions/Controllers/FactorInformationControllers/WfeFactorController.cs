using ApplicationService.DtoModels.WFEDto;
using ApplicationService.ServicesContract.WFEContract;
using ApplicationService.ServicesContract.WFEFactor;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Sazmanyar.Transactions.Controllers.FactorInformationControllers
{
    public class WfeFactorController : BaseController
    {
        private readonly IWfeFactorService _wfeFactorService;
        public WfeFactorController(IWfeFactorService wfeFactorService)
        {
            _wfeFactorService = wfeFactorService;
        }
        [HttpGet("WaitForActionAccess")]
        public async Task<IActionResult> WaitForActionAccess()
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _wfeFactorService.GetAllWaitingForAction(UserName);
            return Ok(result);
        }
        [HttpPost("Send")]
        public async Task<IActionResult> Send([FromBody] List<Guid> obj)
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _wfeFactorService.SendFactor(obj, UserName);
            return Ok(new { message = result.message, isSuccess = result.isSucess });
        }
        [HttpPut("Approve")]
        public async Task<IActionResult> Approve([FromQuery] Guid id)
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _wfeFactorService.ApproveFactor(id, UserName);
            return Ok(new { message = result.message, isSuccess = result.isSucess });
        }
        [HttpPut("Reject")]
        public async Task<IActionResult> Reject([FromQuery] Guid id)
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _wfeFactorService.RejectFactor(id, UserName);
            return Ok(new { message = result.message, isSuccess = result.isSucess });
        }
        [HttpPost("Assign")]
        public async Task<IActionResult> Assign([FromBody] AssignUsersDto obj)
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _wfeFactorService.AssignFactor(obj.id, UserName, obj.users);
            return Ok(new { message = result.message, isSuccess = result.isSucess });
        }
        [HttpPut("Return")]
        public async Task<IActionResult> Return([FromQuery] Guid id)
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _wfeFactorService.ReturnFactor(id, UserName);
            return Ok(new { message = result.message, isSuccess = result.isSucess });
        }
        [HttpPost("AcceptableActions")]
        public async Task<IActionResult> AcceptableActions()
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _wfeFactorService.GetAcceptableActionsForEntity(UserName);
            return Ok(result);
        }
        [HttpPost("GetAllUserCanSeen")]
        public async Task<IActionResult> GetAllUserCanSeen()
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _wfeFactorService.GetAllUserCanSeen(UserName);
            return Ok(result);
        }
        [HttpPost("StageDetails")]
        public async Task<IActionResult> StageDetails([FromQuery] Guid id)
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _wfeFactorService.GetFactorStageDetails(id, UserName);
            return Ok(result);
        }
        [HttpGet("GetAllWaitForActionIds")]
        public async Task<IActionResult> GetAllWaitForActionIds()
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _wfeFactorService.GetEntitiesAwaitingUserAction(UserName);
            return Ok(result);
        }
        [HttpGet("GetAllWaitForApprovalIds")]
        public async Task<IActionResult> GetAllWaitForApprovalIds()
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _wfeFactorService.GetEntitiesAwaitingApproval(UserName);
            return Ok(result);
        }
    }
}
