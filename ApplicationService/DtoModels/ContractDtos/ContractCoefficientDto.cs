using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.ContractDtos
{
    public class ContractCoefficientDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal defaultCoefficient { get; set; }
        public string defaultRows { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsUpdated { get; set; }
        public bool? IsAddendum { get; set; }
        public Guid? AddendumId { get; set; }
        public int row {  get; set; }
    }
}
