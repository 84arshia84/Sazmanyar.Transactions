using AppCore.Entities.SettingEntities.TypeOfGuarantees;
using InfrStructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.SettingRepository
{
    internal class TypeOfGuaranteeRepository : ITypeOfGuaranteeRepository
    {
        private readonly AppDbContext _context;
        public TypeOfGuaranteeRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Add(TypeOfGuarantee guarantee)
        {
            try
            {
                if (!TypeOfGuaranteeCrudValidation.CheckDuplicate(_context, guarantee))
                {
                    await _context.TypeOfGuarantees.AddAsync(guarantee);
                    return true;
                }
                return false;
            }
            catch (Exception ex )
            {

                throw ex;
            }
        }

        public async Task<bool> Delete(Guid guarantee)
        {
            try
            {
                var model = await Get(guarantee);
               
                if(model != null)
                {
                    if (!TypeOfCooperationCrudValidation.CheckIsItUsed(_context, guarantee))
                    {
                        _context.TypeOfGuarantees.Remove(model);
                        return true;
                    }
                   
                }
                return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<TypeOfGuarantee> Get(Guid guaranteeId)
        {
            try
            {
                return await _context.TypeOfGuarantees.FirstOrDefaultAsync(x=>x.ID == guaranteeId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<TypeOfGuarantee>> GetAll()
        {
            try
            {
                return await _context.TypeOfGuarantees.OrderBy(x=>x.Order).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> Update(TypeOfGuarantee guarantee)
        {
            try
            {
                var model = await Get(guarantee.ID);
                if(model != null)
                {
                    if (!TypeOfGuaranteeCrudValidation.CheckDuplicate(_context, guarantee))
                    {
                        _context.Entry(model).CurrentValues.SetValues(guarantee);
                        return true;
                    }
                  
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
