using AppCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.ContractDtos
{
    public class ContractAddendumDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime AddendumDate { get; set; }
        public string? RateOfContractPriceChanged { get; set; }
        public bool? IsDeleted { get; set; }
        public int AddendumChangeType { get; set; }
        public Guid ContractId { get; set; }
        public ContractDto? Contract { get; set; }
        public Guid AddendumTypeId { get; set; }
        public string? AddendumTypeTitle { get; set; }
        public bool? IsfinalApproved { get; set; }

    }
}
