using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.WFEDto
{
    public class GetFirstStageDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public int StageOrder { get; set; }

        public bool IsFirst { get; set; }

        public bool IsLast { get; set; }

        public bool IsEditableByStageUsers { get; set; }

        public Guid ModuleId { get; set; }

        public bool ApproveIsMax { get; set; }

        public bool RejectIsMax { get; set; }

        public int RejectType { get; set; }

        public bool CanAssign { get; set; }

        public bool IsEditableByAssignedUsers { get; set; }

        public bool AlertByEmail { get; set; }

        public bool AlertBySMS { get; set; }

        public bool JumpFromHere { get; set; }

        public bool IsStampRequired { get; set; }

        public bool IsSignatureRequired { get; set; }

        public bool IsDefault { get; set; }

        public bool IsUsersSelectiveForNextStage { get; set; }
    }
}
