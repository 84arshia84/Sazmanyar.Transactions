using ApplicationService.DtoModels.ContractDtos;
using ApplicationService.DtoModels.FactorDtos.FactorNettingProcessItem;
using ApplicationService.DtoModels.UserDtos;
using ApplicationService.ServicesContract.ContractInformation;
using ApplicationService.ServicesContract.FactorInformation;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Sazmanyar.Transactions.Controllers.FactorInformationControllers
{
    public class FactorNettingProcessItemController : BaseController
    {
        private readonly IFactorNettingProcessItemService _factorNettingProcessItemService;
        public FactorNettingProcessItemController(IFactorNettingProcessItemService factorNettingProcessItemService)
        {
            _factorNettingProcessItemService = factorNettingProcessItemService;
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] FactorNettingProcessItemAddDto obj)
        {
            string UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _factorNettingProcessItemService.Add(obj, UserName);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] Guid factorId)
        {
            var result = await _factorNettingProcessItemService.GetAll(factorId);
            return Ok(new { nettingItems = result.factorNettingProcessesItems, nettedValue = result.netted });
        }

        [HttpPost("Get")]
        public async Task<IActionResult> Get([FromQuery] Guid id)
        {
            var result = await _factorNettingProcessItemService.Get(id);
            return Ok(result);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] FactorNettingProcessItemUpdateDto obj)
        {
            var result = await _factorNettingProcessItemService.Update(obj);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _factorNettingProcessItemService.Delete(id, UserName);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
    }
}
