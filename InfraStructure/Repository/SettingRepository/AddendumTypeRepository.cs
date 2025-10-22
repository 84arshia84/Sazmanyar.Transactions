using AppCore.Entities.SettingEntities.AddendumTypes;
using InfrStructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.SettingRepository
{
    internal class AddendumTypeRepository : IAddendumTypeRepository
    {
        private readonly AppDbContext _context;
        public AddendumTypeRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Add(AddendumType addendumType)
        {
            try
            {
                if (!AddendumTypeCrudValidation.CheckDuplicate(_context, addendumType)) 
                {
                    await _context.AddendumTypes.AddAsync(addendumType);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw ;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var model =await Get(id); 
                if (model != null)
                {
                    if (!AddendumTypeCrudValidation.CheckIsItUsed(_context, id))
                    {
                        _context.AddendumTypes.Remove(model);
                        return true;
                    }
                    
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<AddendumType> Get(Guid id)
        {
            try
            {
                return await _context.AddendumTypes.FirstOrDefaultAsync(x => x.ID == id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<AddendumType>> GetAll()
        {
            try
            {
                return await _context.AddendumTypes.OrderBy(x=>x.Order).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Update(AddendumType addendumType)
        {
            try
            {
                var model =await Get(addendumType.ID);
                if (model != null)
                {
                    _context.Entry(model).CurrentValues.SetValues(addendumType);
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
