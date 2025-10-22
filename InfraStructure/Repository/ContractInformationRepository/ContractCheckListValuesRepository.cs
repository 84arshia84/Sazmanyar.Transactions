using AppCore.Entities.ContractsInformation.ContractCheckListValues;
using InfrStructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.ContractInformationRepository
{
    internal class ContractCheckListValuesRepository : IContractCheckListValuesRepository
    {
        private readonly AppDbContext _context;
        public ContractCheckListValuesRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Add(ContractCheckListValue contractCheckListValue)
        {
            try
            {
                await _context.ContractCheckListValues.AddAsync(contractCheckListValue);
                return true;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public  async Task<bool> Delete(Guid contractId)
        {
            try
            {
                var model = await Get(contractId);
                if (model != null)
                {
                    _context.Remove(model);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<ContractCheckListValue> Get(Guid contractId)
        {
            try
            {
                return await _context.ContractCheckListValues.FirstOrDefaultAsync(x => x.ContractId == contractId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Task<List<ContractCheckListValue>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(ContractCheckListValue contractCheckListValue)
        {
            try
            {
                var model = await Get(contractCheckListValue.ContractId);
                if(model != null)
                {
                    _context.Entry(model).CurrentValues.SetValues(contractCheckListValue);
                    return true;
                }
                return await Add(contractCheckListValue);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
