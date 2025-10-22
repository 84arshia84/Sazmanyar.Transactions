using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.SettingDtos
{
    public class UnitOfMeasurementDto
    {
        public Guid key { get; set; }
        public long row {  get; set; }
        public string title { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
