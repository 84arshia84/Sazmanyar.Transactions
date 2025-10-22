using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.ContractDtos
{
    public class ContractLogDto
    {
        public Guid Id { get; set; }
        public Guid ContractId { get; set; }
        public string ContractTitle { get; set; }
        public string ContractNumber { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<ChangeDetailDto> Changes { get; set; }
    }
}
