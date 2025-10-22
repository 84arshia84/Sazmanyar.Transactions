using ApplicationService.DtoModels.ContractAccessGroupsDtos;
using ApplicationService.DtoModels.FactorAccessGroupDto;
using ApplicationService.ServicesContract.ContractAccessGroups;
using ApplicationService.ServicesContract.FactorAccessGroups;
using Microsoft.AspNetCore.Mvc;

namespace Sazmanyar.Transactions.Controllers.FactorAccessGroupControllers
{
    public class FactorAccessGroupController : BaseController
    {
        private readonly IFactorAccessGroupsService _factorAccessGroups;

        public FactorAccessGroupController(IFactorAccessGroupsService factorAccessGroups)
        {
            _factorAccessGroups = factorAccessGroups;
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] AllFactorAccessGroupsDto obj)
        {
            var result = await _factorAccessGroups.AddAccessGroup(obj);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _factorAccessGroups.GetAll();
            return Ok(result);
        }
        [HttpPost("Get")]
        public async Task<IActionResult> Get([FromQuery] Guid id)
        {
            var result = await _factorAccessGroups.Get(id);
            return Ok(result);
        }
        [HttpPost("GetAccessForDeleteAndEdit")]
        public async Task<IActionResult> GetAccessForDeleteAndEdit([FromQuery] Guid id, [FromQuery] Guid entityId)
        {
            var result = await _factorAccessGroups.UserAccessOnEditAndDelete(id, entityId);
            return Ok(result);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] AllFactorAccessGroupsDto obj)
        {
            var result = await _factorAccessGroups.Update(obj);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            var result = await _factorAccessGroups.Delete(id);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
    }
}
