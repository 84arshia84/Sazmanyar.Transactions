using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Enums
{
    /// <summary>
    /// بخش های سامانه معاملات
    /// </summary>
    public enum SystemParts
    {
        /// <summary>
        /// در خواست برگزاری معالمه
        /// </summary>
        TransactionExecutionRequest=1,
        /// <summary>
        /// معاملات
        /// </summary>
        Contract=2,
        /// <summary>
        /// الحاقیه معاملات
        /// </summary>
        ContractAddendum=3,
        /// <summary>
        /// صورت وضعیت
        /// </summary>
        Invoice = 4,
        /// <summary>
        /// فاکتور
        /// </summary>
        Factor=5
    }
}
