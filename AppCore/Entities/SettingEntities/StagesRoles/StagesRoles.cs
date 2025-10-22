using AppCore.Entities.SettingEntities.Roles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.SettingEntities.StagesRoles
{
    public class StagesRoles
    {
        public Guid Id { get; set; }
        public bool CanEdit { get; set; }
        public Guid RoleId { get; set; }
        public Guid StageId { get; set; }
        public Role Role { get; set; }
    }
}
