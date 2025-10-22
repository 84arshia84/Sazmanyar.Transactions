using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.SettingEntities.ForGuarantees
{
    public interface IForGuaranteeRepository
    {
        #region crud
        public Task<Tuple<string, bool>> Add(ForGuarantee forGuarantee);
        public Task<ForGuarantee> Get(Guid id);
        public Task<List<ForGuarantee>> GetAll();
        public Task<Tuple<string, bool>> Update(ForGuarantee forGuarantee);
        public Task<Tuple<string, bool>> Delete(Guid id);
        #endregion
    }
}
