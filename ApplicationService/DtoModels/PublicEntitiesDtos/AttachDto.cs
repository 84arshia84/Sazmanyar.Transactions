using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.PublicEntitiesDtos
{
    public class AttachDto
    {
        public Guid Id { get; set; }
        public Guid? SharePointId { get; set; }
        public string? UserUploader { get; set; }
        public string? UserUploaderName { get; set; }
        public string FileName { get; set; }
        public string FileExtention { get; set; }
        public DateTime UploadeDate { get; set; }
        public Guid SectionId { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
