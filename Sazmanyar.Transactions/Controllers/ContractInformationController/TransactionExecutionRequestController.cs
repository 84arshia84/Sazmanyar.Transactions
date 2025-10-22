using ApplicationService.DtoModels.ContractDtos;
using ApplicationService.DtoModels.UserDtos;
using ApplicationService.ServicesContract.ContractInformation;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Sazmanyar.Transactions.Controllers.ContractInformationController
{
    public class TransactionExecutionRequestController : BaseController
    {
        private readonly ITransactionExecutionRequestService _transactionService;
        public TransactionExecutionRequestController(ITransactionExecutionRequestService transactionService)
        {
            _transactionService = transactionService;
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] TransactionExecutionRequestDto obj)
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var user = new LoginUserDto();
            user.FullQualifyName = UserName;
            var result = await _transactionService.Add(obj, user);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] int situation)
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _transactionService.GetAll(UserName, situation);
            return Ok(result);
        }
        [HttpPost("Get")]
        public async Task<IActionResult> Get([FromQuery] Guid id)
        {
            var result = await _transactionService.Get(id);
            return Ok(result);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] TransactionExecutionRequestDto obj)
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var user = new LoginUserDto();
            user.FullQualifyName = UserName;
            var result = await _transactionService.Update(obj, user);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _transactionService.Delete(id, UserName);
            return Ok(result);
        }
        [HttpGet("GetAllApproved")]
        public async Task<IActionResult> GetAllApproved()
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _transactionService.GetAllApproved(UserName);
            return Ok(result);
        }
    }
}
