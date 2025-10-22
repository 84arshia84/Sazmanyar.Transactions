using ApplicationService.ServicesContract.ContractInformation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Sazmanyar.Transactions.Controllers.ContractToWareHouseControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractToWareHouseController : ControllerBase
    {
        private readonly IContractService _contractService;
        private readonly IExecutionRequestServiceExplanationService _executionRequestService;
        private readonly IServiceExplanationService _serviceExplanationService;
   
        public ContractToWareHouseController(IContractService contractService, 
            IExecutionRequestServiceExplanationService executionRequestService,
            IServiceExplanationService serviceExplanationService)
        {
            _contractService = contractService;
            _executionRequestService = executionRequestService;
            _serviceExplanationService = serviceExplanationService;
        }
        [AllowAnonymous]
        [HttpGet("Contracts")]
        public async Task<IActionResult> GetAllContracts()
        {
            //var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _contractService.GetAllWithIsForTransaction();
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpGet("ServiceExplanations")]
        public async Task<IActionResult> GetServiceExplanationsWithComodity([FromQuery] Guid transactionId)
        {
            //var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _serviceExplanationService.GetWithIsFromExecution(transactionId);
            return Ok(result);
        }

    }
}
