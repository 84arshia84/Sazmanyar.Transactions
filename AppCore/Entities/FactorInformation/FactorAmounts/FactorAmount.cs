using AppCore.Entities.FactorInformation.Factors;
using AppCore.Entities.SettingEntities.Currencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.FactorInformation.FactorAmounts
{
    /// <summary>
    /// مقادیر مالی فاکتور
    /// </summary>
    public class FactorAmount
    {
        #region properteis
        public Guid Id { get; set; }
        public decimal RequestedAmount { get; set; }
        public decimal NettedAmount {  get; set; }
        #endregion

        #region relations
        public Guid CurrencyId { get; set; }
        public Currency Currency { get; set; }
        public Guid FactorId { get; set; }
        public Factor Factor { get; set; }
        #endregion
    }
}
