using ApplicationService.DtoModels.FactorDtos.FactorPayment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.FactorInformation
{
    public interface IFactorPaymentService
    {
        public Task<(string message, bool isSuccess)> Add(FactorPaymentAddDto payment,string userName);
        public Task<FactorPaymentGetDto> Get(Guid paymentId);
        public Task<(string message, bool isSuccess)> Delete(Guid paymentId, string userName);
        public Task<(string message, bool isSuccess)> Update(FactorPaymentUpdateDto payment);
        public Task<List<FactorPaymentGetDto>> GetAll(Guid factorId);
    }
}
