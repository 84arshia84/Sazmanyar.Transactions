using AppCore.Entities.ContractsInformation.ContractAddendums;
using AppCore.Entities.ContractsInformation.ContractEstimatedmeters;
using AppCore.Entities.ContractsInformation.Contratcs;
using AppCore.Entities.ContractsInformation.ServiceExplanations;
using AppCore.Entities.SettingEntities.Activitycenters;
using AppCore.Entities.SettingEntities.Currencies;
using AppCore.Entities.SettingEntities.FinePaymentMethods;
using AppCore.Entities.SettingEntities.UnitOfMeasurements;
using AppCore.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.ContractsInformation.ServiceExplanations
{
    /// <summary>
    /// شرح خدمت
    /// </summary>
    [Table("ServiceExplanation", Schema = "TAM")]
    public class ServiceExplanation
    {
        #region properties
        [Key]
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
        public decimal? TotalAmount {  get; set; }
        /// <summary>
        /// مبلغ تخفیف
        /// </summary>
        public decimal DiscountAmount { get; set; }
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
        public Guid? DeliverablePen {  get; set; }
        /// <summary>
        /// ترتیب شرح خدمات
        /// </summary>
        public long Order {  get; set; }
        /// <summary>
        /// شرح خدمتی که در الحاقیه اضافه شده
        /// </summary>
        public bool IsAddendum { get; set; }
        /// <summary>
        /// شرح خدمت در الحاقیه حذف شده
        /// </summary>
        public bool? IsDeleted { get; set; }
        /// <summary>
        /// زمان حذف شرح خدمت
        /// </summary>
        public DateTime? DeleteDate { get; set; }
        /// <summary>
        /// این شرح خدمت در کدام الحاقیه حذف شده
        /// </summary>
        public Guid?  DeletedInAddedumId { get; set; }
        /// <summary>
        /// برای زمانی هست که شرح خدمتی در قرارداد ثبت شده باشد
        /// و بعد در الحاقیه ویرایش شود
        /// توی قرارداد باید شرح خدمت ویرایش نشده نمایش داده شود
        /// ولی توی الحاقیه باید ویرایش شده نمایش داده شود.
        /// </summary>
        public bool? UpdatedInAddendum { get; set; }
        /// <summary>
        /// این شرح خدمت در کدام الحاقیه ویرایش شده
        /// </summary>
        public Guid? UpdateInAddendumId { get; set; }
        /// <summary>
        /// عنوان مرکز هزینه
        /// </summary>
        public string? ActivityCenterTitle { get; set; }
        public bool? IsForExecutionRequest { get; set; }
        #endregion

        #region relation 
        public Guid ActivityCenterID { get; set; }
        public Guid ContractID { get; set; }
        public Contract Contract { get ; set; }
        public Guid CurrencyID { get; set; }
        public Currency Currency { get; set; }
        public Guid? FinePaymentMethodID { get; set; }
        public FinePaymentMethod? FinePaymentMethod { get; set; }
        public Guid? UnitOfMeasurementID { get; set; }
        public List<ContractEstimatedmeter>? ContractEstimatedmeters { get; set; }
        public Guid? ContractAddendumId { get; set; }
        public ContractAddendum? ContractAddendum { get; set; }
        #endregion

    }
}
