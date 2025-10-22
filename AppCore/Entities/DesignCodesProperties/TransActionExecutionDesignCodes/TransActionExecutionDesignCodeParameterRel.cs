using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.DesignCodesProperties.TransActionExecutionDesignCodes
{
    /// <summary>
    /// پارامتر های الگوی کد برای درخواست برگزاری معامله
    /// </summary>
    public class TransActionExecutionDesignCodeParameterRel
    {
        #region properties
        public Guid Id { get; set; }
        public Guid? ContractTypeId { get; set; }
        public Guid? OrganizationUnitId { get; set; }
        #endregion
        #region relation
        public Guid TransactionExecutionDesignCodeId { get; set; }
        #endregion
    }
}
