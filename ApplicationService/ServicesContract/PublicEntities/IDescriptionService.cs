using AppCore.Entities.Descriptions;
using ApplicationService.DtoModels.PublicEntitiesDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.PublicEntities
{
    public interface IDescriptionService
    {
        public Task Add(DescriptionDto description,bool autosave,Guid? sectionId=null);
        public Task Add(List<DescriptionDto> description, bool autosave, Guid? sectionId = null);
        public Task Update(List<DescriptionDto> description, bool autosave, Guid? sectionId = null);
        public Task Update(DescriptionDto description,bool autosave);
        public Task<List<DescriptionDto>> GetAll(Guid sectionId);
        public Task Delete(Guid description, bool autosave);
    }
}
