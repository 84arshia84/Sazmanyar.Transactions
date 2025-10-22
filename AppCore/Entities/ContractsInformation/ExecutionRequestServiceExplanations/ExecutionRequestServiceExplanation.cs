using AppCore.Entities.ContractsInformation.TransactionExecutionRequests;
using AppCore.Entities.SettingEntities.Currencies;
using AppCore.Entities.SettingEntities.FinePaymentMethods;
using AppCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.ContractsInformation.ExecutionRequestServiceExplanations
{
    /// <summary>
    /// شرح خدمت درخواست برگزاری معالمه
    /// </summary>
    public class ExecutionRequestServiceExplanation
    {
        #region properties
        public Guid ID { get; set; }
        /// <summary>
        /// عنوان شرح خدمت
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        ///  مرجع فعالیت
        /// </summary>
        public ActivityReferenceEnum ActivityReference { get; set; }
        public Guid? ProjectID { get; set; }
        /// <summary>
        /// نام پروژه
        /// </summary>
        public string? ProjectName { get; set; } = string.Empty;
        public Guid? ProposalID { get; set; }
        /// <summary>
        /// نام منشور
        /// </summary>
        public string? ProposalName { get; set; } = string.Empty;
        /// <summary>
        /// مبلغ واحد
        /// </summary>
        public decimal? UnitAmount { get; set; }
        /// <summary>
        /// مبلغ کل
        /// </summary>
        public decimal? TotalAmount { get; set; }
        /// <summary>
        /// نرخ تسریع
        /// </summary>
        public decimal? AccelerationRate { get; set; }
        /// <summary>
        /// درصد پیش پرداخت
        /// </summary>
        public decimal PrepaymentPercentage { get; set; }
        /// <summary>
        /// تاریخ شروع شرح خدمت
        /// </summary>
        public DateTime? ExplanationStartingDate { get; set; }
        /// <summary>
        /// تاریخ پایان شرح خدمت
        /// </summary>
        public DateTime? ExplanationEndingDate { get; set; }
        /// <summary>
        /// نوع شرج خدمت
        /// </summary>
        public ExplanationTypeEnum ExplanationType { get; set; }
        /// <summary>
        /// حجم برنامه ای
        /// </summary>
        public decimal? ProgramVolume { get; set; }
        /// <summary>
        /// قلم قابل تحویل
        /// </summary>
        public Guid? DeliverablePen { get; set; }
        /// <summary>
        /// ترتیب شرح خدمات
        /// </summary>
        public long Order { get; set; }
        /// <summary>
        /// لیست تامینی
        /// </summary>
        public bool IsSupplyList { get; set; }
        public Guid? CommodityId { get; set; }
        /// <summary>
        /// نام کالا
        /// </summary>
        public string? CommodityName { get; set; }
        public Guid? SupplyListId {  get; set; }
        /// <summary>
        /// عنوان لیست تامین
        /// </summary>
        public string? SupplyListName { get; set; }
        /// <summary>
        /// عنوان مرکز هزینه
        /// </summary>
        public string? ActivityCenterTitle { get; set; }
        public bool? IsDeleted { get; set; }
        #endregion
        #region relation 
        public Guid? ActivityCenterID { get; set; }
        public Guid? CurrencyID { get; set; }
        public Currency? Currency { get; set; }
        public Guid? FinePaymentMethodID { get; set; }
        public FinePaymentMethod? FinePaymentMethod { get; set; }
        public Guid? UnitOfMeasurementID { get; set; }
        public Guid TransactionExecutionRequestId { get; set; }
        public TransactionExecutionRequest TransactionExecutionRequest {  get; set; } 

        #endregion
    }
}
