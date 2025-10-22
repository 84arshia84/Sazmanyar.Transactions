using AppCore.Entities.ContractsInformation.TransactionExecutionRequests;
using ApplicationService.ServicesContract.ContractInformation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Sazmanyar.Transactions.Controllers.TransactionAndContractControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionAndContract : ControllerBase
    {
        private readonly ITransactionExecutionRequestService _transService;
        private readonly IContractService _contractService;
        public TransactionAndContract(ITransactionExecutionRequestService transService, IContractService contractService)
        {
            _transService = transService;
            _contractService = contractService;
        }
        [HttpGet("Get")]
        public async Task<IActionResult> GetTransactionAndContract(Guid transactionId, Guid contractId)
        {
            await _transService.Get(transactionId);
            await _contractService.Get(contractId);
            return Ok();
        }
    }
}
