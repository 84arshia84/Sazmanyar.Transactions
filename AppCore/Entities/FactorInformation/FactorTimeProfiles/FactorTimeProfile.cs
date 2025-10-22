using AppCore.Entities.FactorInformation.Factors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.FactorInformation.FactorTimeProfiles
{
    /// <summary>
    /// جزئیات زمانی فاکتور
    /// </summary>
    public class FactorTimeProfile
    {
        #region properties
        public Guid Id { get; set; }
        /// <summary>
        /// تاریخ فاکتور
        /// </summary>
        public DateTime? FactorDate { get; set; }
        /// <summary>
        /// تاریخ شروع فاکتور
        /// </summary>
        public DateTime? FactorStartDate { get; set; }
        /// <summary>
        /// تاریخ پایان فاکتور
        /// </summary>
        public DateTime? FactorEndDate { get; set; }
        /// <summary>
        /// مدت فاکتور
        /// </summary>
        public string? FactorPeriod { get; set; }
        #endregion

        #region relations
        public Guid FactorId { get; set; }
        public Factor Factor {  get; set; }
        #endregion
    }
}
