using AppCore.Entities.DesignCodesProperties.ContractDesignCodes;
using AppCore.Entities.DesignCodesProperties.TransActionExecutionDesignCodes;
using AppCore.Entities.SettingEntities.RoleOfOrganizations;
using InfrStructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.DesignCodeRepository
{
    internal class TransactionRequestDesignCodeRepository : ITransActionExecutionDesignCodeRepository
    {
        private readonly AppDbContext _context;
        public TransactionRequestDesignCodeRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task Add(TransActionExecutionDesignCode contractDesignCode)
        {
            try
            {
                await _context.TransActionExecutionDesignCodes.AddAsync(contractDesignCode);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task AddParameters(List<TransActionExecutionDesignCodeParameterRel> parameters)
        {
            try
            {
                await _context.TransActionExecutionDesignCodeParameterRels.AddRangeAsync(parameters);
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
                    _context.TransActionExecutionDesignCodes.Remove(model);
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
                    _context.TransActionExecutionDesignCodeParameterRels.RemoveRange(param);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<TransActionExecutionDesignCode> Get(Guid id)
        {
            try
            {
                return await _context.TransActionExecutionDesignCodes.FindAsync(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<TransActionExecutionDesignCode>> GetAll()
        {
            try
            {
                return await _context.TransActionExecutionDesignCodes.Include(c => c.Parameters).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<TransActionExecutionDesignCodeParameterRel>> GetAllParameters(Guid id)
        {
            try
            {
                return await _context.TransActionExecutionDesignCodeParameterRels.Where(c => c.TransactionExecutionDesignCodeId == id).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<TransActionExecutionDesignCodeParameterRel>> GetAllParameters()
        {
            try
            {
                return await _context.TransActionExecutionDesignCodeParameterRels.ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<TransActionExecutionDesignCode> GetByParameter(Guid contractTypeId, Guid organizationUnitId)
        {
            try
            {
                var model = await _context.TransActionExecutionDesignCodeParameterRels.Where(x => x.ContractTypeId == contractTypeId &&
                x.OrganizationUnitId == organizationUnitId).FirstOrDefaultAsync();
                if (model != null)
                {
                    return await Get(model.TransactionExecutionDesignCodeId);
                }
                return new TransActionExecutionDesignCode();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task Update(TransActionExecutionDesignCode transactionDesignCode)
        {
            try
            {
                var model = await Get(transactionDesignCode.Id);
                if (model != null)
                {
                    model.Preview = transactionDesignCode.Preview;
                    model.DesignCode = transactionDesignCode.DesignCode;
                    model.ParameterPreview = transactionDesignCode.ParameterPreview;
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
