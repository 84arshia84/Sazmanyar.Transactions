using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.WFEDto
{
    public class AssignUsersDto
    {
        public Guid id { get; set; }
        public List<Guid> users { get; set; }
    }
}
