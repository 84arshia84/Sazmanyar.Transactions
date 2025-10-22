using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.SettingDtos
{
    public class StagesRolesDto
    {
        public Guid Key { get; set; }
        public long Row { get; set; }
        public bool CanEdit { get; set; }
        public Guid StageId { get; set; }
        public Guid RoleId { get; set; }
    }
}
