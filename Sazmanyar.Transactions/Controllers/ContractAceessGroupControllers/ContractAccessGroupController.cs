using ApplicationService.DtoModels.ContractAccessGroupsDtos;
using ApplicationService.DtoModels.ContractDtos;
using ApplicationService.ServicesContract.ContractAccessGroups;
using ApplicationService.ServicesContract.ContractInformation;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Sazmanyar.Transactions.Controllers.ContractAceessGroupControllers
{
    public class ContractAccessGroupController : BaseController
    {
        private readonly IContractAccessGroupsService _contractAccessGroups;

        public ContractAccessGroupController(IContractAccessGroupsService contractAccessGroups)
        {
            _contractAccessGroups = contractAccessGroups;
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] AllAccessGroupsDto obj)
        {
            var result = await _contractAccessGroups.AddAccessGroup(obj);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _contractAccessGroups.GetAll();
            return Ok(result);
        }
        [HttpPost("Get")]
        public async Task<IActionResult> Get([FromQuery] Guid id)
        {
            var result = await _contractAccessGroups.Get(id);
            return Ok(result);
        }
        [HttpPost("GetById")]
        public async Task<IActionResult> GetByUserId([FromQuery] Guid id)
        {
            var result = await _contractAccessGroups.GetContractAccessGroupsById(id);
            return Ok(result);
        }
        [HttpPost("GetAccessForDeleteAndEdit")]
        public async Task<IActionResult> GetAccessForDeleteAndEdit([FromQuery] Guid id, [FromQuery] Guid entityId, [FromQuery] int part)
        {
            var result = await _contractAccessGroups.UserAccessOnEditAndDelete(id, entityId,part);
            return Ok(result);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] AllAccessGroupsDto obj)
        {
            var result = await _contractAccessGroups.Update(obj);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            var result = await _contractAccessGroups.Delete(id);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
    }
}
