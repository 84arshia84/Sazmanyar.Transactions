using AppCore.Entities.FactorInformation.FactorServiceExplanations;
using InfrStructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.FactorInformationRepository
{
    internal class FactorServiceExplanationRepository : IFactorServiceExplanationRepository
    {
        private readonly AppDbContext _context;
        public FactorServiceExplanationRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Add(List<FactorServiceExplanation> serviceExplanation)
        {
            try
            {
                await _context.FactorServiceExplanations.AddRangeAsync(serviceExplanation);
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Add(FactorServiceExplanation serviceExplanation)
        {
            try
            {
                await _context.FactorServiceExplanations.AddAsync(serviceExplanation);
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var model = await Get(id);
                if(model != null)
                {
                    _context.FactorServiceExplanations.Remove(model);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<FactorServiceExplanation> Get(Guid id)
        {
            try
            {
                return await _context.FactorServiceExplanations.FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<FactorServiceExplanation>> GetAll(Guid factorId)
        {
            try
            {
                return await _context.FactorServiceExplanations.Where(x=>x.FactorId == factorId).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Update(FactorServiceExplanation serviceExplanation)
        {
            try
            {
                var model = await Get(serviceExplanation.Id);
                if (model != null)
                {
                    //_context.ServiceExplanations.Update(serviceExplanation);
                    _context.Entry(model).CurrentValues.SetValues(serviceExplanation);
                    return true;
                }
                return await Add(serviceExplanation);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
