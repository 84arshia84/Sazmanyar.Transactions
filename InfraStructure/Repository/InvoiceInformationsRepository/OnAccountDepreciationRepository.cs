using AppCore.Entities.InvoiceInformations.OnAccountDepreciations;
using AppCore.Entities.InvoiceInformations.PrePaymentDepreciations;
using InfrStructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.InvoiceInformationsRepository
{
    internal class OnAccountDepreciationRepository : IOnAccountDepreciationRepository
    {
        private readonly AppDbContext _context;
        public OnAccountDepreciationRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Add(OnAccountDepreciation onAccountDepreciation)
        {
            try
            {
                await _context.OnAccountDepreciations.AddAsync(onAccountDepreciation);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<OnAccountDepreciation> Get(Guid id)
        {
            try
            {
                return await _context.OnAccountDepreciations.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<OnAccountDepreciation>> GetAllByFinancialId(Guid financialId)
        {
            try
            {
                return await _context.OnAccountDepreciations.Where(x => x.ServiceExplenationFinancialId == financialId && x.IsDeleted == false).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<OnAccountDepreciation>> GetAllByInvoiceId(Guid invoiceId)
        {
            try
            {
                return await _context.OnAccountDepreciations.Where(x => x.InvoiceId == invoiceId && x.IsDeleted == false).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<OnAccountDepreciation>> GetAllByServiceExplenationId(Guid explenationId)
        {
            try
            {
                return await _context.OnAccountDepreciations.Where(x => x.ServiceExplenationId == explenationId && x.IsDeleted == false).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// تمامی استهلاکات متعلق به یک قرارداد
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<OnAccountDepreciation>> GetAllForContract(Guid contractId)
        {
            try
            {
                var invoices = await _context.InvoiceBaseInformations.Where(x=>x.ContractId == contractId).ToListAsync();
                return await _context.OnAccountDepreciations.Where(x=>invoices.Select(y=>y.Id).Contains(x.InvoiceId) && x.IsDeleted==false).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<OnAccountDepreciation> GetByFinancialId(Guid id)
        {
            try
            {
                return await _context.OnAccountDepreciations.FirstOrDefaultAsync(x => x.ServiceExplenationFinancialId == id && x.IsDeleted == false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Update(OnAccountDepreciation onAccountDepreciation)
        {
            try
            {
                var model = await Get(onAccountDepreciation.Id);
                if (model != null)
                {
                    _context.Entry(model).CurrentValues.SetValues(onAccountDepreciation);
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
