using ApplicationService.DtoModels.SettingDtos;
using ApplicationService.ServicesContract.Setting;
using Microsoft.AspNetCore.Mvc;

namespace Sazmanyar.Transactions.Controllers.SettingController
{
    public class OrganizationalunitController : BaseController
    {
        private readonly IOrganizationalunitService _organizationalunit;
        public OrganizationalunitController(IOrganizationalunitService organizationalunit)
        {
            _organizationalunit = organizationalunit;
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] OrganizationalunitDto obj)
        {
            var result = await _organizationalunit.Add(obj);
            return Ok(result);
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _organizationalunit.GetAll();
            return Ok(result);
        }
        [HttpGet("GetAllWithAccessGroupEffect")]
        public async Task<IActionResult> GetAllWithAccessGroupEffect([FromQuery] Guid id, [FromQuery] int part, [FromQuery] int property, [FromQuery] int mode)
        {
            var result = await _organizationalunit.GetAllWithAccessGroupEffect(id, part, property, mode);
            return Ok(result);
        }
        [HttpGet("GetAllWithFactorAccessGroupEffect")]
        public async Task<IActionResult> GetAllWithFactorAccessGroupEffect([FromQuery] Guid id, [FromQuery] int property, [FromQuery] int mode)
        {
            var result = await _organizationalunit.GetAllWithFactorAccessGroupEffect(id, property, mode);
            return Ok(result);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] OrganizationalunitDto obj)
        {
            var result = await _organizationalunit.Update(obj);
            return Ok(result);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            var result = await _organizationalunit.Delete(id);
            return Ok(result);
        }
    }
}
