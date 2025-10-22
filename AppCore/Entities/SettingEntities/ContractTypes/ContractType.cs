using AppCore.Entities.ContractAccessGroups;
using AppCore.Entities.ContractsInformation.Contratcs;
using AppCore.Entities.ContractsInformation.TransactionExecutionRequests;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroupContractTypes;
using AppCore.Entities.SettingEntities.CheckLists;
using AppCore.Entities.SettingEntities.PublicSettingEntities.SettingEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.SettingEntities.ContractTypes
{
    /// <summary>
    /// نوع معامله
    /// </summary>
    public class ContractType:SettingEntity
    {
        #region properties
        /// <summary>
        /// سند مرتبط با نوع معامله
        /// </summary>
        public Guid? OfficeOnlineDocumentId { get; set; }
        #endregion
        #region relation
        public List<Contract>? Contracts { get; set; }
        public List<TransactionExecutionRequest>? TransactionExecutionRequests { get; set; }
        public List<CheckList>? CheckLists { get; set; }
        public ICollection<InvoiceAccessGroupContractType> InvoiceAccessGroupContractTypes { get; set; }
        public ICollection<ContractAccessGroupContractType>? ContractAccessGroupContractTypes { get; set; }


        #endregion
    }
}
