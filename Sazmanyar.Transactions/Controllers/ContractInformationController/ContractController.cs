using ApplicationService.DtoModels.ContractDtos;
using ApplicationService.DtoModels.SettingDtos;
using ApplicationService.DtoModels.UserDtos;
using ApplicationService.ServicesContract.ContractInformation;
using ApplicationService.ServicesContract.Setting;
using ApplicationService.ServicesContract.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Sazmanyar.Transactions.Controllers.ContractInformationController
{
    public class ContractController : BaseController
    {
        private readonly IContractService _contractService;
        public ContractController(IContractService contractService)
        {
            _contractService = contractService;
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] ContractDto obj)
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var user = new LoginUserDto();
            user.FullQualifyName = UserName;
            var result = await _contractService.Add(obj, user);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] int situation)
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _contractService.GetAll(UserName, situation);
            return Ok(result);
        }
        [HttpGet("GetAllForAddendum")]
        public async Task<IActionResult> GetAllForAddendum()
        {
            string UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _contractService.GetAllForAddendum(UserName);
            return Ok(result);
        }
        [HttpGet("GetAllForInvoice")]
        public async Task<IActionResult> GetAllForInvoice()
        {
            string UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _contractService.GetAllForInvoice(UserName);
            return Ok(result);
        }
        [HttpPost("Get")]
        public async Task<IActionResult> Get([FromQuery] Guid id)
        {
            var result = await _contractService.Get(id);
            return Ok(result);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] ContractDto obj)
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var user = new LoginUserDto();
            user.FullQualifyName = UserName;
            var result = await _contractService.Update(obj, user);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
        [HttpPut("UpdateStatus")]
        public async Task<IActionResult> UpdateStatus([FromQuery] Guid contractId, [FromQuery] Guid newId)
        {
            var result = await _contractService.UpdateStatus(contractId, newId);
            return Ok(result);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id, int m)
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _contractService.Delete(id, UserName);
            return Ok(result);
        }
        [HttpPost("Send")]
        public async Task<IActionResult> Send([FromBody] ContractDto obj)
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var user = new LoginUserDto();
            user.FullQualifyName = UserName;
            var result = await _contractService.Add(obj, user);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
        [HttpPost("HasInvoice")]
        public async Task<IActionResult> HasInvoice([FromQuery] Guid id)
        {
            var result = await _contractService.HasInvoice(id);
            return Ok(result);
        }
        [HttpPost("GetLastContractAmount")]
        public async Task<IActionResult> GetLastContractAmount([FromQuery] Guid id)
        {
            var result = await _contractService.GetLastContractAmount(id);
            return Ok(result);
        }

    }
}
