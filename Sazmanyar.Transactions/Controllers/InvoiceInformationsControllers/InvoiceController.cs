using System.Security.Claims;
using AppCore.Entities.User;
using ApplicationService.DtoModels.ContractDtos;
using ApplicationService.DtoModels.InvoiceDtos.InvoiceBaseInformationsDtos;
using ApplicationService.DtoModels.UserDtos;
using ApplicationService.DtoModels.WFEDto;
using ApplicationService.Services.WFEService;
using ApplicationService.ServicesContract.ContractInformation;
using ApplicationService.ServicesContract.InvoiceInformations;
using ApplicationService.ServicesContract.Setting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Sazmanyar.Transactions.Controllers.InvoicesInformationsControllers
{
    public class InvoiceController : BaseController
    {
        private readonly IInvoiceBaseInformationService _invoiceBaseInformationService;
        private readonly IInvoiceTypeService _invoiceTypeService;
        private readonly IConfiguration _configuration;
        public InvoiceController(IInvoiceBaseInformationService invoiceBaseInformationService, IInvoiceTypeService invoiceTypeService, IConfiguration configuration)
        {
            _invoiceBaseInformationService = invoiceBaseInformationService;
            _invoiceTypeService = invoiceTypeService;
            _configuration = configuration;
        }
        [HttpGet("GetAllInvoiceTypes")]
        public async Task<IActionResult> GetAllInvoiceTypes()
        {
            var result = await _invoiceTypeService.GetAll();
            return Ok(result);
        }
        [HttpGet("GetAllInvoiceTypesWithAccessGroupEffect")]
        public async Task<IActionResult> GetAllInvoiceTypesWithAccessGroupEffect([FromQuery] int mode)
        {
            var username = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _invoiceTypeService.GetAllInvoiceTypesWithAccessGroupEffect(username, mode);
            return Ok(result);
        }
        [HttpPut("UpdateInvoiceType")]
        public async Task<IActionResult> UpdateInvoiceType([FromQuery] Guid id , [FromQuery] Guid officeOnlineId)
        {
            var result = await _invoiceTypeService.Update(id,officeOnlineId);
            return Ok(result);
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] InvoiceBaseInformationInsertDto obj)
        {
            var connectionString = _configuration["ConnectionStrings:DbConnection"];
            var user = new LoginUserDto();
            user.FullQualifyName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _invoiceBaseInformationService.Add(obj, user, connectionString);
            return Ok(new { message = result.message, isSuccess = result.isSuccess, invoiceBaseInformationId = result.invoiceBaseInformationId });
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] InvoiceBaseInformationInsertDto obj)
        {
            var user = new LoginUserDto();
            var result = await _invoiceBaseInformationService.Update(obj, user);
            return Ok(result);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            var user = new LoginUserDto();
            var result = await _invoiceBaseInformationService.Delete(id, user);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
        [HttpGet("Get")]
        public async Task<IActionResult> Get([FromQuery] Guid id)
        {
            var result = await _invoiceBaseInformationService.Get(id);
            return Ok(result);
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var connectionString = _configuration["ConnectionStrings:DbConnection"];
            string UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _invoiceBaseInformationService.GetAll(UserName, connectionString);
            return Ok(result);
        }
        [HttpGet("GetAllMine")]
        public async Task<IActionResult> GetAllMine()
        {
            var connectionString = _configuration["ConnectionStrings:DbConnection"];
            var user = new LoginUserDto();
            user.FullQualifyName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _invoiceBaseInformationService.GetAllMine(user.FullQualifyName, connectionString);
            return Ok(result);
        }
        [HttpGet("GetAllWaitingForAction")]
        public async Task<IActionResult> GetAllWaitingForAction()
        {
            var connectionString = _configuration["ConnectionStrings:DbConnection"];
            var user = new LoginUserDto();
            user.FullQualifyName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _invoiceBaseInformationService.GetAllWaitingForAction(user.FullQualifyName, connectionString);
            return Ok(result);
        }
        [HttpGet("GetAllWaitingForApprove")]
        public async Task<IActionResult> GetAllWaitingForApprove()
        {
            var connectionString = _configuration["ConnectionStrings:DbConnection"];
            var user = new LoginUserDto();
            user.FullQualifyName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _invoiceBaseInformationService.GetAllWaitingForApprove(user.FullQualifyName, connectionString);
            return Ok(result);
        }
        [HttpGet("GetContractCorespondentInformations")]
        public async Task<IActionResult> GetContractCorespondentInformations([FromQuery] Guid contractId)
        {
            var result = await _invoiceBaseInformationService.GetContractCorespondentInformations(contractId);
            return Ok(result);
        }
        [HttpPost("GetAllForContract")]
        public async Task<IActionResult> GetAllForContract([FromQuery] Guid id)
        {
            var connectionString = _configuration["ConnectionStrings:DbConnection"];
            var user = new LoginUserDto();
            user.FullQualifyName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _invoiceBaseInformationService.GetAllForContract(id, user.FullQualifyName, connectionString);
            return Ok(result);
        }
        [HttpGet("search/all")]
        public async Task<IActionResult> Search([FromQuery] string fullQualifyName, [FromQuery] string connectionString, [FromQuery] string? term)
        {
            var list = await _invoiceBaseInformationService.SearchAll(fullQualifyName, connectionString, term);
            return Ok(list);
        }
        [HttpGet("search/mine")]
        public async Task<IActionResult> SearchMine([FromQuery] string fullQualifyName, [FromQuery] string connectionString, [FromQuery] string? term)
        {
            var list = await _invoiceBaseInformationService.SearchMine(fullQualifyName, connectionString, term);
            return Ok(list);
        }
        [HttpGet("search/wait")]
        public async Task<IActionResult> SearchWait([FromQuery] string fullQualifyName, [FromQuery] string connectionString, [FromQuery] string? term)
        {
            var list = await _invoiceBaseInformationService.SearchWaitForAction(fullQualifyName, connectionString, term);
            return Ok(list);
        }
        //         [HttpGet("search/by-contract")]
        // public async Task<IActionResult> SearchByContract([FromQuery] Guid contractId,  [FromQuery] string fullQualifyName, [FromQuery] string connectionString, [FromQuery] string? term)
        // {
        //     var list = await _invoiceBaseInformationService.SearchByContract(contractId, fullQualifyName, connectionString, term);
        //     return Ok(list);
        // }
    }
}
