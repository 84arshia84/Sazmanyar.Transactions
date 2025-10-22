using AppCore.Entities.ContractsInformation.ExecutionRequestServiceExplanations;
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
    internal class ExecutionRequestServiceExplanationRepository : IExecutionRequestServiceExplanationRepository
    {
        private readonly AppDbContext _context;
        public ExecutionRequestServiceExplanationRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<(string message, bool isSuccess)> Add(List<ExecutionRequestServiceExplanation> serviceExplanation)
        {
            try
            {
                var result = _context.ExecutionRequestServiceExplanation.AddRangeAsync(serviceExplanation);
                if (result.IsCompleted)
                {
                    return ("شرح خدمت  ها با موفقیت ثبت شد.", true);
                }
                return ("ثبت شرح خدمت  ها با خطا مواجه شد.", false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<(string message, bool isSuccess)> Add(ExecutionRequestServiceExplanation serviceExplanation)
        {
            try
            {
                var result = await _context.ExecutionRequestServiceExplanation.AddAsync(serviceExplanation);
                return ("شرح خدمت   با موفقیت ثبت شد.", true);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<(string message, bool isSuccess)> Delete(Guid id)
        {
            try
            {
                var model = await Get(id);
                if (model != null)
                {
                    _context.ExecutionRequestServiceExplanation.Remove(model);
                    return ("حذف موفقیت آمیز بود", true);
                }

                return ("شرح خدمت جهت حذف پیدا نشد.", false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ExecutionRequestServiceExplanation> Get(Guid id)
        {
            try
            {

                var model = await _context.ExecutionRequestServiceExplanation.FirstOrDefaultAsync(x => x.ID == id);
                if (model != null)
                {
                    return model;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<ExecutionRequestServiceExplanation>> GetAll(Guid transactionId)
        {
            try
            {
                var models = await _context.ExecutionRequestServiceExplanation.Where(s => s.TransactionExecutionRequestId == transactionId)
                  .OrderBy(x => x.Order).ToListAsync();
                if (models != null)
                {
                    return models;
                }
                return new List<ExecutionRequestServiceExplanation>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<(string message, bool isSuccess)> Update(ExecutionRequestServiceExplanation serviceExplanation)
        {
            try
            {
                var model = await Get(serviceExplanation.ID);
                if (model != null)
                {
                    _context.Entry(model).CurrentValues.SetValues(serviceExplanation);
                    return ("ویرایش با موفقیت انجام شد", true);
                }
                return await Add(serviceExplanation);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<List<ExecutionRequestServiceExplanation>> GetAllWithTransnactionApproved(Guid transactionId)
        {
            try
            {
                var models = await _context.ExecutionRequestServiceExplanation.Where(s => s.TransactionExecutionRequestId == transactionId && s.TransactionExecutionRequest.IsFinalApprove == true)
                  .OrderBy(x => x.Order).ToListAsync();
                if (models != null)
                {
                    return models;
                }
                return new List<ExecutionRequestServiceExplanation>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<ExecutionRequestServiceExplanation>> GetWithComodity(Guid transactionId)
        {
            try
            {
                var models = await _context.ExecutionRequestServiceExplanation.Where(es => es.TransactionExecutionRequestId == transactionId).ToListAsync();
                if(models != null)
                {
                    return models;
                }
                return new List<ExecutionRequestServiceExplanation>();
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
