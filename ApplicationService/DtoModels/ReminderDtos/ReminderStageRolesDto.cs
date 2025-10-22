using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.ReminderDtos
{
    public class ReminderStageRolesDto
    {
        public Guid Id { get; set; }
        public bool HasView { get; set; }
        public bool HasEdit { get; set; }
    }
}
