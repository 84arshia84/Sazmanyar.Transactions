using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.ContractsInformation.ContractLog
{
    [Table("ContractLogs", Schema = "TAM")]
    public class ContractLog
    {
        [Key]
        public Guid Id { get; set; }


        public Guid ContractId { get; set; }


        public Guid? ContractTypeID { get; set; }
        public Guid? TransActionTypeID { get; set; }
        public Guid? RoleOFOrganizationID { get; set; }
        public Guid? CorespondentID { get; set; }
        public bool? CorespondentRealOrLegal { get; set; }
        public Guid? OrganizationUnitID { get; set; }
        public Guid? CreditSourceID { get; set; }
        public Guid? TimeProfileID { get; set; }
        public Guid? FinancialDetailsID { get; set; }
        public Guid? ContractCheckListValueId { get; set; }
        public Guid? StatusId { get; set; }

        [Required]
        public string? ContractTitle { get; set; }
        [Required]
        public string?ContractNumber { get; set; }
        public Guid? ContractExpertID { get; set; }
        public string? ContractExpertName { get; set; }
        public Guid? ConsultantID { get; set; }
        public string? ConsultantName { get; set; }
        public bool? RequirementToCloseTheAccount { get; set; }
        public DateTime? InsertContractDate { get; set; }
        public Guid? InsertContractBy { get; set; }
        public Guid? DeleteBy { get; set; }
        public DateTime? DeleteDate { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? HasAddendum { get; set; }
        public Guid? CurrentStageId { get; set; }
        public string? CurrentStatusTitle { get; set; }
        public string? LastActionTitle { get; set; }
        public bool? IsFinalApprove { get; set; }


        public string UpdatedBy { get; set; }

        public DateTime UpdatedAt { get; set; }

        public string? ChangesSummary { get; set; }
    }
}
