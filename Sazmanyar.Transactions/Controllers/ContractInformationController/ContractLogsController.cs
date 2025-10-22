using ApplicationService.DtoModels.ContractDtos;
using ApplicationService.ServicesContract.ContractInformation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Sazmanyar.Transactions.Controllers.ContractInformationController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractLogsController : ControllerBase
    {
        private readonly IContractLogService _service;
        public ContractLogsController(IContractLogService service)
        {
            _service = service;
        }

        // GET api/contractlogs/{contractId}
        [HttpGet("{contractId:guid}")]
        public async Task<ActionResult<List<ContractLogDto>>> GetByContractId(Guid contractId)
        {
            var result = await _service.GetLogsByContractId(contractId);
            return Ok(result);
        }
    }
}