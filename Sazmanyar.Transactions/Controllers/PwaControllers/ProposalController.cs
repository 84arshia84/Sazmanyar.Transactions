using ApplicationService.ServicesContract.Pwa;
using Microsoft.AspNetCore.Mvc;

namespace Sazmanyar.Transactions.Controllers.PwaControllers
{
    public class ProposalController : BaseController
    {
        private readonly IProposalService _proposalService;
        public ProposalController(IProposalService proposalService)
        {
            _proposalService = proposalService;
        }
        [HttpGet("GetAllProposal")]
        public async Task<IActionResult> GetAllProposals()
        {
            var result = await _proposalService.GetAllProposals();
            return Ok(result);
        }
        [HttpGet("GetAllDeliverablePen")]
        public async Task<IActionResult> GetAllDeliverablePens()
        {
            var result = await _proposalService.GetAllDeliverablePens();
            return Ok(result);
        }
    }
}
