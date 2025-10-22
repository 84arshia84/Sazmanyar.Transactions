using ApplicationService.DtoModels.SettingDtos;
using ApplicationService.ServicesContract.Setting;
using Microsoft.AspNetCore.Mvc;

namespace Sazmanyar.Transactions.Controllers.SettingController
{
    public class FactorTypeController : BaseController
    {
        private readonly IFactorTypeService _factorTypeService;
        public FactorTypeController(IFactorTypeService factorTypeService)
        {
            _factorTypeService = factorTypeService;
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] FactorTypeDto obj)
        {
            var result = await _factorTypeService.Add(obj);
            return Ok(new { item1 = result.message, item2 = result.isSuccess });
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _factorTypeService.GetAll();
            return Ok(result);
        }
        [HttpGet("GetAllWithFactorAccessGroupEffect")]
        public async Task<IActionResult> GetAllWithFactorAccessGroupEffect([FromQuery] Guid id, [FromQuery] int property, [FromQuery] int mode)
        {
            var result = await _factorTypeService.GetAllWithAccessGroupEffect(id, property, mode);
            return Ok(result);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] FactorTypeDto obj)
        {
            var result = await _factorTypeService.Update(obj);
            return Ok(new { item1 = result.message, item2 = result.isSuccess });
        }
        [HttpPut("UpdateOfficeOnline")]
        public async Task<IActionResult> UpdateOfficeOnline([FromQuery] Guid factorTypeId, Guid filenameId)
        {
            var result = await _factorTypeService.UpdateOfficeOnline(factorTypeId, filenameId);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            var result = await _factorTypeService.Delete(id);
            return Ok(new { item1 = result.message, item2 = result.isSuccess });
        }
    }
}
