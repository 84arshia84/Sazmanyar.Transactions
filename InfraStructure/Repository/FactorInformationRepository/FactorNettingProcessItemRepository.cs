using AppCore.Entities.FactorInformation.FactorNettingProcessItems;
using InfrStructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.FactorInformationRepository
{
    internal class FactorNettingProcessItemRepository : IFactorNettingProcessItemRepository
    {
        private readonly AppDbContext _context;
        public FactorNettingProcessItemRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Add(FactorNettingProcessItem nettingProcessItem)
        {
            try
            {
                await _context.FactorNettingProcesses.AddAsync(nettingProcessItem);
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Add(List<FactorNettingProcessItem> nettingProcessItem)
        {
            try
            {
                await _context.FactorNettingProcesses.AddRangeAsync(nettingProcessItem);
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Delete(Guid nettingProcessItemId,Guid userId)
        {
            try
            {
                var model = await Get(nettingProcessItemId);
                if (model != null)
                {
                    model.IsDeleted = true;
                    model.DeleteDate = DateTime.Now;
                    model.DeleteBy = userId;
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<FactorNettingProcessItem> Get(Guid nettingProcessItemId)
        {
            try
            {
                return await _context.FactorNettingProcesses.FirstOrDefaultAsync(x=>x.Id== nettingProcessItemId && x.IsDeleted==false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<FactorNettingProcessItem>> GetAll(Guid factorId)
        {
            try
            {
                return await _context.FactorNettingProcesses.Where(x => x.FactorId == factorId && x.IsDeleted == false).OrderBy(x=>x.CurrencyId).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Update(FactorNettingProcessItem nettingProcessItem)
        {
            try
            {
                var model = await Get(nettingProcessItem.Id);
                if(model != null)
                {
                    _context.Entry(model).CurrentValues.SetValues(nettingProcessItem);
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
