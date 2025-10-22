using AppCore.Entities.ContractsInformation.ContractAddendums;
using AppCore.Entities.ContractsInformation.Contratcs;
using AppCore.Entities.SettingEntities.DefaultCoefficients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.ContractsInformation.ContractCoefficients
{
    /// <summary>
    /// ضرایب قرارداد
    /// این ضرایب در فهرست بها کاربردارد
    /// </summary>
    public class ContractCoefficient
    {
        #region properties
        public Guid Id { get; set; }
        /// <summary>
        /// عنوان ضریب
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// ضریبی که در قرارداد ثبت می شود.
        /// </summary>
        public decimal? CoefficientValue { get; set; }
        /// <summary>
        /// ردیف پیش فرضی که در قرارداد ثبت می شود
        /// </summary>
        public string? DefaultRowValue { get; set; }
        /// <summary>
        /// ضریبی که توی الحاقیه حذف شده
        /// </summary>
        public bool? IsDeleted { get; set; }
        /// <summary>
        /// زمان حذف شدن ضریب
        /// </summary>
        public DateTime? DeleteDate { get; set; }
        /// <summary>
        /// ضریبی که در الحاقیه افزوده شده.
        /// </summary>
        public bool? IsAddendum { get;set; }
        /// <summary>
        /// ضریبی که در قرارداد ثبت شده و در الحاقیه ویرایش شده
        /// </summary>
        public bool? UpdatedInAddendum { get; set; }
        /// <summary>
        /// این ضریب در کدام الحاقیه ویرایش شده
        /// </summary>
        public Guid? UpdateInAddendumId { get; set; }
        /// <summary>
        /// این ضریب در کدام الحاقیه حذف شده
        /// </summary>
        public Guid? DeletedInAddedumId { get; set; }
        public int? Order {  get; set; }
        #endregion
        #region relations
        public Guid ContractId { get; set; }
        public Contract Contract { get; set; }
        public Guid? AddendumId { get; set; }
        public ContractAddendum? ContractAddendum { get; set; }
        #endregion
    }
}
