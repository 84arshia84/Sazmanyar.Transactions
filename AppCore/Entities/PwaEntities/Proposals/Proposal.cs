using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.PwaEntities.Proposals
{
    /// <summary>
    /// منشور
    /// </summary>
    public class Proposal
    {
        public Guid Id { get; set; }
        /// <summary>
        /// نام منشور
        /// </summary>
        public string ProposalName { get; set; }
    }
}
