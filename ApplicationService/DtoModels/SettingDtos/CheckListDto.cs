using AppCore.Enums;
using InfraStructure.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.SettingDtos
{
    public class CheckListDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public bool? IsRequired { get; set; }
        public string Type { get; set; }
        public int? row { get; set; }
        public Guid? LookUpTableId {  get; set; }
        public Guid ContractTypeId { get; set; }
        //public List<AppCore.Enums.SystemParts>? SystemParts { get; set; }
    }
}
