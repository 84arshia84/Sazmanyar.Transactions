using ApplicationService.DtoModels.AccountsDtos;
using ApplicationService.DtoModels.OrganizationInformationDtos;
using ApplicationService.ServicesContract.OrganizationInformations;
using Microsoft.AspNetCore.Mvc;

namespace Sazmanyar.Transactions.Controllers.SettingController
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationInformationController : ControllerBase
    {
        private readonly IOrganizationInformationService _organizationService;
        public OrganizationInformationController(IOrganizationInformationService organizationService)
        {
            _organizationService = organizationService;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] OrganizationInformationDto obj)
        {
            var result = await _organizationService.Add(obj);
            return Ok(new { message = result.message, isSuccess = result.isSuccess,id=result.id });
        }
        [HttpGet("Get")]
        public async Task<IActionResult> Get()
        {
            var result = await _organizationService.Get();
            return Ok(result);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] OrganizationInformationDto obj)
        {
            var result = await _organizationService.Update(obj);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
        [HttpPost("AddAccount")]
        public async Task<IActionResult> AddAccount([FromBody] AccountDto obj)
        {
            var result = await _organizationService.AddAcount(obj);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
        [HttpGet("GetAccounts")]
        public async Task<IActionResult> GetAccounts()
        {
            var result = await _organizationService.GetAccounts();
            return Ok(result);
        }
        [HttpPut("UpdateAccount")]
        public async Task<IActionResult> UpdateAccount([FromBody] AccountDto obj)
        {
            var result = await _organizationService.UpdateAccount(obj);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
        [HttpDelete("DeleteAccount")]
        public async Task<IActionResult> DeleteAccount([FromQuery] Guid id)
        {
            var result = await _organizationService.DeleteAccount(id);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }

    }
}
