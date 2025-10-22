using ApplicationService.DtoModels.InvoiceDtos.EstimatedMeterFinancialsDtos;
using ApplicationService.DtoModels.InvoiceDtos.ServiceExplanationFinancialsDtos;
using ApplicationService.ServicesContract.InvoiceInformations;
using Microsoft.AspNetCore.Mvc;

namespace Sazmanyar.Transactions.Controllers.InvoiceInformationsControllers
{
    public class EstimatedMeterFinancialController : BaseController
    {
        private readonly IEstimatedMeterFinancialService _estimatedMeterFinancialService;
        public EstimatedMeterFinancialController(IEstimatedMeterFinancialService estimatedMeterFinancialService)
        {
            _estimatedMeterFinancialService = estimatedMeterFinancialService;
        }
        /// <summary>
        /// GetAll EstimatedMeterFinancial and ContractEstimatedMeters
        /// </summary>
        [HttpPost("GetAllEMF")]
        public async Task<IActionResult> GetAllEMF([FromBody] GetAllEMFIdsDto getAllEMFIdsDto)
        {
            var result = await _estimatedMeterFinancialService.GetAllEMF(getAllEMFIdsDto);
            return Ok(result);
        }
    }
}
