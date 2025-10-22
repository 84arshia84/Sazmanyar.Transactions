using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.WFEDto
{
    public class WFEApproveOrRejectDto
    {
        public Guid EntityId { get; set; }
        public int Operation { get; set; }
        public Guid? SelectedStageIdForReject { get; set; }
    }
}
