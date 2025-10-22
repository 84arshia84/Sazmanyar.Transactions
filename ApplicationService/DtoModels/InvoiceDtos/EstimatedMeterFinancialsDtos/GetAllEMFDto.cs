using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.InvoiceDtos.EstimatedMeterFinancialsDtos
{
    public class GetAllEMFDto
    {
        public Guid Key { get; set; }
        public long Row { get; set; }
        public string EstimatedMeterType { get; set; }
        public Guid YearId { get; set; }
        public string Year { get; set; }
        public Guid FieldId { get; set; }
        public string Field { get; set; }
        /// <summary>
        /// شماره ردیف
        /// </summary>
        public string RowNumber { get; set; }
        public string ServiceExplanationTitle { get; set; }
        public Guid? ProjectId { get; set; }
        public string? ProjectName { get; set; } = string.Empty;
        public Guid? ProposalId { get; set; }
        public string? ProposalName { get; set; } = string.Empty;
        public Guid? ActivityCenterId { get; set; }
        public string? ActivityCenterTitle { get; set; }
        public Guid CurrencyId { get; set; }
        public string CurrencyTitle { get; set; }
        /// <summary>
        /// بهای واحد
        /// </summary>
        public decimal? UnitPrice { get; set; }
        /// <summary>
        /// واحد
        /// </summary>
        public string Unit { get; set; }
        /// <summary>
        /// مقدار
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// بهای کل خام
        /// </summary>
        public decimal RawPrice { get; set; }
        /// <summary>
        /// شرح ضرایب
        /// </summary>
        public string? CoefficientTitle { get; set; }
        /// <summary>
        /// حاصل جبری ضرایب
        /// </summary>
        public decimal SumOfCoefficients { get; set; }
        /// <summary>
        /// بها با ضریب
        /// </summary>
        public decimal RowPrice { get; set; }
        public string? Description { get; set; }
        /// <summary>
        /// جمع حجمی صورت وضعیت شده تاکنون
        /// </summary>
        public decimal VolumeSummation { get; set; }
        /// <summary>
        /// جمع درصدی صورت وضعیت شده تاکنون
        /// </summary>
        public decimal PercentSummation { get; set; }
        /// <summary>
        /// حجم طبق مرکز پروژه
        /// </summary>
        public decimal ProjectCenterPercent { get; set; }
        public decimal? RequestedVolume { get; set; }
        public decimal RequestedPercent { get; set; }
        public decimal RequestedPrice { get; set; }
        public decimal? ApprovedVolume { get; set; }
        public decimal ApprovedPercent { get; set; }
        public decimal ApprovedPrice { get; set; }
        public bool IsFinancial { get; set; }
    }
}
