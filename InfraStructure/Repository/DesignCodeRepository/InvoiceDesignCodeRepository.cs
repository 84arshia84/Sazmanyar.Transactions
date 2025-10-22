using AppCore.Entities.DesignCodesProperties.ContractDesignCodes;
using AppCore.Entities.DesignCodesProperties.InvoiceDesignCodes;
using InfrStructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.DesignCodeRepository
{
    internal class InvoiceDesignCodeRepository : IInvoiceDesignCodeRepository
    {
        private readonly AppDbContext _context;
        public InvoiceDesignCodeRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task Add(InvoiceDesignCode contractDesignCode)
        {
            try
            {
                await _context.InvoiceDesignCodes.AddAsync(contractDesignCode);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task AddParameters(List<InvoiceDesignCodeParameterRel> parameters)
        {
            try
            {
                await _context.InvoiceDesignCodeParameterRels.AddRangeAsync(parameters);
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
                    _context.InvoiceDesignCodes.Remove(model);
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
                    _context.InvoiceDesignCodeParameterRels.RemoveRange(param);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<InvoiceDesignCode> Get(Guid id)
        {
            try
            {
                return await _context.InvoiceDesignCodes.FindAsync(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<InvoiceDesignCode>> GetAll()
        {
            try
            {
                return await _context.InvoiceDesignCodes.Include(c => c.Parameters).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<InvoiceDesignCodeParameterRel>> GetAllParameters(Guid id)
        {
            try
            {
                return await _context.InvoiceDesignCodeParameterRels.Where(c => c.InvoiceDesignCodeId == id).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<InvoiceDesignCodeParameterRel>> GetAllParameters()
        {
            try
            {
                return await _context.InvoiceDesignCodeParameterRels.ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<InvoiceDesignCode> GetByParameter(Guid contractTypeId, Guid organizationUnitId, Guid roleOfOrganizationId, Guid inovieTypeId)
        {
            try
            {
                var model = await _context.InvoiceDesignCodeParameterRels.Where(x => x.ContractTypeId == contractTypeId &&
                x.RoleOfOrganizationId == roleOfOrganizationId &&
                x.OrganizationUnitId == organizationUnitId &&
                x.InvoiceTypeId == inovieTypeId
                ).FirstOrDefaultAsync();
                if (model != null)
                {
                    return await Get(model.InvoiceDesignCodeId);
                }
                return new InvoiceDesignCode();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task Update(InvoiceDesignCode invoiceDesignCode)
        {
            try
            {
                var model = await Get(invoiceDesignCode.Id);
                if (model != null)
                {
                    model.Preview = invoiceDesignCode.Preview;
                    model.DesignCode = invoiceDesignCode.DesignCode;
                    model.ParameterPreview = invoiceDesignCode.ParameterPreview;
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
