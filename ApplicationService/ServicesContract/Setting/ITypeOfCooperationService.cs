using ApplicationService.DtoModels.SettingDtos;
using AppCore.Entities.SettingEntities.TransActionTypes;
using AppCore.Entities.SettingEntities.TypeOfCooperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.Setting
{
    public interface ITypeOfCooperationService
    {
        public Task<Tuple<string, bool>> Add(TypeOfCooperationDto typeOfCooperation);
        public Task<List<TypeOfCooperationDto>> GetAll();
        public Task<Tuple<string, bool>> Update(TypeOfCooperationDto typeOfCooperation);
        public Task<Tuple<string, bool>> Delete(Guid typeOfCooperation);
    }
}
