using AppCore.Entities.InvoiceInformations.ServiceExplanationFinancials;
using InfraStructure.Repository.SettingRepository;
using InfrStructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.InvoiceInformationsRepository
{
    internal class ServiceExplanationFinancialRepository : IServiceExplanationFinancialRepository
    {
        private readonly AppDbContext _context;
        public ServiceExplanationFinancialRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ServiceExplanationFinancial>> GetAllByInvoiceBaseInformationId(Guid invoiceBaseInformationId)
        {
            try
            {
                if (invoiceBaseInformationId != Guid.Empty)
                {
                    var results = await _context.ServiceExplanationFinancial.Where(sef => sef.InvoiceBaseInformationId == invoiceBaseInformationId).ToListAsync();
                    return results;
                }
                return new List<ServiceExplanationFinancial>();
            }
            catch (Exception ex)
            {
                return new List<ServiceExplanationFinancial>();
            }
        }
        public async Task<ServiceExplanationFinancial> Get(Guid serviceExplanationFinancialId)
        {
            try
            {
                var model = await _context.ServiceExplanationFinancial.Where(sef => sef.Id == serviceExplanationFinancialId).FirstOrDefaultAsync();
                if (model != null)
                {
                    return model;
                }
                return new ServiceExplanationFinancial();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<(string message, bool isSuccess)> Add(ServiceExplanationFinancial serviceExplanationFinancial)
        {
            try
            {
                await _context.ServiceExplanationFinancial.AddAsync(serviceExplanationFinancial);
                await _context.SaveChangesAsync();
                return ("اطلاعات با موفقیت ثبت شد.", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<(string message, bool isSuccess)> Update(ServiceExplanationFinancial serviceExplanationFinancial)
        {
            try
            {
                var model = await Get(serviceExplanationFinancial.Id);
                if (model != null)
                {
                    model.RequestedVolume = serviceExplanationFinancial.RequestedVolume;
                    model.RequestedPercent = serviceExplanationFinancial.RequestedPercent;
                    model.RequestedPrice = serviceExplanationFinancial.RequestedPrice;
                    await _context.SaveChangesAsync();
                    return ("ویرایش با موفقیت انجام شد", true);
                }
                return ("مورد مد نظر جهت ویرایش وجود ندارد.", false);
            }
            catch (Exception ex)
            {

                return ("ویرایش با خطا روبرو شد.", false);
            }
        }
        public async Task<(string message, bool isSuccess)> Delete(Guid serviceExplanationFinancialId)
        {
            try
            {
                var existingServiceExplanationFinancial = await Get(serviceExplanationFinancialId);
                if (existingServiceExplanationFinancial != null)
                {
                    await Task.Run(() => _context.ServiceExplanationFinancial.Remove(existingServiceExplanationFinancial));
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
        public async Task<(string message, bool isSuccess)> DeleteByInvoiceId(Guid InvoiceId)
        {
            try
            {
                var existingServiceExplanationFinancial = await GetAllByInvoiceBaseInformationId(InvoiceId);
                if (existingServiceExplanationFinancial != null)
                {
                    await Task.Run(() => _context.ServiceExplanationFinancial.RemoveRange(existingServiceExplanationFinancial));
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

        public async Task<List<ServiceExplanationFinancial>> GetAllByInvoiceType(Guid serviceExplenationId, Guid InvoiceTypeId, DateTime date)
        {
            try
            {
                var result = await _context.InvoiceBaseInformations
                            .Where(i => i.InvoiceTypeId == InvoiceTypeId && !i.IsDeleted && i.InsertDate < date)
                            .SelectMany(i => i.ServiceExplanationFinancials
                            .Where(sef => sef.ServiceExplanationId == serviceExplenationId))
                            .ToListAsync();
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<ServiceExplanationFinancial>> GetAllByInvoiceType(List<Guid> serviceExplenationId, Guid InvoiceTypeId, DateTime date)
        {
            try
            {
                var result = await _context.InvoiceBaseInformations
                            .Where(i => i.InvoiceTypeId == InvoiceTypeId && !i.IsDeleted && i.InsertDate < date)
                            .SelectMany(i => i.ServiceExplanationFinancials
                            .Where(sef => serviceExplenationId.Contains(sef.ServiceExplanationId)))
                            .ToListAsync();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<ServiceExplanationFinancial>> GetAllBeforThisInvoiceBaseInformationId(Guid invoiceBaseInformationId)
        {
            try
            {
                var targetInvoice = _context.InvoiceBaseInformations.FirstOrDefault(i => i.Id == invoiceBaseInformationId);
                if (targetInvoice == null) return null ; // or handle not found case
                var serviceExplanations = _context.ServiceExplanationFinancial
                    .Include(se => se.InvoiceBaseInformation) // if you need invoice data
                    .Where(se => se.InvoiceBaseInformation.ContractId == targetInvoice.ContractId &&
                                 se.InvoiceBaseInformation.InsertDate < targetInvoice.InsertDate)
                    .ToList();
                return serviceExplanations;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<List<ServiceExplanationFinancial>> GetAllByServiceExplanationId(Guid serviceExplanationId)
        {
            try
            {
                return _context.ServiceExplanationFinancial.Where(sf => sf.ServiceExplanationId == serviceExplanationId).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
