using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.ContractDtos
{
    public class EstimatedmeterDto
    {
        public Guid id { get; set; }
        public int row { get; set; }
        public string year { get; set; }
        public Guid yearId { get; set; }
        public string field { get; set; }
        public Guid fieldId { get; set; }
        public string clause { get; set; }
        public Guid clauseId { get; set; }
        public string explenation { get; set; }
        public Guid explenationId { get; set; }
        public decimal amount { get; set; }
        public string? coefficientTitle { get; set; }
        public decimal sumOfCoefficients { get; set; }
        public decimal rawPrice { get; set; }
        public decimal rowPrice { get; set; }
        public string? description { get; set; }
        public Guid serviceExplanationId { get; set; }
        public bool? isDeleted { get; set; }
        public bool? updatedInAddendum { get; set; }
        public bool? isAddendum { get;set; }
        public Guid? addendumId { get; set; }

    }
}
