using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.PwaEntities.Proposals
{
    public interface IProposalRepository
    {
        public Task<List<Proposal>> GetAllProposals(string query,string connectionstring);
        public Task<List<DeliverablePen>> GetAllDeliverablePens(string query, string connectionstring);
    }
}
