using AppCore.Entities.SettingEntities.CheckLists;
using AppCore.Entities.SettingEntities.LookUpTables;
using InfrStructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.SettingRepository
{
    internal class LookUpTableRepository : ILookUpTableRepository
    {
        private readonly AppDbContext _context;
        public LookUpTableRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Add(LookUpTable lookUpTable)
        {
            try
            {
                if(!LookUpTableCrudValidation.CheckDuplicate(_context, lookUpTable))
                {
                    await _context.LookUpTables.AddAsync(lookUpTable);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> AddInside(LookUpTableInside lookUpTableInside)
        {
            try
            {
                if(!LookUpTableCrudValidation.CheckDuplicate(_context, lookUpTableInside))
                {
                    await _context.LookUpTablesInside.AddAsync(lookUpTableInside);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> Delete(Guid lookUpTable)
        {
            try
            {
                var model = await Get(lookUpTable);
                if(model != null)
                {
                    _context.LookUpTables.Remove(model);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> DeleteInside(Guid lookUpTableInside)
        {
            try
            {
                var model = await GetInside(lookUpTableInside);
                if(model != null)
                {
                    _context.LookUpTablesInside.Remove(model);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<LookUpTable> Get(Guid lookUpTable)
        {
            try
            {
                return await _context.LookUpTables.FirstOrDefaultAsync(x => x.ID == lookUpTable);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<LookUpTable>> GetAll()
        {
            try
            {
                return await _context.LookUpTables.Include(x=>x.LookUpTableInsides).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async  Task<List<LookUpTableInside>> GetAllInside(Guid lookUpId)
        {
            try
            {
                return await _context.LookUpTablesInside.Where(x=>x.LookUpTableId== lookUpId).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<LookUpTableInside> GetInside(Guid lookUpTableInside)
        {
            try
            {
                return await _context.LookUpTablesInside.FirstOrDefaultAsync(x => x.ID == lookUpTableInside);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async  Task<bool> Update(LookUpTable lookUpTable)
        {
            try
            {
                if (!LookUpTableCrudValidation.CheckDuplicate(_context, lookUpTable))
                {
                    var model = await Get(lookUpTable.ID);
                    _context.Entry(model).CurrentValues.SetValues(lookUpTable);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> UpdateInside(LookUpTableInside lookUpTableInside)
        {
            try
            {
                if (!LookUpTableCrudValidation.CheckDuplicate(_context, lookUpTableInside))
                {
                    var model = await GetInside(lookUpTableInside.ID);
                    _context.Entry(model).CurrentValues.SetValues(lookUpTableInside);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
