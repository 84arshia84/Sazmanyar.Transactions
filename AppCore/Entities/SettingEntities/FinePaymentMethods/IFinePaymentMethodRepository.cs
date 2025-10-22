using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.SettingEntities.FinePaymentMethods
{
    public interface IFinePaymentMethodRepository
    {
        #region crud
        public Task<Tuple<string, bool>> Add(FinePaymentMethod finePaymentMethod);
        public Task<FinePaymentMethod> Get(Guid id);
        public Task<List<FinePaymentMethod>> GetAll();
        public Task<Tuple<string, bool>> Update(FinePaymentMethod finePaymentMethod);
        public Task<Tuple<string, bool>> Delete(Guid id);
        #endregion
    }
}
