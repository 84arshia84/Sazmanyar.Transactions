using AppCore.Entities.ContractsInformation.Contratcs;
using AppCore.Entities.SettingEntities.ForGuarantees;
using AppCore.Entities.SettingEntities.ReleaseConditions;
using AppCore.Entities.SettingEntities.TypeOfGuarantees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.ContractsInformation.ContractGuarantees
{
    /// <summary>
    /// تضامین قرارداد
    /// </summary>
    public class ContractGuarantee
    {
        #region properties
        public Guid Id { get; set; }
        /// <summary>
        /// مبلغ ضمانت
        /// </summary>
        public decimal GuaranteePrice { get; set; }
        /// <summary>
        /// تاریخ اعتبار
        /// </summary>
        public DateTime? ValidityDate { get; set; }
        /// <summary>
        /// تاریخ آزادسازی
        /// </summary>
        public DateTime? ReleaseDate { get; set; }
        /// <summary>
        /// توضیحات
        /// </summary>
        public string? RealeaseDescription { get; set; }
        /// <summary>
        /// شرح آزادسازی
        /// </summary>
        public string? RealeaseExplenation { get; set; }
        public bool? IsUpdate { get; set; }
        public bool? IsDeleted { get; set; }
        public int? Order {  get; set; }
        #endregion
        #region relation 
        public Guid? ForGuaranteeId { get; set; }
        public ForGuarantee? ForGuarantee { get; set; }
        public Guid  TypeOfGuaranteeId { get; set; }
        public TypeOfGuarantee TypeOfGuarantee { get; set; }
        public Guid? ReleaseConditionId { get; set; }
        public ReleaseCondition? ReleaseCondition { get; set; }
        public Guid ContractId { get; set; }
        public Contract Contract { get; set; }
        #endregion
    }
}
