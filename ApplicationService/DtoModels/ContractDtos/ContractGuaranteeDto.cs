using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.ContractDtos
{
    public class ContractGuaranteeDto
    {
        public Guid Id { get; set; }
        public int? row {  get; set; }
        public decimal GuaranteePrice { get; set; }
        public DateTime? ValidityDate { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string? RealeaseDescription { get; set; }
        public string? RealeaseExplenation { get; set; }
        public Guid? ForGuaranteeId { get; set; }
        public string? ForGuaranteeTitle { get; set; }
        public Guid TypeOfGuaranteeId { get; set; }
        public string? TypeOfGuaranteeTitle { get; set; }
        public Guid? ReleaseConditionId { get; set; }
        public string? ReleaseConditionTitle { get; set; }
        public Guid ContractId { get; set; }
        public bool? IsUpdated { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
