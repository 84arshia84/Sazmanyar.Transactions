using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.UserDtos
{
    public class UserAccessOnPermissionsDto
    {
        public bool AccessToTransactionExecutionRequest { get; set; } = false;
        public bool AccessToContract { get; set; } = false;
        public bool AccessToContractAddendum { get; set; } = false;
        public bool AccessToAccessGroupSettings { get; set; } = false;
        public bool AccessToBaseSettings { get; set; } = false;
        public bool AccessToPriceListSettings { get; set; } = false;
        public bool AccessToInvoice {  get; set; } = false;
        public bool AccessToFactor { get; set; } = false;
        public bool AccessToFactorAccessGroup { get; set; } = false;
        public bool AccessToWorkFlow { get; set; } = false;
        public bool AccessToInvoiceWorkFlow { get; set; } = false;
        public bool AccessToInvoiceBaseSetting { get; set; } = false;
        public bool AccessToInvoiceAccessGroupSetting { get; set; } = false;
        public bool AccessToSaveContract { get; set; } = false;
        public bool AccessToSaveTransactionExecutionRequest { get; set; } = false;
        public bool AccessToSaveContractAddendum { get; set; } = false;
        public bool AccessToSaveFactor { get; set; } = false;
    }
}
