using ApplicationService.DtoModels.PublicEntitiesDtos;
using ApplicationService.DtoModels.SettingDtos;
using ApplicationService.ServicesContract.PublicEntities;
using ApplicationService.ServicesContract.Setting;
using Microsoft.AspNetCore.Mvc;

namespace Sazmanyar.Transactions.Controllers.PublicEntitiesControllers
{
    public class DescriptionController : BaseController
    {
        private readonly IDescriptionService _descriptionService;
        public DescriptionController(IDescriptionService descriptionService)
        {
            _descriptionService = descriptionService;
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] DescriptionDto obj)
        {
            await _descriptionService.Add(obj,true,obj.SectionId);
            return Ok();
        }
        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] Guid id)
        {
            var result = await _descriptionService.GetAll(id);
            return Ok(result);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] DescriptionDto obj)
        {
            await _descriptionService.Update(obj,true);
            return Ok();
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            await _descriptionService.Delete(id,true);
            return Ok();
        }
    }
}
