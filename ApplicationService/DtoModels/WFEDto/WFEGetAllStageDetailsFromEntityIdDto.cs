using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.WFEDto
{
    public class WFEGetAllStageDetailsFromEntityIdDto
    {
        public Guid StageId { get; set; }
        public string Title { get; set; }
        public bool IsFirst { get; set; }
        public bool IsLast { get; set; }
        public int StageOrder { get; set; }
        public bool IsFinalApproved { get; set; }
        public int WaitingStageOrder { get; set; }
        public string UserNames { get; set; }
    }
}
