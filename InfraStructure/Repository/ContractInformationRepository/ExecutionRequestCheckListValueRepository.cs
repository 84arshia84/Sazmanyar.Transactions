using AppCore.Entities.ContractsInformation.ContractCheckListValues;
using AppCore.Entities.ContractsInformation.ExecutionRequestCheckListValues;
using InfrStructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.ContractInformationRepository
{
    internal class ExecutionRequestCheckListValueRepository : IExecutionRequestCheckListValueRepository
    {
        private readonly AppDbContext _context;
        public ExecutionRequestCheckListValueRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Add(ExecutionRequestCheckListValue CheckListValue)
        {
            try
            {
                await _context.ExecutionRequestCheckListValue.AddAsync(CheckListValue);
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Delete(Guid transactionId)
        {
            try
            {
                var model = await Get(transactionId);
                if (model != null)
                {
                    _context.Remove(model);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ExecutionRequestCheckListValue> Get(Guid transactionId)
        {
            try
            {
                return await _context.ExecutionRequestCheckListValue.FirstOrDefaultAsync(x => x.TransactionExecutionRequestsId == transactionId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Update(ExecutionRequestCheckListValue CheckListValue)
        {
            try
            {
                var model = await Get(CheckListValue.TransactionExecutionRequestsId);
                if (model != null)
                {
                    _context.Entry(model).CurrentValues.SetValues(CheckListValue);
                    return true;
                }
                return await Add(CheckListValue);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
