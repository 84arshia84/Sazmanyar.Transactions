using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.SettingEntities.UnitOfMeasurements
{
    public interface IUnitOfMeasurementRepository
    {
        public Task<(string message, bool isSuccss)> Add(UnitOfMeasurement unit);
        public Task<(string message, bool isSuccss)> Update(UnitOfMeasurement unit);
        public Task<(string message, bool isSuccss)> Delete(Guid currencyId);
        public Task<List<UnitOfMeasurement>> GetAll();
        public Task<UnitOfMeasurement> Get(Guid unitId);
    }
}
