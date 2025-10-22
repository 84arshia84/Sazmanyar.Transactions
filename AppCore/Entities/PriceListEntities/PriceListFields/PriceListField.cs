using AppCore.Entities.ContractsInformation.ContractEstimatedmeters;
using AppCore.Entities.PriceListEntities.PriceListClauses;
using AppCore.Entities.PriceListEntities.PriceLists;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.PriceListEntities.PriceListFields
{
    /// <summary>
    /// رشته فهرست بها
    /// </summary>
    [Table("PriceListField", Schema = "TAM")]
    public class PriceListField
    {
        #region properties
        public Guid ID { get; set; }
        /// <summary>
        /// عنوان رشته
        /// </summary>
        public string FieldTitle { get; set; }

        #endregion

        #region relation
        public Guid PriceListID { get; set; }
        public PriceList PriceList { get; set; }
        public List<PriceListClause> PriceListClauses { get; set; }
        public List<ContractEstimatedmeter> ContractEstimatedmeters { get; set; }
        #endregion
    }
}
