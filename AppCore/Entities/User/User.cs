using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.User
{
    public class User
    {
        public Guid ID { get; set; }
        public string FullQualifyName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
