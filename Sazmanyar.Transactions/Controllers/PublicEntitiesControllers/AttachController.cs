using ApplicationService.DtoModels.PublicEntitiesDtos;
using ApplicationService.ServicesContract.PublicEntities;
using Microsoft.AspNetCore.Mvc;

namespace Sazmanyar.Transactions.Controllers.PublicEntitiesControllers
{
    public class AttachController : BaseController
    {
        private readonly IAttachService _attachService;
        public AttachController(IAttachService attachService)
        {
            _attachService = attachService;
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] AttachDto obj)
        {
            await _attachService.Add(obj, true, obj.SectionId);
            return Ok();
        }
        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] Guid id)
        {
            var result = await _attachService.GetAll(id);
            return Ok(result);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] AttachDto obj)
        {
            await _attachService.Update(obj, true);
            return Ok();
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            await _attachService.Delete(id, true);
            return Ok();
        }
    }
}
