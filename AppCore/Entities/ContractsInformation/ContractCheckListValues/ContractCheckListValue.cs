using AppCore.Entities.ContractsInformation.Contratcs;using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.ContractsInformation.ContractCheckListValues
{
    /// <summary>
    /// مقادیر داخل جک لیست ها متعلق به یک قرارداد
    /// </summary>
    public class ContractCheckListValue
    {
        #region properties
        public Guid Id { get; set; }
        public string CheckListValues { get; set; }
        #endregion

        #region relation

        public Guid ContractId { get; set; }
        public Contract Contract { get; set; }
        #endregion
    }
}
