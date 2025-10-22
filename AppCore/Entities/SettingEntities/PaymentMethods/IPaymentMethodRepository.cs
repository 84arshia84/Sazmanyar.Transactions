using AppCore.Entities.SettingEntities.Currencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.SettingEntities.PaymentMethods
{
    public interface IPaymentMethodRepository
    {
        public Task<(string message, bool isSuccss)> Add(PaymentMethod paymentMethod);
        public Task<(string message, bool isSuccss)> Update(PaymentMethod paymentMethod);
        public Task<(string message, bool isSuccss)> Delete(Guid paymentMethodId);
        public Task<List<PaymentMethod>> GetAll();
        public Task<PaymentMethod> Get(Guid paymentMethodId);
    }
}
