using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.SettingEntities.CreditSources
{
    public interface ICreditSourceRepository
    {
        #region crud
        public Task<Tuple<string, bool>> Add(CreditSource creditSource);
        public Task<CreditSource> Get(Guid id);
        public Task<List<CreditSource>> GetAll();
        public Task<Tuple<string, bool>> Update(CreditSource creditSource);
        public Task<Tuple<string, bool>> Delete(Guid id);
        #endregion
    }
}
