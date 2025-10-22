using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Core.GeoJson;

namespace ApplicationService.DtoModels.WFEDto
{
    public class WFEGetCurentEntitiesForUserDto
    {
        public Guid Id { get; set; }
        public bool Assigned { get; set; }
        public bool IsSendable { get; set; }
        public bool IsEdit { get; set; }
        public bool CanAssign { get; set; }
//        "IsSendableTransmitalByStageUsers": 0,
//"CanArchive": 1,
//"IsArchived": 0,
    }
}
