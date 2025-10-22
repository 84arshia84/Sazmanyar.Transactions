using ApplicationService.DtoModels.ContractDtos;
using ApplicationService.DtoModels.SettingDtos;
using ApplicationService.DtoModels.UserDtos;
using ApplicationService.ServicesContract.ContractInformation;
using ApplicationService.ServicesContract.Setting;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Sazmanyar.Transactions.Controllers.ContractInformationController
{
    public class AddendumController : BaseController
    {
        private readonly IContractAddendumService _contractAddendumService;
        public AddendumController(IContractAddendumService contractAddendumService)
        {
            _contractAddendumService = contractAddendumService;
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] ContractAddendumDto obj)
        {
            string UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _contractAddendumService.Add(obj, UserName);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] int situation)
        {
            string UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _contractAddendumService.GetAll(UserName,situation);
            return Ok(result);
        }
        
        [HttpPost("GetAllById")]
        public async Task<IActionResult> GetAllById([FromQuery] Guid id)
        {
            var result = await _contractAddendumService.GetAll(id);
            return Ok(result);
        }
        [HttpPost("GetAllForContract")]
        public async Task<IActionResult> GetAllForContract([FromQuery] Guid id)
        {
            string UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _contractAddendumService.GetAll(UserName,id);
            return Ok(result);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] ContractAddendumDto obj)
        {
            string UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var user = new LoginUserDto();
            user.FullQualifyName = UserName;
            var result = await _contractAddendumService.Update(obj, user);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _contractAddendumService.Delete(id, UserName);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
    }
}
