using ApplicationService.DtoModels.SettingDtos;
using ApplicationService.ServicesContract.Setting;
using Microsoft.AspNetCore.Mvc;

namespace Sazmanyar.Transactions.Controllers.SettingController
{
    public class AddendumTypeController : BaseController
    {
        private readonly IAddendumTypeService _addendumtypeService;
        public AddendumTypeController(IAddendumTypeService addendumTypeService)
        {
            _addendumtypeService = addendumTypeService;
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] AddendumTypeDto obj)
        {
            var result = await _addendumtypeService.Add(obj);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _addendumtypeService.GetAll();
            return Ok(result);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] AddendumTypeDto obj)
        {
            var result = await _addendumtypeService.Update(obj);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            var result = await _addendumtypeService.Delete(id);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
    }
}
