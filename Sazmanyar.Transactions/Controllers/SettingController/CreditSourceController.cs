using ApplicationService.DtoModels.SettingDtos;
using ApplicationService.ServicesContract.Setting;
using Microsoft.AspNetCore.Mvc;

namespace Sazmanyar.Transactions.Controllers.SettingController
{
    public class CreditSourceController : BaseController
    {
        private readonly ICreditSourceService _creditSource;
        public CreditSourceController(ICreditSourceService creditSource)
        {
            _creditSource = creditSource;
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] CreditSourceDto obj)
        {
            var result = await _creditSource.Add(obj);
            return Ok(result);
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _creditSource.GetAll();
            return Ok(result);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] CreditSourceDto obj)
        {
            var result = await _creditSource.Update(obj);
            return Ok(result);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            var result = await _creditSource.Delete(id);
            return Ok(result);
        }
    }
}
