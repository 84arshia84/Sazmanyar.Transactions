using AppCore.Entities.SettingEntities.AddendumTypes;
using ApplicationService.DtoModels.SettingDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.Setting
{
    public interface IAddendumTypeService
    {
        public Task<(string message,bool isSuccess)> Add(AddendumTypeDto addendumType);
        public Task<(string message, bool isSuccess)> Update(AddendumTypeDto addendumType);
        public Task<List<AddendumTypeDto>> GetAll();
        public Task<AddendumTypeDto> Get(Guid id);
        public Task<(string message, bool isSuccess)> Delete(Guid id);
    }
}
