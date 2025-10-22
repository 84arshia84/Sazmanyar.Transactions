using AppCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.SettingDtos
{
    public class AddendumTypeDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int AddendumChangeType { get; set; }
        public int Row {  get; set; }
    }
}
