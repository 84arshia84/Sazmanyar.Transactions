using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.DesignCodesProperties.InvoiceDesignCodes
{
    /// <summary>
    /// پارامتر های الگوی کد صورت وضعیت
    /// </summary>
    public class InvoiceDesignCodeParameterRel
    {
        #region properties
        public Guid Id { get; set; }
        public Guid? ContractTypeId { get; set; }
        public Guid? RoleOfOrganizationId { get; set; }
        public Guid? OrganizationUnitId { get; set; }
        public Guid? InvoiceTypeId { get; set; }
        #endregion
        #region relation
        public Guid InvoiceDesignCodeId { get; set; }
        #endregion
    }
}
