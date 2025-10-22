using ApplicationService.DtoModels.PriceListsDtos;
using ApplicationService.ServicesContract.PriceLists;
using Microsoft.AspNetCore.Mvc;

namespace Sazmanyar.Transactions.Controllers.PriceListsController
{
    public class PriceListClauseController : BaseController
    {
        private readonly IPriceListClauseService _priceListClauseService;
        public PriceListClauseController(IPriceListClauseService priceListClauseService)
        {
            _priceListClauseService = priceListClauseService;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _priceListClauseService.GetAll();
            return Ok(result);
        }
        [HttpPost("GetAllByField")]
        public async Task<IActionResult> GetAllByField([FromQuery] Guid id)
        {
            var result = await _priceListClauseService.GetAllByFieldId(id);
            return Ok(result);
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] PriceListClauseDto obj)

        {
            var result = await _priceListClauseService.Add(obj);
            return Ok(new { ID = result.ID, isSuccess = result.isSuccess });
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] PriceListClauseDto obj)

        {
            var result = await _priceListClauseService.Update(obj);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)

        {
            var result = await _priceListClauseService.Delete(id);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
    }
}
