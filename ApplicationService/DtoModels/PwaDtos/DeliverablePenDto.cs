using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.PwaDtos
{
    public class DeliverablePenDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid ProposalId { get; set; }
    }
}
