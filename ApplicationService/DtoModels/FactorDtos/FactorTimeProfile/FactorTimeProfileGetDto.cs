using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.FactorDtos.FactorTimeProfile
{
    public class FactorTimeProfileGetDto
    {
        public Guid Id {  get; set; }
        public Guid FactorId { get; set; }
        public DateTime? FactorDate { get; set; }
        public DateTime? FactorStartDate { get; set; }
        public DateTime? FactorEndDate { get; set; }
        public string? FactorPeriod { get; set; }
    }
}
