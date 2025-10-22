using ApplicationService.DtoModels.SettingDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.Setting
{
    public interface IUnitOfMeasurementService
    {
        public Task<(string message, bool isSuccss)> Add(UnitOfMeasurementDto unitDto);
        public Task<(string message, bool isSuccss)> Update(UnitOfMeasurementDto unitDto);
        public Task<(string message, bool isSuccss)> Delete(Guid unitDto);
        public Task<List<UnitOfMeasurementDto>> GetAll();
        public Task<UnitOfMeasurementDto> Get(Guid unitDto);
    }
}
