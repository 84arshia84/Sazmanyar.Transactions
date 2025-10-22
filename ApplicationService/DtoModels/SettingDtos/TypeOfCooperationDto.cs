using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.SettingDtos
{
    public class TypeOfCooperationDto
    {
        public Guid key { get; set; }
        public Int64? row { get; set; }
        public string title { get; set; }
        public string firstWord { get; set; }
    }
}
