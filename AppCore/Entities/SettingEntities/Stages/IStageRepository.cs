using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.SettingEntities.Stages
{
    public interface IStageRepository
    {
        public Task<List<Stage>> GetAll(Guid moduleId, string connectionString);
    }
}
