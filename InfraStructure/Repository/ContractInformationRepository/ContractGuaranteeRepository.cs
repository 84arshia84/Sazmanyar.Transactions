using AppCore.Entities.ContractsInformation.ContractGuarantees;
using AppCore.Entities.ContractsInformation.ServiceExplanations;
using InfrStructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.ContractInformationRepository
{
    internal class ContractGuaranteeRepository : IContractGuaranteeRepository
    {
        private readonly AppDbContext _context;
        public ContractGuaranteeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(List<ContractGuarantee> contractGuarantee)
        {
            try
            {
                await _context.AddRangeAsync(contractGuarantee);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> Add(ContractGuarantee contractGuarantee)
        {
            try
            {
                await _context.AddAsync(contractGuarantee);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> Delete(Guid contractGuarantee)
        {
            try
            {
                var model = await Get(contractGuarantee);
                if(model != null)
                {
                    _context.ContractGuarantees.Remove(model);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ContractGuarantee> Get(Guid id)
        {
            try
            {
                return await _context.ContractGuarantees.FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<ContractGuarantee>> GetAll(Guid contractId)
        {
            try
            {
                return await _context.ContractGuarantees.Where(x=>x.ContractId==contractId)
                    .Include(x=>x.ForGuarantee)
                    .Include(x=>x.ReleaseCondition)
                    .Include(x=>x.TypeOfGuarantee)
               .ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Update(ContractGuarantee contractGuarantee)
        {
            try
            {
                var model = await Get(contractGuarantee.Id);
                if (model != null)
                {
                    _context.Entry(model).CurrentValues.SetValues(contractGuarantee);
                    return true;
                }
                return await Add(contractGuarantee);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
