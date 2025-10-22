using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.ContractDtos
{
    public class ServiceExplanationDto
    {
        public Guid key { get; set; }
        public string Title { get; set; }
        public int ActivityReference { get; set; }
        public Guid? ProjectID { get; set; }
        public string? ProjectName { get; set; } = string.Empty;
        public Guid? ProposalID { get; set; }
        public string? ProposalName { get; set; } = string.Empty;
        public decimal DiscountAmount { get; set; }
        [Column(TypeName = "decimal(30,5)")]
        public decimal? UnitAmount { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? programVolume { get; set; }
        public Guid? DeliverablePen {  get; set; }
        [Column(TypeName = "decimal(30,5)")]
        public decimal Price { get; set; }
        [Column(TypeName = "decimal(30,5)")] 
        public decimal? AccelerationRate { get; set; }
        [Column(TypeName = "decimal(7,5)")] 
        public decimal PrepaymentPercentage { get; set; }
        public DateTime? ExplanationStartingDate { get; set; }
        public DateTime? ExplanationEndingDate { get; set; }
        public int ExplanationType { get; set; }
        public List<EstimatedmeterDto>? estimatedmeterDtos { get; set; }
        public Guid ActivityCenterID { get; set; }
        public string? ActivityCenterTitle { get; set; }
        public Guid ContractID { get; set; }
        public Guid CurrencyID { get; set; }
        public Guid? FinePaymentMethodID { get; set; }
        public Guid? UnitOfMeasurementID { get; set; }
        public bool? IsUpdated { get; set; }
        public bool IsAddendum {  get; set; }
        public Guid? AddendumId { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? UpdatedInAddendum { get; set; }
        public long Row {  get; set; }
        public bool? IsForExecutionRequest { get; set;}
        public Guid? TransactionExecutionServiceExplanationId { get; set; }
    }
}
