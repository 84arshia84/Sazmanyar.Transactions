using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Entities.InvoiceInformations.Payments;
using AppCore.Entities.InvoiceInformations.ServiceExplanationFinancials;
using InfrStructure.DataBase;
using Microsoft.EntityFrameworkCore;

namespace InfraStructure.Repository.InvoiceInformationsRepository
{
    internal class PaymentRepository : IPaymentRepository
    {
        private readonly AppDbContext _context;
        public PaymentRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<(string message, bool isSuccess)> Add(Payment payment)
        {
            try
            {
                await _context.Payments.AddAsync(payment);
                await _context.SaveChangesAsync();
                return ("با موفقیت ثبت شد.", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<(string message, bool isSuccess)> Delete(Guid paymentId)
        {
            try
            {
                var existingPayment = await Get(paymentId);
                if (existingPayment != null) 
                {
                    await Task.Run(() => _context.Payments.Remove(existingPayment));
                    await _context.SaveChangesAsync();
                    return ("حذف با موفقیت انجام شد.", true);
                }
                return ("مورد مد نظر جهت حذف وجود ندارد.", false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<(string message, bool isSuccess)> DeleteByInvoiceId(Guid invoiceId)
        {
            try
            {
                var existingPayment = await GetAll(invoiceId);
                if (existingPayment != null)
                {
                    await Task.Run(() => _context.Payments.RemoveRange(existingPayment));
                    await _context.SaveChangesAsync();
                    return ("حذف با موفقیت انجام شد.", true);
                }
                return ("مورد مد نظر جهت حذف وجود ندارد.", false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Payment> Get(Guid paymentId)
        {
            try
            {
                var model = await _context.Payments.Where(p => p.Id == paymentId).FirstOrDefaultAsync();
                if (model != null)
                {
                    return model;
                }
                return new Payment();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<Payment>> GetAll(Guid invoiceBaseInformationId)
        {
            try
            {
                var model = await _context.Payments.Where(p => p.InvoiceBaseInformationId == invoiceBaseInformationId).Include(p => p.Currency).Include(p => p.HowToPay).OrderBy(x=>x.CurrencyId).ToListAsync();
                if (model != null)
                {
                    return model;
                }
                return new List<Payment>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<Payment>> GetAllBeforThisInvoiceBaseInformationId(Guid invoiceBaseInformationId)
        {
            try
            {
                var targetInvoice = _context.InvoiceBaseInformations.FirstOrDefault(i => i.Id == invoiceBaseInformationId);
                if (targetInvoice == null) return null; // or handle not found case
                var payment = _context.Payments
                    .Include(p => p.InvoiceBaseInformation) // if you need invoice data
                    .Where(p => p.InvoiceBaseInformation.ContractId == targetInvoice.ContractId &&
                                 p.InvoiceBaseInformation.InsertDate < targetInvoice.InsertDate)
                    .ToList();
                return payment;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
