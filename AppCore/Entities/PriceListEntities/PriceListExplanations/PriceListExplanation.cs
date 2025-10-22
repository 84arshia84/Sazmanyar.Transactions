using AppCore.Entities.ContractsInformation.ContractEstimatedmeters;
using AppCore.Entities.PriceListEntities.PriceListClauses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.PriceListEntities.PriceListExplanations
{
    /// <summary>
    /// شرح فهرست بها
    /// </summary>
    [Table("PriceListExplanation", Schema = "TAM")]
    public class PriceListExplanation
    {
        #region properties
        public Guid ID { get; set; }
        /// <summary>
        /// شرح
        /// </summary>
        public string Explanation { get; set; }
        /// <summary>
        /// شماره ردیف
        /// </summary>
        public string RowNumber { get; set; }
        /// <summary>
        /// واحد
        /// </summary>
        public string Unit {  get; set; }
        /// <summary>
        /// بهای واحد
        /// </summary>
        public string UnitPrice { get; set; }
        /// <summary>
        /// شرح فهرست بهای دارای ستاره
        /// </summary>
        public bool? IsStar { get; set; }
        #endregion
        #region relation
        public Guid PriceListClauseID { get; set; }
        public PriceListClause PriceListClause { get; set; }
        public List<ContractEstimatedmeter> ContractEstimatedmeters { get; set; }
        #endregion
    }
}
