using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Enums
{
    public enum WorkFlowSituationEnum
    {
        /// <summary>
        /// برای جاری ها
        /// </summary>
        Mine=1,
        /// <summary>
        /// برای منتظر اقدام
        /// </summary>
        WaitForAction=2,

        ///<summary>
        /// منتظر تایید
        ///</summary>
        WaitForConfirm = 3,
        /// <summary>
        /// برای همه
        /// </summary>
        All = 4,

    }
}
