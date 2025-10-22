using AppCore.Entities.ContractsInformation.ContractCoefficients;
using AppCore.Entities.SettingEntities.DefaultCoefficients;
using InfrStructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.SettingRepository
{
    internal class DefaultCoefficientsRepository : IDefaultCoefficientsRepository
    {
        private readonly AppDbContext _context;
        public DefaultCoefficientsRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Add(DefaultCoefficients defaultCoefficients)
        {
            try
            {
                await _context.DefaultCoefficients.AddAsync(defaultCoefficients);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var model = await Get(id);
                if (model != null)
                {
                    _context.DefaultCoefficients.Remove(model);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return  false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DefaultCoefficients> Get(Guid id)
        {
            try
            {
                return await _context.DefaultCoefficients.FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DefaultCoefficients>> GetAll()
        {
            try
            {
                return await _context.DefaultCoefficients.OrderBy(x=>x.Order).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Update(DefaultCoefficients defaultCoefficients)
        {
            try
            {
                _context.DefaultCoefficients.Update(defaultCoefficients);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
