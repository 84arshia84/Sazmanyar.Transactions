using AppCore.Entities.InvoiceInformations.InvoiceAmounts;
using InfrStructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.InvoiceInformationsRepository
{
    internal class InvoiceAmountRepository : IInvoiceAmountRepository
    {
        private readonly AppDbContext _context;
        public InvoiceAmountRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddInvoiceAmount(InvoiceAmount invoiceAmount)
        {
            try
            {
                await _context.InvoiceAmount.AddAsync(invoiceAmount);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<InvoiceAmount>> GetAllInvoiceAmount()
        {
            try
            {
                return await _context.InvoiceAmount.Include(x=>x.Currency).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<List<InvoiceAmount>> GetAllInvoiceAmount(Guid invoiceId)
        {
            try
            {
                return await _context.InvoiceAmount.Where(x => x.InvocieBaseInformationId == invoiceId).Include(x => x.Currency).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<InvoiceAmount> GetInvoiceAmount(Guid invoiceId)
        {
            try
            {
                return await _context.InvoiceAmount.Include(x => x.Currency).FirstOrDefaultAsync(x => x.InvocieBaseInformationId == invoiceId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdateApprovedAmount(decimal amount, Guid invoiceId,Guid CurrencyId)
        {
            try
            {
                var model = await _context.InvoiceAmount.FirstOrDefaultAsync(x => x.InvocieBaseInformationId == invoiceId && x.CurrencyId== CurrencyId);
                if (model != null)
                {
                    model.ApprovedAmount = amount;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task DeleteAmounts(Guid invoiceId)
        {
            try
            {
                var model = await GetAllInvoiceAmount(invoiceId);
                if (model != null && model.Count>0)
                {
                    _context.InvoiceAmount.RemoveRange(model);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task DeleteApprovedAmount(Guid invoiceId)
        {
            try
            {
                var model = await GetInvoiceAmount(invoiceId);
                if (model != null)
                {
                    _context.InvoiceAmount.Remove(model);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task UpdateNettingAmount(decimal amount, Guid invoiceId, Guid CurrencyId)
        {
            try
            {
                var model = await _context.InvoiceAmount.FirstOrDefaultAsync(x => x.InvocieBaseInformationId == invoiceId && x.CurrencyId == CurrencyId);
                if (model != null)
                {
                    model.NettingAmount = amount;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task UpdateRequestedAmount(decimal amount, Guid invoiceId, Guid CurrencyId)
        {
            try
            {
                var model = await _context.InvoiceAmount.FirstOrDefaultAsync(x => x.InvocieBaseInformationId == invoiceId && x.CurrencyId == CurrencyId);
                if (model != null)
                {
                    model.RequestedAmount = amount;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
