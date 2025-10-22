using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.UserDtos
{
    public class LoginUserDto
    {
        public Guid ID { get; set; }
        public string? FullQualifyName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FullName { get; set; }
    }
}
