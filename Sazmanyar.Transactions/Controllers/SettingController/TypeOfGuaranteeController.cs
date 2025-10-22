using ApplicationService.DtoModels.SettingDtos;
using ApplicationService.ServicesContract.Setting;
using Microsoft.AspNetCore.Mvc;

namespace Sazmanyar.Transactions.Controllers.SettingController
{
    public class TypeOfGuaranteeController : BaseController
    {
        private readonly ITypeOfGuaranteeService _typeOfGuaranteeService;
        public TypeOfGuaranteeController(ITypeOfGuaranteeService typeOfGuaranteeService)
        {
            _typeOfGuaranteeService = typeOfGuaranteeService;
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] TypeOfGuaranteeDto obj)
        {
            var result = await _typeOfGuaranteeService.Add(obj);
            return Ok(new { message = result.message, isSuccess = result.IsSuccess });
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _typeOfGuaranteeService.GetAll();
            return Ok(result);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] TypeOfGuaranteeDto obj)
        {
            var result = await _typeOfGuaranteeService.Update(obj);
            return Ok(new { message = result.message, isSuccess = result.IsSuccess });
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            var result = await _typeOfGuaranteeService.Delete(id);
            return Ok(new { message = result.message, isSuccess = result.IsSuccess });
        }
    }
}
