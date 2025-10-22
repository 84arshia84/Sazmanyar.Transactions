using AppCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.ContractDtos
{
    public class ExecutionRequestServiceExplanationDto
    {
        public Guid key { get; set; }
        public string Title { get; set; }
        public int ActivityReference { get; set; }
        public Guid? ProjectID { get; set; }
        public string? ProjectName { get; set; } = string.Empty;
        public Guid? ProposalID { get; set; }
        public string? ProposalName { get; set; } = string.Empty;
        public decimal? UnitAmount { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? AccelerationRate { get; set; }
        public decimal PrepaymentPercentage { get; set; }
        public DateTime? ExplanationStartingDate { get; set; }
        public DateTime? ExplanationEndingDate { get; set; }
        public int ExplanationType { get; set; }
        public decimal? ProgramVolume { get; set; }
        public Guid? DeliverablePen { get; set; }
        public long Order { get; set; }
        public bool IsSupplyList { get; set; }
        public Guid? CommodityId { get; set; }
        public string? CommodityName { get; set; }
        public Guid? SupplyListId { get; set; }
        public string? SupplyListName { get; set; }
        public string? ActivityCenterTitle { get; set; }
        public Guid? ActivityCenterID { get; set; }
        public Guid TransactionExecutionRequestId { get; set; }
        public Guid? CurrencyID { get; set; }
        public Guid? FinePaymentMethodID { get; set; }
        public Guid? UnitOfMeasurementID { get; set; }
        public bool?IsDeleted { get; set; }
        public bool?IsUpdated { get; set; }
        public long Row { get; set; }

    }
}
