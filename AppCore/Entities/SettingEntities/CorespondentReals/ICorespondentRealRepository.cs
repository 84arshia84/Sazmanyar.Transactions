using AppCore.Entities.SettingEntities.CorespondentReals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.SettingEntities.CorespondentReals
{
    public interface ICorespondentRealRepository
    {
        #region crud
        public Task<Tuple<string, bool>> Add(CorespondentReal corespondentReal);
        public Task<CorespondentReal> Get(Guid id);
        public Task<List<CorespondentReal>> GetAll();
        public Task<Tuple<string, bool>> Update(CorespondentReal corespondentReal);
        public Task<Tuple<string, bool>> Delete(Guid id);
        #endregion
    }
}
