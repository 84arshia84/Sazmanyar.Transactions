using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Enums
{
    public enum NettingProcessTypesEnum
    {
        /// <summary>
        /// پیش پرداخت
        /// </summary>
        prePayment=1,
        /// <summary>
        /// علی الحساب
        /// </summary>
        onAccount = 2,
        /// <summary>
        /// بیمه
        /// </summary>
        insurance= 3,
        /// <summary>
        /// ارزش افزوده
        /// </summary>
        addedValue=4,
        /// <summary>
        /// مالیات
        /// </summary>
        Tax = 5,
        /// <summary>
        /// حسن انجام کار
        /// </summary>
        GoodJob= 6,
        /// <summary>
        /// ما بقی
        /// </summary>
        others = 0,
    }
}
