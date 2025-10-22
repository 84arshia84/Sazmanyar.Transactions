using ApplicationService.DtoModels.SettingDtos;
using ApplicationService.ServicesContract.Setting;
using Microsoft.AspNetCore.Mvc;

namespace Sazmanyar.Transactions.Controllers.SettingController
{
    public class CorespondentRealController : BaseController
    {
        private readonly ICorespondentRealService _corespondentReal;
        public CorespondentRealController(ICorespondentRealService corespondentReal)
        {
            _corespondentReal = corespondentReal;
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] CorespondentRealDto obj)
        {
            var result = await _corespondentReal.Add(obj);
            return Ok(result);
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _corespondentReal.GetAll();
            return Ok(result);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] CorespondentRealDto obj)
        {
            var result = await _corespondentReal.Update(obj);
            return Ok(result);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            var result = await _corespondentReal.Delete(id);
            return Ok(result);
        }

    }
}
