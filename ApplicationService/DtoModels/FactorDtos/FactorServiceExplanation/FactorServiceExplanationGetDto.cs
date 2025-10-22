using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.FactorDtos.FactorServiceExplanation
{
    public class FactorServiceExplanationGetDto
    {
        public Guid Id { get; set; }
        public Guid FactorId { get; set; }
        public string Title { get; set; }
        public int ActivityReference { get; set; }
        public Guid? ProjectID { get; set; }
        public Guid? ProposalID { get; set; }
        public Guid? DeliverablePen { get; set; }
        public int ExplanationType { get; set; }
        public decimal? UnitAmount { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? AccelerationRate { get; set; }
        public decimal? Amount { get; set; }
        public bool IsSupplyList { get; set; }
        public Guid? CommodityId { get; set; }
        public string? CommodityName { get; set; }
        public string? CommodityCode { get; set; }
        public Guid? SupplyListId { get; set; }
        public string? SupplyListName { get; set; }
        public Guid ActivityCenterID { get; set; }
        public Guid CurrencyID { get; set; }
        public Guid? UnitOfMeasurementID { get; set; }
    }
}
