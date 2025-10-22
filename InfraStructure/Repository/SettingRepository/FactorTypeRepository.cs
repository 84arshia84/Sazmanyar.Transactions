using AppCore.Entities.SettingEntities.ContractTypes;
using AppCore.Entities.SettingEntities.FactorTypes;
using InfrStructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.SettingRepository
{
    internal class FactorTypeRepository : IFactorTypeRepository
    {
        private readonly AppDbContext _context;
        public FactorTypeRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Add(FactorType factorType)
        {
            try
            {
                if (!FactorTypeCrudValidation.CheckDuplicate(_context, factorType))
                {
                    await _context.FactorTypes.AddAsync(factorType);
                    return true; 
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Delete(Guid factorType)
        {
            try
            {
                if (!FactorTypeCrudValidation.CheckIsItUsed(_context, factorType))
                {
                    var model = await Get(factorType);
                    _context.FactorTypes.Remove(model);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<FactorType> Get(Guid factorType)
        {
            try
            {
                return await _context.FactorTypes.FirstOrDefaultAsync(x => x.ID == factorType);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<FactorType>> GetAll()
        {
            try
            {
                return await _context.FactorTypes.ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Update(FactorType factorType)
        {
            try
            {
                var model = await Get(factorType.ID);
                if (model != null)
                {
                    _context.Entry(model).CurrentValues.SetValues(factorType);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> UpdateOfficeOnline(Guid factorTypeId, Guid filenameId)
        {
            try
            {
                var model = await _context.FactorTypes.FirstOrDefaultAsync(x => x.ID == factorTypeId);
                if (model != null)
                {
                    model.OfficeOnlineDocumentId = filenameId;
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
