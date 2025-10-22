using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.DesignCodesProperties.FactorDesignCodes
{
    /// <summary>
    /// پارامتر های الگوی کد برای فاکتور
    /// </summary>
    public class FactorDesignCodeParameterRel
    {
        #region properties
        public Guid Id { get; set; }
        public Guid? FactorTypeId { get; set; }
        public Guid? RoleOfOrganizationId { get; set; }
        public Guid? OrganizationUnitId { get; set; }
        #endregion
        #region relation
        public Guid FactorDesignCodeId { get; set; }
        #endregion
    }
}
