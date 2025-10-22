using ApplicationService.ServicesContract.ContractInformation;
using ApplicationService.ServicesContract.FactorInformation;
using Microsoft.AspNetCore.Mvc;

namespace Sazmanyar.Transactions.Controllers.FactorInformationControllers
{
    public class FactorServiceExplanationController : BaseController
    {
        private readonly IFactorServiceExplanationService _serviceExplanationService;
        public FactorServiceExplanationController(IFactorServiceExplanationService serviceExplanationService)
        {
            _serviceExplanationService = serviceExplanationService;
        }
        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] Guid id)
        {
            var result = await _serviceExplanationService.GetAll(id);
            return Ok(result);
        }

    }
}
