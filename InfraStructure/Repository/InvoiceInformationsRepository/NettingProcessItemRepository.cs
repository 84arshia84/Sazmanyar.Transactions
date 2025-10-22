using AppCore.Entities.InvoiceInformations.EstimatedMeterFinancials;
using AppCore.Entities.InvoiceInformations.NettingProcesses;
using InfrStructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.InvoiceInformationsRepository
{
    internal class NettingProcessItemRepository : INettingProcessItemRepository
    {
        private readonly AppDbContext _context;
        public NettingProcessItemRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<(string message, bool isSuccess)> Add(NettingProcessItem nettingProcessItem)
        {
            try
            {
                await _context.NettingProcessItems.AddAsync(nettingProcessItem);
                await _context.SaveChangesAsync();
                return ("با موفقیت ثبت شد.", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<(string message, bool isSuccess)> Delete(Guid nettingProcessItemId)
        {
            try
            {
                var existingNettingProcessItem = await Get(nettingProcessItemId);
                if (existingNettingProcessItem != null)
                {
                    await Task.Run(() => _context.NettingProcessItems.Remove(existingNettingProcessItem));
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
                var existingNettingProcessItem = await GetAll(invoiceId);
                if (existingNettingProcessItem != null)
                {
                    await Task.Run(() => _context.NettingProcessItems.RemoveRange(existingNettingProcessItem));
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
        public async Task<NettingProcessItem> Get(Guid nettingProcessItemId)
        {
            try
            {
                var model = await _context.NettingProcessItems.Where(npi => npi.Id == nettingProcessItemId && npi.IsDeleted == false).FirstOrDefaultAsync();
                if (model != null)
                {
                    return model;
                }
                return new NettingProcessItem();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<NettingProcessItem>> GetAll(Guid invoiceBaseInformationId)
        {
            try
            {
                var model = await _context.NettingProcessItems.Where(npi => npi.InvoiceBaseInformationId == invoiceBaseInformationId && npi.IsDeleted == false).OrderBy(x=>x.CurrencyId).ToListAsync();
                if (model != null)
                {
                    return model;
                }
                return new List<NettingProcessItem>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
