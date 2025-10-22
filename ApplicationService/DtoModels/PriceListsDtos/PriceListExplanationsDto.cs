using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.PriceListsDtos
{
    public class PriceListExplanationsDto
    {
        public Guid id { get; set; }
        public string Explanation { get; set; }
        public string RowNumber { get; set; }
        public string Unit { get; set; }
        public string UnitPrice { get; set; }
        public Guid PriceListClauseID { get; set; }
        public bool? IsStar { get; set; }
    }
}
