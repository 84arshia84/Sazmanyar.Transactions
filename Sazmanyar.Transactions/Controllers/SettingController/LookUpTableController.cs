using ApplicationService.DtoModels.SettingDtos;
using ApplicationService.ServicesContract.ExceptionHandling;
using ApplicationService.ServicesContract.Setting;
using Microsoft.AspNetCore.Mvc;

namespace Sazmanyar.Transactions.Controllers.SettingController
{
    public class LookUpTableController : BaseController
    {
        private ILookUpTableService _lookUpTableService;
        public LookUpTableController(ILookUpTableService lookUpTableService)
        {
           _lookUpTableService = lookUpTableService;
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] LookUpTableDtos obj)
        {
            var result = await _lookUpTableService.Add(obj);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _lookUpTableService.GetAll();
            return Ok(result);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] LookUpTableDtos obj)
        {
            var result = await _lookUpTableService.Update(obj);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            var result = await _lookUpTableService.Delete(id);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
        [HttpPost("AddInside")]
        public async Task<IActionResult> AddInside([FromBody] LookUpTableInsideDto obj)
        {
            var result = await _lookUpTableService.AddInside(obj);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
        [HttpGet("GetAllInside")]
        public async Task<IActionResult> GetAllInside([FromQuery] Guid id)
        {
            var result = await _lookUpTableService.GetAllInside(id);
            return Ok(result);
        }
        [HttpPost("GetAllInsides")]
        public async Task<IActionResult> GetAllInsides([FromBody] Guid id)
        {
            var result = await _lookUpTableService.GetAllInside(id);
            return Ok(result);
        }
        [HttpPut("UpdateInside")]
        public async Task<IActionResult> UpdateInside([FromBody] LookUpTableInsideDto obj)
        {
            var result = await _lookUpTableService.UpdateInside(obj);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
        [HttpDelete("DeleteInside")]
        public async Task<IActionResult> DeleteInside([FromQuery] Guid id)
        {
            var result = await _lookUpTableService.DeleteInside(id);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
    }
}
