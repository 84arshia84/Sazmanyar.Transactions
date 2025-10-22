using AppCore.Entities.DesignCodesProperties.ContractDesignCodes;
using InfrStructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.DesignCodeRepository
{
    internal class ContractDesignCodeRepository : IContractDesignCodeRepository
    {
        private readonly AppDbContext _context;
        public ContractDesignCodeRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task Add(ContractDesignCode contractDesignCode)
        {
            try
            {
                await _context.ContractDesignCodes.AddAsync(contractDesignCode);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task AddParameters(List<ContractDesignCodeParameterRel> parameters)
        {
            try
            {
                await _context.ContractDesignCodeParameterRels.AddRangeAsync(parameters);
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
                    _context.ContractDesignCodes.Remove(model);
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
                    _context.ContractDesignCodeParameterRels.RemoveRange(param);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ContractDesignCode> Get(Guid id)
        {
            try
            {
                return await _context.ContractDesignCodes.FindAsync(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<ContractDesignCode>> GetAll()
        {
            try
            {
                return await _context.ContractDesignCodes.Include(c => c.Parameters).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<ContractDesignCodeParameterRel>> GetAllParameters(Guid id)
        {
            try
            {
                return await _context.ContractDesignCodeParameterRels.Where(c => c.ContractDesignCodeId == id).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<ContractDesignCodeParameterRel>> GetAllParameters()
        {
            try
            {
                return await _context.ContractDesignCodeParameterRels.ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ContractDesignCode> GetByParameter(Guid contractTypeId, Guid organizationUnitId, Guid roleOfOrganizationId)
        {
            try
            {
                var model = await _context.ContractDesignCodeParameterRels.Where(x=>x.ContractTypeId==contractTypeId &&
                x.RoleOfOrganizationId == roleOfOrganizationId &&
                x.OrganizationUnitId == organizationUnitId).FirstOrDefaultAsync();
                if(model != null)
                {
                    return await Get(model.ContractDesignCodeId);
                }
                return new ContractDesignCode();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task Update(ContractDesignCode contractDesignCode)
        {
            try
            {
                var model = await Get(contractDesignCode.Id);
                if (model != null)
                {
                    model.Preview = contractDesignCode.Preview;
                    model.DesignCode = contractDesignCode.DesignCode;
                    model.ParameterPreview = contractDesignCode.ParameterPreview;
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
                    model.Counter +=1;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
