using ApplicationService.DtoModels.SettingDtos;
using ApplicationService.ServicesContract.Setting;
using Microsoft.AspNetCore.Mvc;

namespace Sazmanyar.Transactions.Controllers.SettingController
{
    public class ReasonForCancellationController : BaseController
    {
        private readonly IReasonForCancellationService _reasonForCancellation;
        public ReasonForCancellationController(IReasonForCancellationService reasonForCancellation)
        {
            _reasonForCancellation = reasonForCancellation;
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] ReasonForCancellationDto obj)
        {
            var result = await _reasonForCancellation.Add(obj);
            return Ok(result);
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _reasonForCancellation.GetAll();
            return Ok(result);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] ReasonForCancellationDto obj)
        {
            var result = await _reasonForCancellation.Update(obj);
            return Ok(result);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            var result = await _reasonForCancellation.Delete(id);
            return Ok(result);
        }
    }
}
