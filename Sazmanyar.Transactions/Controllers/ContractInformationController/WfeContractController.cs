using ApplicationService.DtoModels.ContractDtos;
using ApplicationService.DtoModels.UserDtos;
using ApplicationService.DtoModels.WFEDto;
using ApplicationService.ServicesContract.ContractInformation;
using ApplicationService.ServicesContract.WFEContract;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text.Json;

namespace Sazmanyar.Transactions.Controllers.ContractInformationController
{
    public class WfeContractController : BaseController
    {
        private readonly IWfeContractService _wfeContractService;
        public WfeContractController(IWfeContractService wfeContractService)
        {
            _wfeContractService = wfeContractService;
        }
        [HttpGet("WaitForActionAccess")]
        public async Task<IActionResult> WaitForActionAccess()
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _wfeContractService.GetAllWaitingForAction(UserName);
            return Ok(result);
        }
        [HttpGet("WaitForActionAccessCount")]
        public async Task<IActionResult> WaitForActionAccessCount()
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _wfeContractService.GetAllWaitingForActionIds(UserName);
            return Ok(result);
        }
        [HttpPost("Send")]
        public async Task<IActionResult> Send([FromBody] List<Guid> obj)
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _wfeContractService.SendContract(obj, UserName);
            return Ok(new { message = result.message, isSuccess = result.isSucess });
        }
        [HttpPut("Approve")]
        public async Task<IActionResult> Approve([FromQuery] Guid id)
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _wfeContractService.ApproveContract(id, UserName);
            return Ok(new { message = result.message, isSuccess = result.isSucess });
        }
        [HttpPut("Reject")]
        public async Task<IActionResult> Reject([FromQuery] Guid id)
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _wfeContractService.RejectContract(id, UserName);
            return Ok(new { message = result.message, isSuccess = result.isSucess });
        }
        [HttpPost("Assign")]
        public async Task<IActionResult> Assign([FromBody] AssignUsersDto obj)
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _wfeContractService.AssignContract(obj.id, UserName, obj.users);
            return Ok(new { message = result.message, isSuccess = result.isSucess });
        }
        [HttpPut("Return")]
        public async Task<IActionResult> Return([FromQuery] Guid id)
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _wfeContractService.ReturnContract(id, UserName);
            return Ok(new { message = result.message, isSuccess = result.isSucess });
        }
        [HttpPost("AcceptableActions")]
        public async Task<IActionResult> AcceptableActions()
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _wfeContractService.GetAcceptableActionsForEntity(UserName);
            return Ok(result);
        }
        [HttpPost("GetAllUserCanSeen")]
        public async Task<IActionResult> GetAllUserCanSeen()
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _wfeContractService.GetAllUserCanSeen(UserName);
            return Ok(result);
        }
        [HttpPost("StageDetails")]
        public async Task<IActionResult> StageDetails([FromQuery] Guid id)
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _wfeContractService.GetContractStageDetails(id, UserName);
            return Ok(result);
        }
        [HttpGet("GetAllWaitingForActionIds")]
        public async Task<List<Guid>> GetAllWaitingForActionIds()
        {
            var userClaim = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var UserName = userClaim;
            var result = await _wfeContractService.GetEntitiesAwaitingUserAction(UserName);
            return result;
        }
        [HttpGet("GetAllWaitingForApprovalIds")]
        public async Task<List<Guid>> GetAllWaitingForApprovalIds()
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _wfeContractService.GetEntitiesAwaitingApproval(UserName);
            return result;

        }
    }
}
