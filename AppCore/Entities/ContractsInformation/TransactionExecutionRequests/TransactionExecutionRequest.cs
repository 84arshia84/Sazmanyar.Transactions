using AppCore.Entities.ContractsInformation.ExecutionRequestCheckListValues;
using AppCore.Entities.ContractsInformation.ExecutionRequestServiceExplanations;
using AppCore.Entities.ContractsInformation.ServiceExplanations;
using AppCore.Entities.SettingEntities.ContractTypes;
using AppCore.Entities.SettingEntities.Organizationalunits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.ContractsInformation.TransactionExecutionRequests
{
    /// <summary>
    /// درخواست برگزاری معامله
    /// </summary>
    public class TransactionExecutionRequest
    {
        #region properties
        public Guid Id { get; set; }
        /// <summary>
        /// موضوع درخواست
        /// </summary>
        public string SubjectOfRequest { get; set; }
        /// <summary>
        /// تاریخ درخواست
        /// </summary>
        public DateTime DateOfRequest { get; set; }
        /// <summary>
        /// شماره درخواست
        /// </summary>
        public string NumberOfRequest { get; set; }
        /// <summary>
        /// مدت برآورداجرای کار
        /// </summary>
        public string EstimatedTimeForDoingRequest { get; set; }
        /// <summary>
        /// شرح درخواست
        /// </summary>
        public string? ExplenationRequest { get; set; }
        /// <summary>
        /// شرح درخواست مشاور
        /// </summary>
        public string? ExplenationRequestOfConsultant { get; set; }
        public bool IsDeleted { get; set; }
        /// <summary>
        /// چه کسی حذف کرد
        /// </summary>
        public Guid? DeletedBy { get; set; }
        /// <summary>
        /// در چه تاریخی حذف شد
        /// </summary>
        public DateTime? DeletedDate { get; set; }
        /// <summary>
        /// ثبت درخواست توسط چه کسی انجام شده
        /// </summary>
        public Guid InsertBy {  get; set; }
        /// <summary>
        /// ثبت درخواست درچه تاریخی صورت گرفته
        /// </summary>
        public DateTime InsertDate {  get; set; }
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
        public Guid ContractTypeId { get; set; }
        public ContractType ContractType {  get; set; }
        public Guid OrganizationId { get; set; }
        public Organizationalunit Organizationalunit { get; set; }
        public Guid? ExecutionRequestCheckListValueId { get; set; }
        public ExecutionRequestCheckListValue? ExecutionRequestCheckListValue { get; set; }
        public List<ExecutionRequestServiceExplanation> ExecutionRequestServiceExplanations { get; set;}
        #endregion
    }
}
