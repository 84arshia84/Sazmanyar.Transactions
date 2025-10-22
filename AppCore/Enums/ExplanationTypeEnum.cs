using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Enums
{
    /// <summary>
    /// نوع شرح خدمت
    /// </summary>
    public enum ExplanationTypeEnum
    {
        /// <summary>
        /// شرح خدمت معمولی که نحوه محاسبات آن با درصد می باشد
        /// </summary>
        normalExplanation = 0,
        /// <summary>
        /// شرح خدمت حجمی که نحوه محاسبه آن با واحد اندازه گیری می باشد
        /// </summary>
        volumetricExplanation = 1,
        /// <summary>
        /// شرح خدمت فهرست بهایی که نحوه محاسبه آن متر برآورد می باشد
        /// </summary>
        PriceListExplanation = 2
    }
}
