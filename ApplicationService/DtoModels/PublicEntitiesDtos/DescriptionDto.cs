using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.PublicEntitiesDtos
{
    public class DescriptionDto
    {
        public Guid Id { get; set; }
        public string AuthorName { get; set; }
        public string AuthorFullQualifyName { get; set; }
        public string WriteTime { get; set; }
        public DateTime CreateTime { get; set; }
        public string Text { get; set; } = string.Empty;
        public Guid SectionId { get; set; }
        public bool? IsUpdated { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
