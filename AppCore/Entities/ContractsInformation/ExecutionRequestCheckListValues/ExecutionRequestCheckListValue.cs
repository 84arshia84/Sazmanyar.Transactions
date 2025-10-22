using AppCore.Entities.ContractsInformation.TransactionExecutionRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.ContractsInformation.ExecutionRequestCheckListValues
{
    public class ExecutionRequestCheckListValue
    {
        #region properties
        public Guid Id { get; set; }
        public string CheckListValues { get; set; }

        #endregion
        #region relations
        public Guid TransactionExecutionRequestsId { get; set; }
        public TransactionExecutionRequest TransactionExecutionRequests { get; set; }
        #endregion
    }
}
