using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.SettingEntities.Activitycenters
{
    public interface IActivitycenterRepository
    {
        #region Crud
        public Task<Tuple<string, bool>> Add(Activitycenter activitycenter);
        public  Task<Activitycenter> Get(Guid id);
        public Task<List<Activitycenter>> GetAll(string query,string connectionString);
        public Task<Tuple<string, bool>> Update(Activitycenter activitycenter);
        public Task<Tuple<string, bool>> Delete(Guid id);
        #endregion
    }
}
