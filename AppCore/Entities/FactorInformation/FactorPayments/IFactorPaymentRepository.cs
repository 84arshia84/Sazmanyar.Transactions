using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.FactorInformation.FactorPayments
{
    public interface IFactorPaymentRepository
    {
        public Task<bool> Add(FactorPayment payment);
        public Task<FactorPayment> Get(Guid paymentId);
        public Task<bool> Delete(Guid paymentId, Guid UserId);
        public Task<bool> Update(FactorPayment payment);
        public Task<List<FactorPayment>> GetAll(Guid factorId);
    }
}
