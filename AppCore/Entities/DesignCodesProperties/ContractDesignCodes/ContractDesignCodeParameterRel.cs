using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.DesignCodesProperties.ContractDesignCodes
{
    /// <summary>
    /// پارامتر های الگوی کد برای معاملات
    /// </summary>
    public class ContractDesignCodeParameterRel
    {
        #region properties
        public Guid Id { get; set; }
        public Guid? ContractTypeId { get; set; }
        public Guid? RoleOfOrganizationId { get; set; }
        public Guid? OrganizationUnitId { get; set; }
        #endregion
        #region relation
        public Guid ContractDesignCodeId { get; set; }
        #endregion
    }
}
