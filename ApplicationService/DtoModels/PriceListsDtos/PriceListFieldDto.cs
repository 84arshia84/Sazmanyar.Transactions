using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.PriceListsDtos
{
    public class PriceListFieldDto
    {
        public Guid id { get; set; }
        public string fieldTitle { get; set; }
        public Guid PriceListID { get; set; }
    }
}
