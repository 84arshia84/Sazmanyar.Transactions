using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.SettingDtos
{
    public class DefaultCoefficientsDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal? DefaultCoefficient { get; set; }
        public string? DefaultRows { get; set; }
        public int row {  get; set; }
    }
}
