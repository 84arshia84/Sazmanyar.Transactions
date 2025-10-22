using AppCore.Entities.FactorInformation.Factors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.FactorInformation.FactorFinancialDetailes
{
    /// <summary>
    /// جزئیات مالی فاکتور
    /// </summary>
    public class FactorFinancialDetaile
    {
        #region properties
        public Guid Id { get; set; }
        /// <summary>
        /// درصد بیمه
        /// </summary>
        public decimal FactorInsurance_Percent { get; set; }
        /// <summary>
        /// درصد ارزش افزوده
        /// </summary>
        public decimal FactorValue_Added_Percent { get; set; }
        /// <summary>
        /// درصد مالیات
        /// </summary>
        public decimal FactorTax_Percent { get; set; }
        /// <summary>
        /// درصد حسن انجام کار
        /// </summary>
        public decimal GoodJob_Percent { get; set; }
        #endregion

        #region relations
        public Guid FactorId { get; set; }
        public Factor Factor { get; set; }
        #endregion
    }
}
