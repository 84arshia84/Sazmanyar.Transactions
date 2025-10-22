using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.FactorDtos.FactorTimeProfile
{
    public class FactorTimeProfileAddDto
    {
        public DateTime? FactorDate { get; set; }
        public DateTime? FactorStartDate { get; set; }
        public DateTime? FactorEndDate { get; set; }
        public string? FactorPeriod { get; set; }
    }
}
