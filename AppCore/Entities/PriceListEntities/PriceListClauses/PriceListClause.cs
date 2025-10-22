using AppCore.Entities.ContractsInformation.ContractEstimatedmeters;
using AppCore.Entities.PriceListEntities.PriceListExplanations;
using AppCore.Entities.PriceListEntities.PriceListFields;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.PriceListEntities.PriceListClauses
{
    /// <summary>
    /// فصل فهرست بها
    /// </summary>
    [Table("PriceListClause", Schema = "TAM")]
    public class PriceListClause
    {
        #region properties
        public Guid ID { get; set; }
        /// <summary>
        /// عنوان فصل
        /// </summary>
        public string ClauseTitle { get; set; }
        #endregion

        #region relation
        public Guid PriceListFieldID { get; set; }
        public PriceListField PriceListField { get; set; }
        public List<ContractEstimatedmeter> ContractEstimatedmeters { get; set; }
        public List<PriceListExplanation> PriceListExplanations { get; set; }
        #endregion
    }
}
