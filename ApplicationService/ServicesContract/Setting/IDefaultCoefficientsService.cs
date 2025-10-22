using AppCore.Entities.SettingEntities.DefaultCoefficients;
using ApplicationService.DtoModels.SettingDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.Setting
{
    public  interface IDefaultCoefficientsService
    {
        public Task<(string message, bool isSuccess)> Add(DefaultCoefficientsDto defaultCoefficients);
        public Task<(string message, bool isSuccess)> Update(DefaultCoefficientsDto defaultCoefficients);
        public Task<List<DefaultCoefficientsDto>> GetAll();
        public Task<DefaultCoefficientsDto> Get(Guid id);
        public Task<(string message, bool isSuccess)> Delete(Guid id);
    }
}
