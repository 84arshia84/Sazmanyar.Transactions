using ApplicationService.DtoModels.SettingDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.Setting
{
    public interface IPaymentMethodService
    {
        public Task<(string message, bool isSuccss)> Add(PaymentMethodDto paymentMethod);
        public Task<(string message, bool isSuccss)> Update(PaymentMethodDto paymentMethod);
        public Task<(string message, bool isSuccss)> Delete(Guid paymentMethodId);
        public Task<List<PaymentMethodDto>> GetAll();
        public Task<PaymentMethodDto> Get(Guid paymentMethodId);
    }
}
