using AppCore.Entities.ContractsInformation.ContratTimeProfiles;
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
    internal class FactorTimeProfileRepository : IFactorTimeProfileRepository
    {
        private readonly AppDbContext _context;
        public FactorTimeProfileRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Add(FactorTimeProfile factorTimeProfile)
        {
            try
            {
                await _context.FactorTimeProfiles.AddAsync(factorTimeProfile);
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<FactorTimeProfile> Get(Guid factorId)
        {
            try
            {
                return await _context.FactorTimeProfiles.FirstOrDefaultAsync(x=>x.FactorId == factorId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Update(FactorTimeProfile factorTimeProfile)
        {
            try
            {
                var model = await Get(factorTimeProfile.FactorId);
                if (model != null)
                {
                    _context.Entry(model).CurrentValues.SetValues(factorTimeProfile);
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
