using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.FactorDtos.FactorNettingProcessItem
{
    /// <summary>
    /// مبلغ خالص شده فاکتور
    /// </summary>
    public class NettedAmountGet
    {
        public decimal NettedAmount { get; set; }
        public Guid Currency { get; set; }
    }
}
