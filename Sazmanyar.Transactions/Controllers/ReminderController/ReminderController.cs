using AppCore.Entities.FactorInformation.Factors;
using ApplicationService.DtoModels.InvoiceDtos.EstimatedMeterFinancialsDtos;
using ApplicationService.ServicesContract.InvoiceInformations;
using ApplicationService.ServicesContract.Reminder;
using ApplicationService.ServicesContract.WFEContract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Sazmanyar.Transactions.Controllers.ReminderController
{
    public class ReminderController : BaseController
    {
        private readonly IReminderService _reminderService;
        public ReminderController(IReminderService reminderService)
        {
            _reminderService = reminderService;
        }
        [AllowAnonymous]
        [HttpGet("ContractReminder")]
        public async Task<IActionResult> ContractReminder([FromQuery] string fullqualifyName)
        {
            return Ok(await _reminderService.GetReminderContractCounts(fullqualifyName));
        }
        [AllowAnonymous]
        [HttpGet("ContractAddendumReminder")]
        public async Task<IActionResult> ContractAddendumReminder([FromQuery] string fullqualifyName)
        {
            return Ok(await _reminderService.GetReminderAddendumCounts(fullqualifyName));
        }
        [AllowAnonymous]
        [HttpGet("TransactionReminder")]
        public async Task<IActionResult> TransactionReminder([FromQuery] string fullqualifyName)
        {
            return Ok(await _reminderService.GetReminderTransactionCounts(fullqualifyName));
        }
        [AllowAnonymous]
        [HttpGet("InvoiceReminder")]
        public async Task<IActionResult> InvoiceReminder([FromQuery] string fullqualifyName)
        {
            return Ok(await _reminderService.GetReminderInvoiceCounts(fullqualifyName));
        }
        [AllowAnonymous]
        [HttpGet("FactorReminder")]
        public async Task<IActionResult> FactorReminder([FromQuery] string fullqualifyName)
        {
            return Ok(await _reminderService.GetReminderFactorCounts(fullqualifyName));
        }
    }
}
