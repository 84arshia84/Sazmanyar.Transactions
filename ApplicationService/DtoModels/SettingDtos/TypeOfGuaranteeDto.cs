using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.SettingDtos
{
    public class TypeOfGuaranteeDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Order { get; set; }
    }
}
