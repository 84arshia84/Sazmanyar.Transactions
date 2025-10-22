using ApplicationService.ServicesContract.Setting;
using Microsoft.AspNetCore.Mvc;

namespace Sazmanyar.Transactions.Controllers.SettingController
{
    public class StageController : BaseController
    {
        private readonly IStageService _stageService;
        private readonly IConfiguration _config;
        public StageController(IStageService stageService, IConfiguration config)
        {
            _stageService = stageService;
            _config = config;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var connectionString = _config["ConnectionStrings:DbConnection"];
            var moduleId = Guid.Parse(_config["WorkFlowSettings:InvoiceModuleId"]);
            var result = await _stageService.GetAll(moduleId, connectionString);
            return Ok(result);
        }
    }
}
