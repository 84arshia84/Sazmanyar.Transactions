using AppCore.Entities.DesignCodesProperties.ContractDesignCodes;
using AppCore.Entities.DesignCodesProperties.FactorDesignCodes;
using AppCore.Entities.SettingEntities.ContractTypes;
using InfrStructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.DesignCodeRepository
{
    internal class FactorDesignCodeRepository : IFactorDesignCodeRepository
    {
        private readonly AppDbContext _context;
        public FactorDesignCodeRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task Add(FactorDesignCode contractDesignCode)
        {
            try
            {
                await _context.FactorDesignCodes.AddAsync(contractDesignCode);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task AddParameters(List<FactorDesignCodeParameterRel> parameters)
        {
            try
            {
                await _context.FactorDesignCodeParameterRels.AddRangeAsync(parameters);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task Delete(Guid id)
        {
            try
            {
                var model = await Get(id);
                if (model != null)
                {
                    _context.FactorDesignCodes.Remove(model);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteParameters(Guid id)
        {
            try
            {
                var param = await GetAllParameters(id);
                if (param != null && param.Count > 0)
                {
                    _context.FactorDesignCodeParameterRels.RemoveRange(param);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<FactorDesignCode> Get(Guid id)
        {
            try
            {
                return await _context.FactorDesignCodes.FindAsync(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<FactorDesignCode>> GetAll()
        {
            try
            {
                return await _context.FactorDesignCodes.Include(c => c.Parameters).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<FactorDesignCodeParameterRel>> GetAllParameters(Guid id)
        {
            try
            {
                return await _context.FactorDesignCodeParameterRels.Where(c => c.FactorDesignCodeId == id).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<FactorDesignCodeParameterRel>> GetAllParameters()
        {
            try
            {
                return await _context.FactorDesignCodeParameterRels.ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<FactorDesignCode> GetByParameter(Guid factorTypeId, Guid organizationUnitId, Guid roleOfOrganizationId)
        {
            try
            {
                var model = await _context.FactorDesignCodeParameterRels.Where(x => x.FactorTypeId == factorTypeId && x.RoleOfOrganizationId == roleOfOrganizationId && x.OrganizationUnitId == organizationUnitId).FirstOrDefaultAsync();
                if (model != null)
                {
                    return await Get(model.FactorDesignCodeId);
                }
                return new FactorDesignCode();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task Update(FactorDesignCode factorDesignCode)
        {
            try
            {
                var model = await Get(factorDesignCode.Id);
                if (model != null)
                {
                    model.Preview = factorDesignCode.Preview;
                    model.DesignCode = factorDesignCode.DesignCode;
                    model.ParameterPreview = factorDesignCode.ParameterPreview;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdateCounter(Guid id)
        {
            try
            {
                var model = await Get(id);
                if (model != null)
                {
                    model.Counter += 1;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
