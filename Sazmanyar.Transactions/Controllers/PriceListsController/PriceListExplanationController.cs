using ApplicationService.ServicesContract.PriceLists;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text.Unicode;
using ApplicationService.DtoModels.PriceListsDtos;

namespace Sazmanyar.Transactions.Controllers.PriceListsController
{
    public class PriceListExplanationController : BaseController
    {
        private readonly IPriceListExplanationService _explanationService;
        public PriceListExplanationController(IPriceListExplanationService explanationService)
        {
            _explanationService = explanationService;
        }
        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] Guid id)

        {
            var result = await _explanationService.GetAllListExplanationByClauseID(id);
            return Ok(result);
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] PriceListExplanationsDto obj)

        {
            var result = await _explanationService.AddExplanation(obj);
            return Ok(new { ID = result.message, isSuccess = result.isSuccess });
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] PriceListExplanationsDto obj)

        {
            var result = await _explanationService.UpdateExplanation(obj);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)

        {
            var result = await _explanationService.DeleteExplanation(id);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
    }
}
