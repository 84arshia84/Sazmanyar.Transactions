using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.FactorDtos.FactorNettingProcessItem
{
    public class FactorNettingProcessItemGetDto
    {
        public Guid Id { get; set; }
        public Guid FactorId { get; set; }
        public string Title { get; set; }
        public decimal Percentage { get; set; }
        public decimal Amount { get; set; }
        public bool IsDeduction { get; set; }
        public bool IsEditable { get; set; }
        public Guid? CurrencyId { get; set; }
    }
}
