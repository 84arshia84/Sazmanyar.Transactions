using ApplicationService.DtoModels.PwaDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.Pwa
{
    public interface IProposalService
    {
        public Task<List<ProposalDto>> GetAllProposals();
        public Task<List<DeliverablePenDto>> GetAllDeliverablePens();
    }
}
