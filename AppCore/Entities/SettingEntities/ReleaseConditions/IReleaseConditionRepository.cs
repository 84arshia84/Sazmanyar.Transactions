using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.SettingEntities.ReleaseConditions
{
    public interface IReleaseConditionRepository
    {
        #region crud
        public Task<Tuple<string, bool>> Add(ReleaseCondition releaseCondition);
        public Task<ReleaseCondition> Get(Guid id);
        public Task<List<ReleaseCondition>> GetAll();
        public Task<Tuple<string, bool>> Update(ReleaseCondition releaseCondition);
        public Task<Tuple<string, bool>> Delete(Guid id);
        #endregion
    }
}
