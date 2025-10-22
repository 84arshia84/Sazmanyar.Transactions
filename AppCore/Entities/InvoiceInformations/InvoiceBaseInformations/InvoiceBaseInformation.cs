using AppCore.Entities.ContractsInformation.Contratcs;
using AppCore.Entities.InvoiceInformations.EstimatedMeterFinancials;
using AppCore.Entities.InvoiceInformations.InvoiceAmounts;
using AppCore.Entities.InvoiceInformations.NettingProcesses;
using AppCore.Entities.InvoiceInformations.ServiceExplanationFinancials;
using AppCore.Entities.InvoicesInformations.InvoiceType;
using AppCore.Entities.Organizations;
using AppCore.Entities.SettingEntities.CreditSources;
using AppCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.InvoiceInformations.InvoiceBaseInformations
{
    public class InvoiceBaseInformation
    {
        #region properties
        public Guid Id { get; set; }
        /// <summary>
        /// عنوان صورت وضعیت
        /// </summary>
        public string InvoiceTitle { get; set; }
        /// <summary>
        /// تاریخ منتهی به
        /// </summary>
        public DateTime? LeadingToDate { get; set; }
        /// <summary>
        /// تاریخ ارسال
        /// </summary>
        public DateTime SendDate { get; set; }
        /// <summary>
        /// شرح
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// کد صورت وضعیت
        /// </summary>
        public string InvoiceCode { get; set; }
        /// <summary>
        /// شماره صورت وضعیت
        /// </summary>
        public int? InvoiceNumber { get; set; }
        /// <summary>
        /// تاریخ ایجاد اطلاعات پایه صورت وضعیت
        /// </summary>
        public DateTime InsertDate { get; set; }
        /// <summary>
        /// ثبت کننده اطلاعات پایه صورت وضعیت
        /// </summary>
        public Guid InsertBy { get; set; }
        /// <summary>
        /// حذف کننده اطلاعات پایه صورت وضعیت
        /// </summary>
        public Guid? DeleteBy { get; set; }
        /// <summary>
        /// تاریخ حذف
        /// </summary>
        public DateTime? DeleteDate { get; set; }
        public bool IsDeleted { get; set; }
        /// ستون های مربوط به فلو
        public Guid CurrentStageId { get; set; }
        public string LastActionTitle { get; set; }
        public string CurrentStateTitle { get; set; }
        public bool IsFinalApproved { get; set; }

        #endregion

        #region relations
        /// <summary>
        /// قرارداد
        /// </summary>
        public Guid ContractId { get; set; }
        public Contract Contract { get; set; }
        /// <summary>
        /// نوع صورت وضیعت
        /// </summary>
        public Guid InvoiceTypeId { get; set; }
        public InvoiceType InvoiceType { get; set; }
        public List<ServiceExplanationFinancial> ServiceExplanationFinancials { get; set; }
        public List<EstimatedMeterFinancial> EstimatedMeterFinancials { get; set; }
        public List<NettingProcessItem> NettingProcessItems { get; set; }
        public List<InvoiceAmount>? InvoiceAmount { get; set; }
        public Guid? AccountId { get; set; }
        public Account? Account { get; set; }
        #endregion


    }
}
