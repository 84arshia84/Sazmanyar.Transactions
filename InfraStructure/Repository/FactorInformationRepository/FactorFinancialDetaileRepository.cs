using AppCore.Entities.FactorInformation.FactorFinancialDetailes;
using AppCore.Entities.FactorInformation.FactorTimeProfiles;
using InfrStructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.FactorInformationRepository
{
    internal class FactorFinancialDetaileRepository : IFactorFinancialDetaileRepository
    {
        private readonly AppDbContext _context;
        public FactorFinancialDetaileRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Add(FactorFinancialDetaile factorFinancialDetaile)
        {
            try
            {
                await _context.FactorFinancialDetailes.AddAsync(factorFinancialDetaile);
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<FactorFinancialDetaile> Get(Guid factorId)
        {
            try
            {
                return await _context.FactorFinancialDetailes.FirstOrDefaultAsync(x=>x.FactorId ==factorId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Update(FactorFinancialDetaile factorFinancialDetaile)
        {
            try
            {
                var model = await Get(factorFinancialDetaile.FactorId);
                if (model != null)
                {
                    _context.Entry(model).CurrentValues.SetValues(factorFinancialDetaile);
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
