using AppCore.Entities.FactorInformation.FactorAmounts;
using AppCore.Entities.FactorInformation.FactorFinancialDetailes;
using AppCore.Entities.FactorInformation.FactorNettingProcessItems;
using AppCore.Entities.FactorInformation.FactorPayments;
using AppCore.Entities.FactorInformation.FactorServiceExplanations;
using AppCore.Entities.FactorInformation.FactorTimeProfiles;
using AppCore.Entities.SettingEntities.CreditSources;
using AppCore.Entities.SettingEntities.FactorTypes;
using AppCore.Entities.SettingEntities.Organizationalunits;
using AppCore.Entities.SettingEntities.RoleOfOrganizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.FactorInformation.Factors
{
    /// <summary>
    /// فاکتور
    /// </summary>
    public class Factor
    {
        #region properties
        public Guid Id { get; set; }
        /// <summary>
        /// موضوع فاکتور
        /// </summary>
        public string FactorTitle { get; set; }
        /// <summary>
        /// شماره فاکتور
        /// </summary>
        public string FactorNumber { get; set; }
        /// <summary>
        /// کارشناس معامله
        /// </summary>
        public Guid? FactorExpertID { get; set; }
        /// <summary>
        ///  الزام به مفاصا حساب
        /// </summary>
        public bool RequirementToCloseTheAccount { get; set; } = true;
        /// <summary>
        /// تاریخ ایجاد قرارداد
        /// </summary>
        public DateTime InsertFactorDate { get; set; }
        /// <summary>
        /// ثبت کننده قرارداد
        /// </summary>
        public Guid InsertFactorBy { get; set; }
        /// <summary>
        /// حذف کننده قرارداد
        /// </summary>
        public Guid? DeleteBy { get; set; }
        /// <summary>
        /// تاریخ حذف
        /// </summary>
        public DateTime? DeleteDate { get; set; }
        public bool IsDeleted { get; set; }
        /// <summary>
        /// آی دی آخرین وضعیت قرارداد در فلو
        /// </summary>
        public Guid CurrentStageId { get; set; }
        /// <summary>
        /// عنوان وضعیت کنونی فلو
        /// </summary>
        public string CurrentStatusTitle { get; set; }
        /// <summary>
        /// عنوان آخرین وضعیت فلو
        /// </summary>
        public string LastActionTitle { get; set; }
        /// <summary>
        /// جریان فلو به انتها رسیده است
        /// </summary>
        public bool? IsFinalApprove { get; set; }
        #endregion

        #region relations
        /// <summary>
        /// نوع فاکتور
        /// </summary>
        public Guid FactorTypeId { get; set; }
        public FactorType FactorType { get; set; }
        /// <summary>
        /// واحد سازمانی
        /// </summary>
        public Guid? OrganizationalunitId { get; set; }
        public Organizationalunit? Organizationalunit { get; set; }
        /// <summary>
        /// نقش سازمان
        /// </summary>
        public Guid RoleOfOrganizationId { get; set; }
        public RoleOfOrganization RoleOfOrganization { get; set; }
        /// <summary>
        /// محل تامین اعتبار
        /// </summary>
        public Guid? CreditSourceID { get; set; }
        public CreditSource CreditSource { get; set; }
        /// <summary>
        /// طرف معامله
        /// </summary>
        public Guid CorespondentID { get; set; }
        /// <summary>
        ///  طرف معامله حقیقی است یا حفوقی
        ///  true --> Real
        /// </summary>
        public bool CorespondentRealOrLegal { get; set; }
        /// <summary>
        /// جزئیات زمانی فاکتور
        /// </summary>
        public Guid FactorTimeProfileId { get; set; }
        public FactorTimeProfile FactorTimeProfile { get; set; }
        /// <summary>
        /// جزئیات مالی فاکتور
        /// </summary>
        public Guid FactorFinancialDetaileId { get; set; }
        public FactorFinancialDetaile FactorFinancialDetaile { get; set; }
        public List<FactorServiceExplanation> FactorServiceExplanations { get; set; }
        public List<FactorNettingProcessItem> FactorNettingProcessItems { get; set; }
        public List<FactorPayment> FactorPayments { get; set; }
        public List<FactorAmount> FactorAmounts { get; set; }
        #endregion
    }
}
