using ApplicationService.ServicesContract.ContractInformation;
using Microsoft.AspNetCore.Mvc;

namespace Sazmanyar.Transactions.Controllers.ContractInformationController
{
    public class ContractGauranteeController : BaseController
    {
        private readonly IContractGuaranteeService _contractGuaranteeService;
        public ContractGauranteeController(IContractGuaranteeService contractGuaranteeService)
        {
            _contractGuaranteeService = contractGuaranteeService;
        }
        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] Guid id)
        {
            var result = await _contractGuaranteeService.GetAll(id);
            return Ok(result);
        }
    }
}
