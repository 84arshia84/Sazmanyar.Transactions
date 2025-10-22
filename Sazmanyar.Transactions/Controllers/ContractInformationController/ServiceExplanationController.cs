using ApplicationService.DtoModels.ContractDtos;
using ApplicationService.DtoModels.PublicEntitiesDtos;
using ApplicationService.ServicesContract.ContractInformation;
using ApplicationService.ServicesContract.InvoiceInformations;
using ApplicationService.ServicesContract.PublicEntities;
using Microsoft.AspNetCore.Mvc;

namespace Sazmanyar.Transactions.Controllers.ContractInformationController
{
    public class ServiceExplanationController : BaseController
    {
        private readonly IServiceExplanationService _serviceExplanationService;
        public ServiceExplanationController(IServiceExplanationService serviceExplanationService)
        {
            _serviceExplanationService = serviceExplanationService;
        }
        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] Guid id)
        {
            var result = await _serviceExplanationService.GetAllServiceExplanations(id);
            return Ok(result);
        }
        [HttpPost("GetAllForAddendums")]
        public async Task<IActionResult> GetAllAddendums([FromQuery] Guid contractid, [FromQuery] Guid? addendumId = null)
        {
            var result = await _serviceExplanationService.GetAllServiceExplenationForAddendum(contractid, addendumId);
            return Ok(result);
        }

        
        //[HttpPut("Update")]
        //public async Task<IActionResult> Update([FromBody] ServiceExplanationDto obj, Guid sectionId)
        //{
        //    await _serviceExplanationService.Update(obj, true, sectionId);
        //    return Ok();
        //}
    }
}
