using AppCore.Entities.FactorInformation.FactorPayments;
using InfrStructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.FactorInformationRepository
{
    internal class FactorPaymentRepository : IFactorPaymentRepository
    {
        private readonly AppDbContext _context;
        public FactorPaymentRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Add(FactorPayment payment)
        {
            try
            {
                await _context.FactorPayments.AddAsync(payment);
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Delete(Guid paymentId,Guid UserId)
        {
            try
            {
                var model = await Get(paymentId);
                if (model != null)
                {
                    model.IsDeleted= true;
                    model.DeleteDate = DateTime.Now;
                    model.DeleteBy = UserId;
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<FactorPayment> Get(Guid paymentId)
        {
            try
            {
                return await _context.FactorPayments.FirstOrDefaultAsync(x => x.Id == paymentId && x.IsDeleted==false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<FactorPayment>> GetAll(Guid factorId)
        {
            try
            {
                return await _context.FactorPayments.Where(x=>x.FactorId == factorId && x.IsDeleted == false).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<bool> Update(FactorPayment payment)
        {
            try
            {
                var model = await Get(payment.Id);
                if (model != null)
                {
                    _context.Entry(model).CurrentValues.SetValues(payment);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
