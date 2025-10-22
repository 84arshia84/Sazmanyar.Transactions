using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.UserDtos
{
    public class UserAccessOnEditAndDelete
    {
        public bool AccessToEdit { get; set; } = false;
        public bool AccessToDelete { get; set; } = false;
    }
}
