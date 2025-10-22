using AppCore.Entities.ContractAccessGroups;
using AppCore.Entities.ContractsInformation.Contratcs;
using AppCore.Entities.ContractsInformation.TransactionExecutionRequests;
using AppCore.Entities.FactorAccessGroup;
using AppCore.Entities.FactorInformation.Factors;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroupOrganizationUnits;
using AppCore.Entities.SettingEntities.PublicSettingEntities.SettingEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.SettingEntities.Organizationalunits
{
    /// <summary>
    /// واحد سازمانی
    /// </summary>
    [Table("Organizationalunit", Schema = "TAM")]
    public class Organizationalunit : SettingEntity
    {
        #region relation
        public List<Contract> Contract { get; set; }
        public List<Factor>? Factors { get; set; }
        public List<TransactionExecutionRequest> TransactionExecutionRequests { get; set; }
        public ICollection<InvoiceAccessGroupOrganizationUnit> InvoiceAccessGroupOrganizationUnits { get; set; }
        public ICollection<ContractAccessGroupOrganizationUnits>? ContractAccessGroupOrganizationUnits { get; set; }
        public ICollection<FactorAccessGroupOrganizationUnits> FactorAccessGroupOrganizationUnits { get; set; } 
       
        #endregion
    }
}
