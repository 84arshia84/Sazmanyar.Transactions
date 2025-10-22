using AppCore.Entities.SettingEntities.ReasonForCancellations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.SettingEntities.ReasonForTerminations
{
    public interface IReasonForTerminationRepository
    {
        #region crud
        public Task<Tuple<string, bool>> Add(ReasonForTermination reasonForTermination);
        public Task<ReasonForTermination> Get(Guid id);
        public Task<List<ReasonForTermination>> GetAll();
        public Task<Tuple<string, bool>> Update(ReasonForTermination reasonForTermination);
        public Task<Tuple<string, bool>> Delete(Guid id);
        #endregion
    }
}
