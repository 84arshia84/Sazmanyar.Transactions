using ApplicationService.DtoModels.SettingDtos;
using ApplicationService.ServicesContract.Setting;
using Microsoft.AspNetCore.Mvc;

namespace Sazmanyar.Transactions.Controllers.SettingController
{
    public class ForGuaranteeController : BaseController
    {
        private readonly IForGuaranteeService _forGuarantee;
        public ForGuaranteeController(IForGuaranteeService forGuarantee)
        {
            _forGuarantee = forGuarantee;
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] ForGuaranteeDto obj)
        {
            var result = await _forGuarantee.Add(obj);
            return Ok(result);
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _forGuarantee.GetAll();
            return Ok(result);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] ForGuaranteeDto obj)
        {
            var result = await _forGuarantee.Update(obj);
            return Ok(result);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            var result = await _forGuarantee.Delete(id);
            return Ok(result);
        }
    }
}
