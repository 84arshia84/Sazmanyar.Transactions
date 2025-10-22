using ApplicationService.DtoModels.SettingDtos;
using ApplicationService.ServicesContract.Setting;
using Microsoft.AspNetCore.Mvc;

namespace Sazmanyar.Transactions.Controllers.SettingController
{
    public class TypeOfCooperationController : BaseController
    {
        private readonly ITypeOfCooperationService _typeOfCooperation;
        public TypeOfCooperationController(ITypeOfCooperationService typeOfCooperation)
        {
            _typeOfCooperation = typeOfCooperation;
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] TypeOfCooperationDto obj)
        {
            var result = await _typeOfCooperation.Add(obj);
            return Ok(result);
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _typeOfCooperation.GetAll();
            return Ok(result);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] TypeOfCooperationDto obj)
        {
            var result = await _typeOfCooperation.Update(obj);
            return Ok(result);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            var result = await _typeOfCooperation.Delete(id);
            return Ok(result);
        }
    }
}
