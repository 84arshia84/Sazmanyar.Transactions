using ApplicationService.DtoModels.SettingDtos;
using ApplicationService.ServicesContract.Setting;
using Microsoft.AspNetCore.Mvc;

namespace Sazmanyar.Transactions.Controllers.SettingController
{
    public class CorespondentLegalController : BaseController
    {
        private readonly ICorespondentLegalService _corespondentLegal;
        public CorespondentLegalController(ICorespondentLegalService corespondentLegal)
        {
            _corespondentLegal = corespondentLegal;
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] CorespondentLegalDto obj)
        {
            var result = await _corespondentLegal.Add(obj);
            return Ok(result);
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _corespondentLegal.GetAll();
            return Ok(result);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] CorespondentLegalDto obj)
        {
            var result = await _corespondentLegal.Update(obj);
            return Ok(result);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            var result = await _corespondentLegal.Delete(id);
            return Ok(result);
        }

    }
}
