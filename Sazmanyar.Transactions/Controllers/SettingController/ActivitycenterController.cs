using ApplicationService.DtoModels.SettingDtos;
using AppCore.Entities.SettingEntities.Activitycenters;
using ApplicationService.ServicesContract.Setting;
using Microsoft.AspNetCore.Mvc;

namespace Sazmanyar.Transactions.Controllers.SettingController
{
    public class ActivitycenterController : BaseController
    {
        private readonly IActivitycenterService _activitycenterService;
        public ActivitycenterController(IActivitycenterService activitycenterService)
        {
            _activitycenterService = activitycenterService;
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] ActivitycenterDto obj)
        {
            var result =await _activitycenterService.Add(obj);
            return Ok(result);
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _activitycenterService.GetAll();
            return Ok(result);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] ActivitycenterDto obj)
        {
            var result = await _activitycenterService.Update(obj);
            return Ok(result);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            var result = await _activitycenterService.Delete(id);
            return Ok(result);
        }

    }
}
