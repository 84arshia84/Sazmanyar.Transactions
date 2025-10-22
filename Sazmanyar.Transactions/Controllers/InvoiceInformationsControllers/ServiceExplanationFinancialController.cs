using ApplicationService.DtoModels.InvoiceDtos.ServiceExplanationFinancialsDtos;
using ApplicationService.DtoModels.UserDtos;
using ApplicationService.ServicesContract.InvoiceInformations;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Sazmanyar.Transactions.Controllers.InvoiceInformationsControllers
{
    public class ServiceExplanationFinancialController : BaseController
    {
        private readonly IServiceExplanationFinancialService _serviceExplanationFinancialService;
        public ServiceExplanationFinancialController(IServiceExplanationFinancialService serviceExplanationFinancialService)
        {
            _serviceExplanationFinancialService = serviceExplanationFinancialService;
        }
        /// <summary>
        /// GetAllContractsAndServiceExplanationFinancials
        /// </summary>
        [HttpPost("GetAllCFS")]
        public async Task<IActionResult> GetAllCFS([FromBody] GetAllCSFIdsDto getAllCSFIdsDto, [FromQuery] bool mode)
        {
            var result = await _serviceExplanationFinancialService.GetAllCFS(getAllCSFIdsDto, mode);
            return Ok(result);
        }
        [HttpPost("CUDSERequestedFinancial")]
        public async Task<IActionResult> CUDRequestedFinancial([FromBody] CUDSERequestedFinancialDto cUDSERequestedFinancialDto)
        {
            var username = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var user = new LoginUserDto();
            user.FullQualifyName = username;
            var result = await _serviceExplanationFinancialService.CUDSERequestedFinancial(cUDSERequestedFinancialDto, user);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
        [HttpPost("SetSEApprovedFinancial")]
        public async Task<IActionResult> CUDApprovedFinancial([FromBody] SetSEApprovedFinancialDto setSEApprovedFinancialDto)
        {
            var username = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var user = new LoginUserDto();
            user.FullQualifyName = username;
            var result = await _serviceExplanationFinancialService.SetSEApprovedFinancial(setSEApprovedFinancialDto, user);
            return Ok(result);
        }
    }
}
