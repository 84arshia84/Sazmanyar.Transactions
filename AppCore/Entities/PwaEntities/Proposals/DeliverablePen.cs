using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.PwaEntities.Proposals
{
    /// <summary>
    /// قلم قابل تحویل
    /// </summary>
    public class DeliverablePen
    {
        public Guid Id { get; set; }
        public string DeliverableName { get; set; }
        public Guid ProposalId { get; set; }
    }
}
