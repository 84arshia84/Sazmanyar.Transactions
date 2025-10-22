using AppCore.Entities.ContractsInformation.ContractAddendums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.ContractsInformation.AddendumCheckListValues
{
    public class AddendumCheckListValue
    {
        #region properties
        public Guid Id { get; set; }
        public string CheckListValues { get; set; }
        #endregion

        #region relation
        public Guid AddendumId { get; set; }
        public ContractAddendum ContractAddendum { get; set; }
        #endregion
    }
}
