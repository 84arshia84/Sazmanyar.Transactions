using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Enums
{
    /// <summary>
    /// نوع نقش سازمان
    /// </summary>
    public enum OrganizationRoleTypeEnum
    {
        /// <summary>
        /// درآمد
        /// </summary>
        INCOME=1,  
        /// <summary>
        /// هزینه
        /// </summary>
        COST=2,    // Role as a cost
    }
}
