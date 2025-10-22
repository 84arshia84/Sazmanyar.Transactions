using AppCore.Entities.ContractsInformation.TransactionExecutionRequests;
using AppCore.Entities.User;
using InfrStructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.ContractInformationRepository
{
    internal class TransactionExecutionRequestRepository : ITransactionExecutionRequestRepository
    {
        private readonly AppDbContext _context;
        public TransactionExecutionRequestRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<(string message, bool isSuccess)> Add(TransactionExecutionRequest transaction)
        {
            try
            {
                var result = _context.TransactionExecutionRequest.AddAsync(transaction);
                if (result.IsCompleted == true)
                {
                    return ("اطلاعات  با موفقیت ثبت شد.", true);
                }
                return ("ثبت اطلاعات  با مشکل مواجه شد.", false);
            }
            catch (Exception)
            {
                throw ;
            }
        }

        public async Task<(string message, bool isSuccess)> Delete(Guid transactionId, Guid userId)
        {
            try
            {
                var model = await _context.TransactionExecutionRequest.Where(C => C.Id == transactionId && C.IsDeleted == false).FirstOrDefaultAsync();
                if (model != null)
                {
                    model.IsDeleted = true;
                    model.DeletedDate = DateTime.Now;
                    model.DeletedBy = userId;
                    await _context.SaveChangesAsync();
                    return ("حذف  با موفقیت انجام شد.", true);
                }
                return ("مورد مد نظر جهت حذف یافت نشد.", false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteExceptionalTransaction(Guid transactionId)
        {
            try
            {
                var model = await _context.TransactionExecutionRequest.Where(C => C.Id == transactionId && C.IsDeleted == false).FirstOrDefaultAsync();
                if (model != null)
                {
                    _context.TransactionExecutionRequest.Remove(model);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<TransactionExecutionRequest> Get(Guid transactionId)
        {
            try
            {
                var model = await _context.TransactionExecutionRequest.Where(t => t.Id == transactionId && t.IsDeleted == false).Include(t=>t.ExecutionRequestCheckListValue).FirstOrDefaultAsync();
                if (model != null)
                {
                    return model;
                }
                return null;
            }
            catch (Exception )
            {
                throw;
            }
        }

        public async Task<List<TransactionExecutionRequest>> GetAll()
        {
            try
            {
                return await _context.TransactionExecutionRequest.Where(C => C.IsDeleted == false).Include(t=>t.ExecutionRequestCheckListValue).OrderByDescending(x=>x.InsertDate).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<TransactionExecutionRequest>> GetAllMine(Guid userId)
        {
            try
            {
                return await _context.TransactionExecutionRequest.Where(C => C.IsDeleted == false && C.InsertBy == userId).Include(t => t.ExecutionRequestCheckListValue)
                   .OrderByDescending(x => x.InsertDate)
                   .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<TransactionExecutionRequest>> GetAllWaitForAction(List<Guid> transactionId)
        {
            try
            {
                return await _context.TransactionExecutionRequest.Where(C => C.IsDeleted == false && transactionId.Contains(C.Id)).Include(t=> t.ExecutionRequestCheckListValue)
                   .OrderByDescending(x => x.InsertDate)
                   .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<(string message, bool isSuccess)> Update(TransactionExecutionRequest transaction)
        {
            try
            {
                var model = await Get(transaction.Id);
                if (model != null)
                {
                    _context.Entry(model).CurrentValues.SetValues(transaction);
                    return ("ویرایش با موفقیت انجام شد", true);
                }
                return ("مورد مد نظر جهت ویرایش وجود ندارد.", false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<TransactionExecutionRequest>> GetAllFinalApproved()
        {
            try
            {
                return await _context.TransactionExecutionRequest.Where(C => C.IsDeleted == false && C.IsFinalApprove == true)
                   .OrderByDescending(x => x.InsertDate)
                   .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<TransactionExecutionRequest?> GetByRequestNumber(string numberOfRequest)
        {
            var entity = await _context.TransactionExecutionRequest.FirstOrDefaultAsync(t=>t.NumberOfRequest == numberOfRequest);
            if (entity == null)
                return null;
            return entity;
        }
    }
}
