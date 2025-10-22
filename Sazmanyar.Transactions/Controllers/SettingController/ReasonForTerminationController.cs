using ApplicationService.DtoModels.SettingDtos;
using ApplicationService.ServicesContract.Setting;
using Microsoft.AspNetCore.Mvc;

namespace Sazmanyar.Transactions.Controllers.SettingController
{
    public class ReasonForTerminationController : BaseController
    {
        private readonly IReasonForTerminationService _reasonForTermination;
        public ReasonForTerminationController(IReasonForTerminationService reasonForTermination)
        {
            _reasonForTermination = reasonForTermination;
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] ReasonForTerminationDto obj)
        {
            var result = await _reasonForTermination.Add(obj);
            return Ok(result);
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _reasonForTermination.GetAll();
            return Ok(result);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] ReasonForTerminationDto obj)
        {
            var result = await _reasonForTermination.Update(obj);
            return Ok(result);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            var result = await _reasonForTermination.Delete(id);
            return Ok(result);
        }
    }
}
