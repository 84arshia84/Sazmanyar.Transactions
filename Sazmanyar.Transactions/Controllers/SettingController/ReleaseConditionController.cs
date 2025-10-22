using ApplicationService.DtoModels.SettingDtos;
using AppCore.Entities.SettingEntities.ReleaseConditions;
using ApplicationService.ServicesContract.Setting;
using Microsoft.AspNetCore.Mvc;

namespace Sazmanyar.Transactions.Controllers.SettingController
{
    public class ReleaseConditionController : BaseController
    {
        private readonly IReleaseConditionService _releaseCondition;
        public ReleaseConditionController(IReleaseConditionService releaseCondition)
        {
            _releaseCondition = releaseCondition;
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] ReleaseConditionDto obj)
        {
            var result = await _releaseCondition.Add(obj);
            return Ok(result);
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _releaseCondition.GetAll();
            return Ok(result);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] ReleaseConditionDto obj)
        {
            var result = await _releaseCondition.Update(obj);
            return Ok(result);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            var result = await _releaseCondition.Delete(id);
            return Ok(result);
        }
    }
}
