using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.SettingEntities.Organizationalunits
{
    public interface IOrganizationalunitRepository
    {
        #region crud
        public Task<Tuple<string, bool>> Add(Organizationalunit organizationalunit);
        public Task<Organizationalunit> Get(Guid id);
        public Task<List<Organizationalunit>> GetAll();
        public Task<Tuple<string, bool>> Update(Organizationalunit organizationalunit);
        public Task<Tuple<string, bool>> Delete(Guid id);
        #endregion
    }
}
