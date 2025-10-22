using ApplicationService.DtoModels.PublicEntitiesDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.PublicEntities
{
    public interface IAttachService
    {
        public Task Add(AttachDto attach, bool autosave, Guid? sectionId = null);
        public Task Add(List<AttachDto> attach, bool autosave, Guid? sectionId = null);
        public Task Update(List<AttachDto> attach, bool autosave, Guid? sectionId = null);
        public Task Update(AttachDto attach, bool autosave);
        public Task<List<AttachDto>> GetAll(Guid sectionId);
        public Task Delete(Guid attach, bool autosave);
    }
}
