using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.SettingEntities.DefaultCoefficients
{
    public interface IDefaultCoefficientsRepository
    {
        public Task<bool> Add(DefaultCoefficients defaultCoefficients);
        public Task<bool> Update(DefaultCoefficients defaultCoefficients);
        public Task<List<DefaultCoefficients>> GetAll();
        public Task<DefaultCoefficients> Get(Guid id);
        public Task<bool> Delete(Guid id);
    }
}
