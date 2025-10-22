using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.Histories
{
    public class History
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public DateTime InsertDate { get; set; }    
        public string UserName { get; set; }
        public DateTime OperationDate { get; set; }
        public string? ActionType { get; set; }
        public Guid EntityId { get; set; }
    }
}
