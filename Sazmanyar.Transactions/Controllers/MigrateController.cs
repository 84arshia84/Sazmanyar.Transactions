using ApplicationService.DtoModels.InvoiceDtos.InvoiceAccessGroupsDtos;
using ApplicationService.ServicesContract.InvoiceInformations;
using Microsoft.AspNetCore.Mvc;

namespace Sazmanyar.Transactions.Controllers
{
    public class MigrateController : BaseController
    {
        private readonly IAccountService _invoiceAccessGroupService;
        public MigrateController(IAccountService invoiceAccessGroupService)
        {
            _invoiceAccessGroupService = invoiceAccessGroupService;
        }
        [HttpPost("migrate")]
        public async Task<IActionResult> migrate()
        {
            await _invoiceAccessGroupService.migrateData();
            return Ok();
        }
    }
}
