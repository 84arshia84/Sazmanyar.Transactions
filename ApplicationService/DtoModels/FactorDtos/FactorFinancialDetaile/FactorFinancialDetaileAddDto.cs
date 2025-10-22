using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.FactorDtos.FactorFinancialDetaile
{
    public class FactorFinancialDetaileAddDto
    {
        public decimal FactorInsurance_Percent { get; set; }
        public decimal FactorValue_Added_Percent { get; set; }
        public decimal FactorTax_Percent { get; set; }
        public decimal GoodJob_Percent { get; set; }
    }
}
