using AppCore.Entities.ContractsInformation.ContractEstimatedmeters;
using AppCore.Entities.PriceListEntities.PriceListFields;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.PriceListEntities.PriceLists
{
    /// <summary>
    /// فهرست بها
    /// </summary>
    [Table("PriceList", Schema = "TAM")]
    public class PriceList
    {
        #region properties
        public Guid ID { get; set; }
        /// <summary>
        /// سال فهرست بها
        /// </summary>
        public string Year { get; set; }
        #endregion
        #region relation
        public List<PriceListField> PriceListFields { get; set; }
        public List<ContractEstimatedmeter> ContractEstimatedmeters { get; set; }
        #endregion
    }
}
