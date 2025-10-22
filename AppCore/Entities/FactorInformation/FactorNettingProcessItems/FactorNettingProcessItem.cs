using AppCore.Entities.FactorInformation.Factors;
using AppCore.Entities.SettingEntities.Currencies;
using AppCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.FactorInformation.FactorNettingProcessItems
{
    /// <summary>
    /// خالص سازی فاکتور
    /// </summary>
    public class FactorNettingProcessItem
    {
        #region properties
        public Guid Id { get; set; }
        /// <summary>
        /// عنوان
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// درصد
        /// </summary>
        public decimal Percentage { get; set; }
        /// <summary>
        /// مبلغ
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// نوع خالص سازی
        /// </summary>
        public NettingProcessTypesEnum NettingProcessTypes { get; set; }
        /// <summary>
        /// کسورات است یا اضافات
        /// </summary>
        public bool IsDeduction { get; set; }
        public bool IsEditable { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime InsertDate { get; set; }
        public Guid InsertBy { get; set; }
        public Guid? DeleteBy { get; set; }
        public DateTime? DeleteDate { get; set; }
        #endregion

        #region relations
        public Guid? CurrencyId {  get; set; }
        public Currency? Currency { get; set; }
        public Guid FactorId { get; set; }
        public Factor Factor { get; set; }
        #endregion
    }
}
