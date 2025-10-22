using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Enums
{
    /// <summary>
    /// تغییرات الحاقیه بر چه اساس هست
    /// </summary>
    public enum AddendumChangeTypeEnum
    {
        /// <summary>
        /// الحاقیه جهت تغییر اطلاعات قرارداد ثبت شده.
        /// </summary>
        ChangeContractInformation=1,
        /// <summary>
        /// الحاقیه جهت تغییر و یا اضافه کردن شرح خدمت ثبت شده
        /// </summary>
        AddOrChangeServiceExplenation=2
    }
}
