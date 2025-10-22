using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.InvoiceDtos.ServiceExplanationFinancialsDtos
{
    public class GetAllCSFDto
    {
        public Guid Key { get; set; }
        public long Row { get; set; }
        public Guid ServiceExplanationId { get; set; }
        public string ServiceExplanationTitle { get; set; }
        public string ProjectOrProposalName { get; set; }
        public Guid ActivityCenterId { get; set; }
        public string ActivityCenterTitle { get; set; }
        public Guid CurrencyId { get; set; }
        public string CurrencyTitle { get; set; }
        public Guid? UnitOfMeasurementId { get; set; }
        //public string UnitOfMeasurementTitle { get; set; }
        public decimal? ProgramVolume { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? UnitAmount { get; set; }
        public decimal? TotalAmount { get; set; }
        public DateTime? ExplanationStartingDate { get; set; }
        public DateTime? ExplanationEndingDate { get; set; }
        public string VolumeSummation { get; set; }
        public string PercentSummation { get; set; }
        public string ProjectCenterPercent { get; set; }
        public decimal? RequestedVolume { get; set; }
        public decimal RequestedPercent { get; set; }
        public decimal RequestedPrice { get; set; }
        public decimal? ApprovedVolume { get; set; }
        public decimal ApprovedPercent { get; set; }
        public decimal ApprovedPrice { get; set; }
        public bool IsFinancial { get; set; }
    }
}
