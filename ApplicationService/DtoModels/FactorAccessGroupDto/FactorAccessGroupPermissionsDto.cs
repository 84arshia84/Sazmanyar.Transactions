using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.FactorAccessGroupDto
{
    public class FactorAccessGroupPermissionsDto
    {
        public Guid Id { get; set; }
        public bool Factor { get; set; }    
        public bool AccessGroupSettings { get; set; }    
        public bool BaseSettings { get; set; }    
        public bool WorkFlow { get; set; }
        public Guid FactorAccessGroupId { get; set; }
    }
}
