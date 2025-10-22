using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.DesignCodeDtos.FieldObjectDtos
{
    public class FieldObjectDto
    {
        public string Order { get; set; }
        public string Field { get; set; }
        public string ShortKey { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public string Min { get; set; }
        public string Max { get; set; }
        public string ToNext { get; set; }
    }
}
