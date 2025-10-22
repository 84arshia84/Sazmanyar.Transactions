using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.SettingDtos
{
    public class RoleOfOrganizationDto
    {
        public Guid key { get; set; }
        public int? row { get; set; }
        public string title { get; set; }
    }
}
