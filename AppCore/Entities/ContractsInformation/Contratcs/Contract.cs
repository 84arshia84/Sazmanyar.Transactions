using AppCore.Entities.Attaches;
using AppCore.Entities.ContractsInformation.ContractAddendums;
using AppCore.Entities.ContractsInformation.ContractCheckListValues;
using AppCore.Entities.ContractsInformation.ContractCoefficients;
using AppCore.Entities.ContractsInformation.ContractEstimatedmeters;
using AppCore.Entities.ContractsInformation.ContractFinancialDetailes;
using AppCore.Entities.ContractsInformation.ContractGuarantees;
using AppCore.Entities.ContractsInformation.ContratTimeProfiles;
using AppCore.Entities.ContractsInformation.ServiceExplanations;
using AppCore.Entities.InvoiceInformations.InvoiceBaseInformations;
using AppCore.Entities.SettingEntities.ContractTypes;
using AppCore.Entities.SettingEntities.CreditSources;
using AppCore.Entities.SettingEntities.Organizationalunits;
using AppCore.Entities.SettingEntities.Statuses;
using AppCore.Entities.SettingEntities.TransActionTypes;
using AppCore.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.ContractsInformation.Contratcs
{
    /// <summary>
    ///  قرارداد 
    /// </summary>
    [Table("Contracts", Schema = "TAM")]
    public class Contract
    {
        #region properties
        [Key]
        public Guid ID { get; set; }
        /// <summary>
        /// عنوان قرارداد
        /// </summary>
        [Required]
        public string ContractTitle { get; set; }
        /// <summary>
        /// شماره قرارداد
        /// </summary>
        [Required]
        public string ContractNumber { get; set; }
        public Guid? ContractExpertID { get; set; }
        /// <summary>
        /// کارشناس قرارداد
        /// </summary>
        public string? ContractExpertName { get; set; }
        public Guid? ConsultantID { get; set; }
        /// <summary>
        /// نام مشاور
        /// </summary>
        public string? ConsultantName { get; set; }
        /// <summary>
        ///  الزام به مفاصا حساب
        /// </summary>
        public bool RequirementToCloseTheAccount { get; set; } = true;
        /// <summary>
        /// تاریخ ایجاد قرارداد
        /// </summary>
        public DateTime InsertContractDate { get; set; }
        /// <summary>
        /// ثبت کننده قرارداد
        /// </summary>
        public Guid InsertContractBy {  get; set; }
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
        /// قراردادی که الحاقیه دارد
        /// </summary>
        public bool? HasAddendum { get; set; }
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


        #region relation
        /// <summary>
        /// نوع قرارداد
        /// </summary>
        public Guid ContractTypeID { get; set; }
        public ContractType ContractType { get; set; }
        /// <summary>
        /// تیپ معامله
        /// </summary>
        public Guid TransActionTypeID { get; set; }
        public TransActionType TransActionType { get; set; }
        /// <summary>
        /// نقش سازمان
        /// </summary>
        public Guid RoleOFOrganizationID { get; set; }
        /// <summary>
        /// طرف معامله
        /// </summary>
        public Guid CorespondentID { get; set; }
        /// <summary>
        ///  طرف معامله حقیقی است یا حفوقی
        ///  true --> Real
        /// </summary>
        public bool CorespondentRealOrLegal {  get; set; }
        /// <summary>
        /// واحد سازمانی
        /// </summary>
        public Guid OrganizationUnitID { get; set; }
        public Organizationalunit Organizationalunit { get; set; }
        /// <summary>
        /// محل تامین اعتبار
        /// </summary>
        public Guid? CreditSourceID { get; set; }
        public CreditSource CreditSource { get; set; }
        /// <summary>
        /// مشخصات زمانی قرارداد
        /// </summary>
        public Guid TimeProfileID { get; set; }
        public ContractTimeProfile ContratTimeProfile { get; set; }
        public Guid FinancialDetailsID { get; set; }
        public ContractFinancialDetails ContractFinancialDetails { get; set; }
        public Guid? ContractCheckListValueId { get; set; }
        public ContractCheckListValue? ContractCheckListValues { get; set; }
        public List<ServiceExplanation> ServiceExplanations { get; set; }
        public List<InvoiceBaseInformation> InvoiceBaseInformations { get; set; }
        public List<ContractCoefficient>? ContractCoefficients { get; set; }
        public List<ContractAddendum>? ContractAddendums {  get; set; } 
        public List<ContractGuarantee>? ContractGuarantees { get; set; }
        public Guid? StatusId { get; set; }
        public Status? Status { get; set; }
        #endregion
    }
}
