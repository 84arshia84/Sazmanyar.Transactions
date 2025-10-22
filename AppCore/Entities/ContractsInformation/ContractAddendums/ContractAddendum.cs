using AppCore.Entities.ContractsInformation.ContractCoefficients;
using AppCore.Entities.ContractsInformation.ContractEstimatedmeters;
using AppCore.Entities.ContractsInformation.Contratcs;
using AppCore.Entities.ContractsInformation.ServiceExplanations;
using AppCore.Entities.SettingEntities.AddendumTypes;
using AppCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.ContractsInformation.ContractAddendums
{
    /// <summary>
    /// الحاقیه قرارداد
    /// </summary>
    public class ContractAddendum
    {
        #region properties
        public Guid Id { get; set; }
        /// <summary>
        /// عنوان الحاقیه
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// تاریخ الحاقیه
        /// </summary>
        public DateTime AddendumDate { get; set; }
        /// <summary>
        /// تاریخ ثبت الحاقیه
        /// </summary>
        public DateTime InsertAddendumDate { get; set; }
        /// <summary>
        /// شخصی که الحاقیه ثبت کرده
        /// </summary>
        public Guid? InsertAddendumBy {  get; set; }
        /// <summary>
        /// تاریخ حذف الحاقیه
        /// </summary>
        public DateTime? DeleteAddenDumDate { get; set; }
        /// <summary>
        /// شخصی که الحاقیه را حذف کرده
        /// </summary>
        public Guid? DeleteAddendumBy { get; set;}
        public bool? IsDeleted { get; set; }
        /// <summary>
        /// نوع تغییر الحاقیه
        /// </summary>
        public AddendumChangeTypeEnum AddendumChangeType { get; set; }
        /// <summary>
        /// تغییر مبلغ قرارداد در این الحاقیه
        /// </summary>
        public string? RateOfContractPriceChanged { get; set; }
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
        public Guid ContractId { get; set; }
        public Contract Contract { get; set; }
        public List<ServiceExplanation>? ServiceExplanations { get; set; }
        public List<ContractCoefficient>? ContractCoefficients { get; set; }
        public List<ContractEstimatedmeter>? ContractEstimatedmeters { get; set; }
        public Guid AddendumTypeId { get; set; }
        public AddendumType AddendumType { get; set; }
        #endregion
    }
}
