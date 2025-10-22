using AppCore.Entities.InvoiceInformations.EstimatedMeterFinancials;
using AppCore.Entities.InvoiceInformations.ServiceExplanationFinancials;
using InfrStructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.InvoiceInformationsRepository
{
    internal class EstimatedMeterFinancialRepository : IEstimatedMeterFinancialRepository
    {
        private readonly AppDbContext _context;
        public EstimatedMeterFinancialRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<(string message, bool isSuccess)> Add(EstimatedMeterFinancial estimatedMeterFinancial)
        {
            try
            {
                await _context.EstimatedMeterFinancials.AddAsync(estimatedMeterFinancial);
                await _context.SaveChangesAsync();
                return ("اطلاعات با موفقیت ثبت شد.", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<(string message, bool isSuccess)> Delete(Guid estimatedMeterFinancialId)
        {
            try
            {
                var existingEstimatedMeterFinancial = await Get(estimatedMeterFinancialId);
                if (existingEstimatedMeterFinancial != null)
                {
                    await Task.Run(() => _context.EstimatedMeterFinancials.Remove(existingEstimatedMeterFinancial));
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

        public async Task<EstimatedMeterFinancial> Get(Guid estimatedMeterFinancialId)
        {
            try
            {
                var model = await _context.EstimatedMeterFinancials.Where(emf => emf.Id == estimatedMeterFinancialId).FirstOrDefaultAsync();
                if (model != null)
                {
                    return model;
                }
                return new EstimatedMeterFinancial();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<EstimatedMeterFinancial>> GetAllByInvoiceBaseInformationId(Guid invoiceBaseInformationId)
        {
            try
            {
                if (invoiceBaseInformationId != Guid.Empty)
                {
                    var results = await _context.EstimatedMeterFinancials.Where(emf => emf.InvoiceBaseInformationId == invoiceBaseInformationId).ToListAsync();
                    return results;
                }
                else
                {
                    return new List<EstimatedMeterFinancial>();
                }
            }
            catch (Exception ex)
            {
                return new List<EstimatedMeterFinancial>();
            }
        }
    }
}
