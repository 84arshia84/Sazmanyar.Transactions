using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.PriceListsDtos
{
    public class PriceListClauseDto
    {
        public Guid id { get; set; }
        public string CluaseTitle { get; set; }
        public Guid FieldID { get; set; }
    } 
}
