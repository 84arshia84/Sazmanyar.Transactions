using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.SettingEntities.ReasonForCancellations
{
    public interface IReasonForCancellationRepository
    {
        #region crud
        public Task<Tuple<string, bool>> Add(ReasonForCancellation reasonForCancellation);
        public Task<ReasonForCancellation> Get(Guid id);
        public Task<List<ReasonForCancellation>> GetAll();
        public Task<Tuple<string, bool>> Update(ReasonForCancellation reasonForCancellation);
        public Task<Tuple<string, bool>> Delete(Guid id);
        #endregion
    }
}
