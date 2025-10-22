using ApplicationService.DtoModels.SettingDtos;
using AppCore.Entities.SettingEntities.ReleaseConditions;
using AppCore.Entities.SettingEntities.TransActionTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.Setting
{
    public interface ITransActionTypeService
    {
        public Task<Tuple<string, bool>> Add(TransActionTypeDto transActionType);
        public Task<List<TransActionTypeDto>> GetAll();
        public Task<Tuple<string, bool>> Update(TransActionTypeDto transActionType);
        public Task<Tuple<string, bool>> Delete(Guid transActionType);
    }
}
