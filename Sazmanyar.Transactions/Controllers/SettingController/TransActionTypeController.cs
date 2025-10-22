using ApplicationService.DtoModels.SettingDtos;
using ApplicationService.ServicesContract.Setting;
using Microsoft.AspNetCore.Mvc;

namespace Sazmanyar.Transactions.Controllers.SettingController
{
    public class TransActionTypeController : BaseController
    {
        private readonly ITransActionTypeService _transActionType;
        public TransActionTypeController(ITransActionTypeService transActionType)
        {
            _transActionType = transActionType;
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] TransActionTypeDto obj)
        {
            var result = await _transActionType.Add(obj);
            return Ok(result);
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _transActionType.GetAll();
            return Ok(result);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] TransActionTypeDto obj)
        {
            var result = await _transActionType.Update(obj);    
            return Ok(result);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            var result = await _transActionType.Delete(id);
            return Ok(result);
        }

    }
}
