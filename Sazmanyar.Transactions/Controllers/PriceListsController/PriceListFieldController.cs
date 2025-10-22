using ApplicationService.DtoModels.PriceListsDtos;
using ApplicationService.ServicesContract.PriceLists;
using Microsoft.AspNetCore.Mvc;

namespace Sazmanyar.Transactions.Controllers.PriceListsController
{
    public class PriceListFieldController : BaseController
    {
        private readonly IPriceListFieldService _priceListFieldService;
        public PriceListFieldController(IPriceListFieldService priceListFieldService)
        {
            _priceListFieldService = priceListFieldService;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()

        {
            var result = await _priceListFieldService.GetAll();
            return Ok(result);
        }
        [HttpPost("GetAllByYear")]
        public async Task<IActionResult> GetAllByYear([FromQuery] Guid id)
        {
            var result = await _priceListFieldService.GetAllByYearId(id);
            return Ok(result);
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] PriceListFieldDto obj)

        {
            var result = await _priceListFieldService.Add(obj);
            return Ok(new { ID = result.ID, isSuccess = result.isSecces });
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] PriceListFieldDto obj)

        {
            var result = await _priceListFieldService.Update(obj);
            return Ok(new { message = result.message, isSuccess = result.isSecces });
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)

        {
            var result = await _priceListFieldService.Delete(id);
            return Ok(new { message = result.message, isSuccess = result.isSecces });
        }
    }
}
