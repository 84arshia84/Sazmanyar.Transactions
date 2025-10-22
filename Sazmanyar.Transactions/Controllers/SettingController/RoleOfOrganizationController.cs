using AppCore.Entities.SettingEntities.Organizationalunits;
using ApplicationService.DtoModels.SettingDtos;
using ApplicationService.ServicesContract.Setting;
using Microsoft.AspNetCore.Mvc;

namespace Sazmanyar.Transactions.Controllers.SettingController
{
    public class RoleOfOrganizationController : BaseController
    {
        private readonly IRoleOfOrganizationService _roleOfOrganizationService;
        public RoleOfOrganizationController(IRoleOfOrganizationService roleOfOrganizationService)
        {
            _roleOfOrganizationService = roleOfOrganizationService;
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] RoleOfOrganizationDto obj)
        {
            var result = await _roleOfOrganizationService.Add(obj);
            return Ok(new { item1 = result.message, item2 = result.isSuccess });
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _roleOfOrganizationService.GetAll();
            return Ok(result);
        }
        [HttpGet("GetAllWithAccessGroupEffect")]
        public async Task<IActionResult> GetAllWithAccessGroupEffect([FromQuery] Guid id, [FromQuery] int part, [FromQuery] int property, [FromQuery] int mode)
        {
            var result = await _roleOfOrganizationService.GetAllWithAccessGroupEffect(id, part, property, mode);
            return Ok(result);
        }
        [HttpGet("GetAllWithFactorAccessGroupEffect")]
        public async Task<IActionResult> GetAllWithFactorAccessGroupEffect([FromQuery] Guid id, [FromQuery] int property, [FromQuery] int mode)
        {
            var result = await _roleOfOrganizationService.GetAllWithFactorAccessGroupEffect(id, property, mode);
            return Ok(result);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] RoleOfOrganizationDto obj)
        {
            var result = await _roleOfOrganizationService.Update(obj);
            return Ok(new { item1 = result.message, item2 = result.isSuccess });
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            var result = await _roleOfOrganizationService.Delete(id);
            return Ok(new { item1 = result.message, item2 = result.isSuccess });
        }
    }
}
