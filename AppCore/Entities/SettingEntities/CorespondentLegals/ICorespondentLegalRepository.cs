using AppCore.Entities.SettingEntities.CorespondentLegals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.SettingEntities.CorespondentLegals
{
    public interface ICorespondentLegalRepository
    {
        #region crud
        public Task<Tuple<string, bool>> Add(CorespondentLegal corespondentLegal);
        public Task<CorespondentLegal> Get(Guid id);
        public Task<List<CorespondentLegal>> GetAll();
        public Task<Tuple<string, bool>> Update(CorespondentLegal corespondentLegal);
        public Task<Tuple<string, bool>> Delete(Guid id);
        #endregion
    }
}
