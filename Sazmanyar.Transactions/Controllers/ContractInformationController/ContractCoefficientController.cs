using ApplicationService.ServicesContract.ContractInformation;
using Microsoft.AspNetCore.Mvc;

namespace Sazmanyar.Transactions.Controllers.ContractInformationController
{
    public class ContractCoefficientController : BaseController
    {
        private readonly IContractCoefficientService _contractCoefficient;
        public ContractCoefficientController(IContractCoefficientService contractCoefficient)
        {
            _contractCoefficient = contractCoefficient;
        }
        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] Guid id)
        {
            var result = await _contractCoefficient.GetAllCoefficient(id);
            return Ok(result);
        }
        [HttpPost("GetAllForAddendums")]
        public async Task<IActionResult> GetAllAddendums([FromQuery] Guid contractid, [FromQuery] Guid? addendumId = null)
        {
            var result = await _contractCoefficient.GetAllCoefficientForAddendum(contractid, addendumId);
            return Ok(result);
        }
    }
}
