using ApplicationService.DtoModels.SettingDtos;
using ApplicationService.ServicesContract.Setting;
using Microsoft.AspNetCore.Mvc;

namespace Sazmanyar.Transactions.Controllers.SettingController
{
    public class UnitOfMeasurementController : BaseController
    {
        private readonly IUnitOfMeasurementService _unitOfMeasurementService;
        public UnitOfMeasurementController(IUnitOfMeasurementService unitOfMeasurementService)
        {
            _unitOfMeasurementService = unitOfMeasurementService;
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] UnitOfMeasurementDto obj)
        {
            var result = await _unitOfMeasurementService.Add(obj);
            return Ok(new { item1 = result.message, item2 = result.isSuccss });
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _unitOfMeasurementService.GetAll();
            return Ok(result);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] UnitOfMeasurementDto obj)
        {
            var result = await _unitOfMeasurementService.Update(obj);
            return Ok(new { item1 = result.message, item2 = result.isSuccss });
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            var result = await _unitOfMeasurementService.Delete(id);
            return Ok(new { item1 = result.message, item2 = result.isSuccss });
        }
    }
}
