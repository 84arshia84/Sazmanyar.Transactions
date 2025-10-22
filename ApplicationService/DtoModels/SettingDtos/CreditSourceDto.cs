using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.SettingDtos
{
    public class CreditSourceDto
    {
        public Guid key { get; set; }
        public Guid? parentKey { get; set; }
        public string? row { get; set; }
        public string title { get; set; }
        public string? CreditSourceCode { get; set; }
        public List<CreditSourceDto> children { get; set; }=new List<CreditSourceDto>();
    }
}
