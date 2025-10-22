using ApplicationService.DtoModels.WFEDto;
using ApplicationService.ServicesContract.InvoiceInformations;
using ApplicationService.ServicesContract.WFEContract;
using ApplicationService.ServicesContract.WFEInvoice;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Sazmanyar.Transactions.Controllers.InvoiceInformationsControllers
{
    public class WfeInvoiceController : BaseController
    {
        private readonly IWfeInvoiceService _wfeInvoice;
        private readonly IInvoiceAmountService _invoiceAmountService;
        public WfeInvoiceController(IWfeInvoiceService wfeInvoice,IInvoiceAmountService invoiceAmountService)
        {
            _wfeInvoice = wfeInvoice;
            _invoiceAmountService = invoiceAmountService;
        }
        [HttpGet("WaitForActionAccess")]
        public async Task<IActionResult> WaitForActionAccess()
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _wfeInvoice.GetAllWaitingForAction(UserName);
            return Ok(result);
        }
        [HttpPost("Send")]
        public async Task<IActionResult> Send([FromBody] List<Guid> obj)
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _wfeInvoice.SendInvoice(obj, UserName);
            await _invoiceAmountService.AddInvoiceAmount(obj[0]);
            return Ok(new { message = result.message, isSuccess = result.isSucess });
        }
        [HttpPut("Approve")]
        public async Task<IActionResult> Approve([FromQuery] Guid id, [FromQuery]  bool paymentCompleted, [FromQuery] int stage)
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _wfeInvoice.ApproveInvoice(id, UserName, paymentCompleted, stage);
            await _invoiceAmountService.UpdateApprovedAmount(id,false);
            return Ok(new { message = result.message, isSuccess = result.isSucess });
        }
        [HttpPut("Reject")]
        public async Task<IActionResult> Reject([FromQuery] Guid id)
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _wfeInvoice.RejectInvoice(id, UserName);
            await _invoiceAmountService.UpdateApprovedAmount(id, true);
            return Ok(new { message = result.message, isSuccess = result.isSucess });
        }
        [HttpPost("Assign")]
        public async Task<IActionResult> Assign([FromBody] AssignUsersDto obj)
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _wfeInvoice.AssignInvoice(obj.id, UserName, obj.users);
            return Ok(new { message = result.message, isSuccess = result.isSucess });
        }
        [HttpPut("Return")]
        public async Task<IActionResult> Return([FromQuery] Guid id)
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _wfeInvoice.ReturnInvoice(id, UserName);
            return Ok(new { message = result.message, isSuccess = result.isSucess });
        }
        [HttpPost("AcceptableActions")]
        public async Task<IActionResult> AcceptableActions()
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _wfeInvoice.GetAcceptableActionsForEntity(UserName);
            return Ok(result);
        }
        [HttpPost("StageDetails")]
        public async Task<IActionResult> StageDetails([FromQuery] Guid id)
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _wfeInvoice.GetInvoiceStageDetails(id, UserName);
            return Ok(result);
        }
        [HttpPost("WaitForMe")]
        public async Task<IActionResult> WaitForMe([FromQuery] Guid id)
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _wfeInvoice.WaitForMe(id, UserName);
            return Ok(result);
        }
        [HttpPost("InvoiceRoles")]
        public async Task<IActionResult> InvoiceRoles([FromQuery] Guid id)
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _wfeInvoice.WaitForMe(id, UserName);
            return Ok(result);
        }
        [HttpPost("InvoiceStageRoles")]
        public async Task<IActionResult> InvoiceStageRoles([FromQuery] Guid id)
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _wfeInvoice.InvoiceStageRoles(id, UserName);
            return Ok(result);
        }
        [HttpGet("GetAllWaitingForActionIds")]
        public async Task<IActionResult> GetAllWaitingForActionIds()
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _wfeInvoice.InvoiceGetEntitiesAwaitingUserAction(UserName);
            return Ok(result);
        }
        [HttpGet("GetAllWaitingForApprovalIds")]
        public async Task<IActionResult> GetAllWaitingForApprovalIds()
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _wfeInvoice.InvoiceGetEntitiesAwaitingApproval(UserName);
            return Ok(result);
        }
        [HttpGet("GetAllWaitingForApproval")]
        public async Task<IActionResult> GetAllWaitingForApproval()
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _wfeInvoice.InvoiceGetEntitiesAwaitingApprovalDto(UserName);
            return Ok(result);
        }


    }
}
