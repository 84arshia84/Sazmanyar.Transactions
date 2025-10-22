using ApplicationService.ServicesContract.ContractInformation;
using Microsoft.AspNetCore.Mvc;

namespace Sazmanyar.Transactions.Controllers.ContractInformationController
{
    public class ContractEstimatedMeterController : BaseController
    {
        private readonly IContractEstimatedmeterService _contractEstimatedmeterService;
        public ContractEstimatedMeterController(IContractEstimatedmeterService contractEstimatedmeterService)
        {
            _contractEstimatedmeterService = contractEstimatedmeterService;
        }
        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] Guid id)
        {
            var result = await _contractEstimatedmeterService.GetAll(id);
            return Ok(result);
        }
        [HttpPost("GetAllForAddendums")]
        public async Task<IActionResult> GetAllAddendums([FromQuery] Guid contractid, [FromQuery] Guid? addendumId = null)
        {
            var result = await _contractEstimatedmeterService.GetAllForAddendum(contractid, addendumId);
            return Ok(result);
        }
    }
}
