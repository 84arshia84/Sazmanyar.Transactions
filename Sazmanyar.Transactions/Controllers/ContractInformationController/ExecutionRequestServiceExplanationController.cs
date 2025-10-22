using ApplicationService.ServicesContract.ContractInformation;
using Microsoft.AspNetCore.Mvc;

namespace Sazmanyar.Transactions.Controllers.ContractInformationController
{
    public class ExecutionRequestServiceExplanationController : BaseController
    {
        private readonly IExecutionRequestServiceExplanationService _serviceExplanationService;
        public ExecutionRequestServiceExplanationController(IExecutionRequestServiceExplanationService serviceExplanationService)
        {
            _serviceExplanationService = serviceExplanationService;
        }
        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] Guid id)
        {
            var result = await _serviceExplanationService.GetAllServiceExplanations(id);
            return Ok(result);
        }
        [HttpPost("GetAllApproved")]
        public async Task<IActionResult> GetAllWithTransactionApproved([FromQuery] Guid id)
        {
            var result = await _serviceExplanationService.GetAllWithTransactionIsFinalApproved(id);
            return Ok(result);
        }
    }
}
