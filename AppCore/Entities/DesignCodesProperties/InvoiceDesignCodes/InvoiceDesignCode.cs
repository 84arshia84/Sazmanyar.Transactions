using AppCore.Entities.DesignCodesProperties.ContractDesignCodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.DesignCodesProperties.InvoiceDesignCodes
{
    /// <summary>
    /// الگوی کد صورت وضعیت 
    /// </summary>
    public class InvoiceDesignCode
    {
        #region properties
        public Guid Id { get; set; }
        /// <summary>
        /// الگوی کد
        /// </summary>
        public string DesignCode { get; set; }
        /// <summary>
        /// پیش نمایش الگوی کد
        /// </summary>
        public string Preview { get; set; }
        /// <summary>
        /// نمایش پارامتر های الگوی کد
        /// </summary>
        public string ParameterPreview { get; set; }
        /// <summary>
        /// شمارنده
        /// </summary>
        public int Counter { get; set; }
        #endregion
        #region relation
        public List<InvoiceDesignCodeParameterRel> Parameters { get; set; }
        #endregion
    }
}
