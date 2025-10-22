
using ApplicationService.ServicesContract.History;
using Microsoft.AspNetCore.Mvc;

namespace Sazmanyar.Transactions.Controllers.HistoryController
{
 
    public class HistoryController : BaseController
    {
        private readonly IHistoryService _historyService;
        public HistoryController(IHistoryService historyService)
        {
            _historyService = historyService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] Guid entityId)
        {
            var result = await _historyService.GetAll(entityId);
            return Ok(result);
        }
    }
}
