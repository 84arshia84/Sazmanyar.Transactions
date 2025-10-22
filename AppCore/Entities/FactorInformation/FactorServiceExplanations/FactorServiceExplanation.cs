using AppCore.Entities.FactorInformation.Factors;
using AppCore.Entities.SettingEntities.Activitycenters;
using AppCore.Entities.SettingEntities.Currencies;
using AppCore.Entities.SettingEntities.UnitOfMeasurements;
using AppCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.FactorInformation.FactorServiceExplanations
{
    /// <summary>
    /// شرح اقلام فاکتور
    /// </summary>
    public class FactorServiceExplanation
    {
        #region properties
        public Guid Id { get; set; }
        /// <summary>
        /// عنوان شرح اقلام
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        ///  مرجع فعالیت
        /// </summary>
        public ActivityReferenceEnum ActivityReference { get; set; }
        /// <summary>
        /// پروژه
        /// </summary>
        public Guid? ProjectID { get; set; }
        /// <summary>
        ///  منشور
        /// </summary>
        public Guid? ProposalID { get; set; }
        /// <summary>
        /// قلم قابل تحویل
        /// </summary>
        public Guid? DeliverablePen { get; set; }
        /// <summary>
        /// نوع شرج خدمت
        /// </summary>
        public ExplanationTypeEnum ExplanationType { get; set; }
        /// <summary>
        /// مبلغ واحد
        /// فی قلم
        /// </summary>
        public decimal? UnitAmount { get; set; }
        /// <summary>
        /// مبلغ کل
        /// </summary>
        public decimal? TotalAmount { get; set; }
        /// <summary>
        /// نرخ تسعیر
        /// </summary>
        public decimal? AccelerationRate { get; set; }
        /// <summary>
        /// مقدار
        /// </summary>
        public decimal? Amount { get; set; }
        /// <summary>
        /// لیست تامینی
        /// </summary>
        public bool IsSupplyList { get; set; }
        /// <summary>
        /// کالا
        /// </summary>
        public Guid? CommodityId { get; set; }
        /// <summary>
        /// نام کالا
        /// </summary>
        public string? CommodityName { get; set; }
        /// <summary>
        /// کد کالا
        /// </summary>
        public string? CommodityCode { get; set; }
        public Guid? SupplyListId { get; set; }
        /// <summary>
        /// عنوان لیست تامین
        /// </summary>
        public string? SupplyListName { get; set; }
        #endregion
        #region relations
        /// <summary>
        /// مرکز هزینه
        /// </summary>
        public Guid ActivityCenterID { get; set; }
        /// <summary>
        /// نوع ارز
        /// </summary>
        public Guid CurrencyID { get; set; }
        public Currency Currency { get; set; }
        /// <summary>
        /// واحد اندازه گیری
        /// </summary>
        public Guid? UnitOfMeasurementID { get; set; }
        public UnitOfMeasurement? UnitOfMeasurement { get; set; }
        public Guid FactorId { get; set; }
        public Factor Factor { get; set; }
        #endregion
    }
}
