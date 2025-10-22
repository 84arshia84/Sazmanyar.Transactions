using AppCore.Entities.ContractsInformation.Contratcs;
using InfrStructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.ContractInformationRepository
{
    internal class ContractRepository :
    IContractRepository
    {
        private readonly AppDbContext _context;
        public ContractRepository(AppDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// اضافه کردن اطلاعات قرارداد
        /// یه دیتابیس
        /// </summary>
        /// <param name="contract"></param>
        /// <returns>Tuple{string,bool}</returns>
        /// <exception cref="{string, false}"></exception>
        public async Task<(string message, bool isSuccess)> Add(Contract contract)
        {
            try
            {
                var result = _context.Contracts.AddAsync(contract);
                if (result.IsCompleted == true)
                {
                    return ("اطلاعات قرارداد با موفقیت ثبت شد.", true);
                }
                return ("ثبت اطلاعات قرارداد با مشکل مواجه شد.", false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// حذف کردن قرارداد از دیتابیس
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns>Tuple{string, bool}</returns>
        /// <exception cref="{string, false}"></exception>
        public async Task<(string message, bool isSuccess)> Delete(Guid contractId, Guid userId)
        {
            try
            {
                var model = await _context.Contracts.Where(C => C.ID == contractId && C.IsDeleted == false).FirstOrDefaultAsync();
                if (model != null)
                {
                    model.IsDeleted = true;
                    model.DeleteDate = DateTime.Now;
                    model.DeleteBy = userId;
                    await _context.SaveChangesAsync();
                    return ("حذف قرارداد با موفقیت انجام شد.", true);
                }
                return ("مورد مد نظر جهت حذف یافت نشد.", false);
            }
            catch (Exception ex)
            {
                return ("حذف با خطا روبرو شد.", false);
            }
        }
        /// <summary>
        /// حذف قرارداد خطا خورده 
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task DeleteExceptinalContract(Guid contractId)
        {
            try
            {
                var ExceptionModel = await _context.Contracts.Where(x=>x.ID== contractId).FirstOrDefaultAsync();
                _context.Contracts.Remove(ExceptionModel);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// گرفتن اطلاعات یک قرارداد،
        /// از دیتابیس
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns>Contract</returns>
        /// <exception cref="ContractDto"></exception>
        public async Task<Contract?> Get(Guid contractId)
        {
            try
            {
                var model = await _context.Contracts
                    .AsNoTracking() // 👈 مهم‌ترین تغییر برای درست شدن لاگ
                    .Where(c => c.ID == contractId && c.IsDeleted == false)
                    .Include(c => c.ContratTimeProfile)
                    .Include(c => c.ContractFinancialDetails)
                    .Include(x => x.ContractCheckListValues)
                    .FirstOrDefaultAsync();

                return model;
            }
            catch (Exception ex)
            {
                // می‌تونی اینجا لاگ خطا بذاری مثلاً _errorLoggerService.SaveError(ex);
                return null;
            }
        }
        /// <summary>
        /// گرفتن اطلاعات تمامی قرارداد ها،
        /// از دیتابیس
        /// </summary>
        /// <returns>List{Contract}</returns>
        /// <exception cref="List{Contract}"></exception>
        public async Task<List<Contract>> GetAll()
        {
            try
            {
                return await _context.Contracts.Where(C => C.IsDeleted == false)
                    .Include(c => c.ContratTimeProfile)
                    .Include(c => c.ContractFinancialDetails)
                    .Include(x => x.ContractCheckListValues)
                    .OrderByDescending(x => x.InsertContractDate)
                    .ToListAsync();

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<Contract>> GetAllWithFinalApprove()
        {
            try
            {
                return await _context.Contracts.Where(C => C.IsDeleted == false && C.IsFinalApprove == true)
                    .Include(c => c.ContratTimeProfile)
                    .Include(c => c.ContractFinancialDetails)
                    .Include(x => x.ContractCheckListValues)
                    .OrderByDescending(x => x.InsertContractDate)
                    .ToListAsync();

            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// قرارداد هایی که کاربر در ثبت الحاقیه می تواند ببیند
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<Contract>> GetAllForAddendum(List<Guid> contractId)
        {
            try
            {
                return await _context.Contracts.Where(C => C.IsDeleted == false && C.IsFinalApprove == true && (contractId == null || !contractId.Any() || contractId.Contains(C.ID)))
                   .Include(c => c.ContratTimeProfile)
                   .Include(c => c.ContractFinancialDetails)
                   .Include(x => x.ContractCheckListValues)
                   .OrderByDescending(x => x.InsertContractDate)
                   .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// تمام قرارداد هایی که هنگام ثبت صورت وضعیت کاربر می تواند ببیند
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<Contract>> GetAllForInvoice(List<Guid> contractId)
        {
            try
            {
                return await _context.Contracts.Where(C => C.IsDeleted == false && C.IsFinalApprove == true && (contractId == null || !contractId.Any() || contractId.Contains(C.ID)))
                  .Include(c => c.ContratTimeProfile)
                  .Include(c => c.ContractFinancialDetails)
                  .Include(x => x.ContractCheckListValues)
                  .OrderByDescending(x => x.InsertContractDate)
                  .ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// گرفتن تمامی قرارداد های کاربر لاگین شده ثبت کرده
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<Contract>> GetAllMine(Guid userId)
        {
            try
            {
                return await _context.Contracts.Where(C => C.IsDeleted == false && C.InsertContractBy == userId)
                   .Include(c => c.ContratTimeProfile)
                   .Include(c => c.ContractFinancialDetails)
                   .Include(c => c.ContractCheckListValues)
                   .OrderByDescending(x => x.InsertContractDate)
                   .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// گرفتن تمامی قرارداد های منتظر اقدام من
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<Contract>> GetAllWaitForAction(List<Guid> contractId)
        {
            try
            {
                return await _context.Contracts.Where(C => C.IsDeleted == false && contractId.Contains(C.ID))
                   .Include(c => c.ContratTimeProfile)
                   .Include(c => c.ContractFinancialDetails)
                   .Include(x => x.ContractCheckListValues)
                   .OrderByDescending(x => x.InsertContractDate)
                   .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        ///<summary>
        ///گرفتن منتظر تاییدها
        /// <summary>
       public async Task<List<Contract>> GetAllWaitToConfirm(List<Guid> contractId)
        {
            try
            {
                return await _context.Contracts.Where(C => C.IsDeleted == false && contractId.Contains(C.ID))
                   .Include(c => c.ContratTimeProfile)
                   .Include(c => c.ContractFinancialDetails)
                   .Include(x => x.ContractCheckListValues)
                   .OrderByDescending(x => x.InsertContractDate)
                   .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// بروزرسانی اطلاعات قرارداد
        /// در دیتابیس
        /// </summary>
        /// <param name="contract"></param>
        /// <returns>Tuple{string, bool}</returns>
        /// <exception cref="{string, false}"></exception>
        public async Task<(string message, bool isSuccess)> Update(Contract contract)
        {
            try
            {
                var model = await Get(contract.ID);
                if (model != null)
                {
                    _context.Entry(model).CurrentValues.SetValues(contract);
                    return ("ویرایش با موفقیت انجام شد", true);
                }
                return ("مورد مد نظر جهت ویرایش وجود ندارد.", false);
            }
            catch (Exception ex)
            {

                return ("ویرایش با خطا روبرو شد.", false);
            }
        }

        public async Task<Contract> HasInvoice(Guid contractId)
        {
            try
            {
                return await _context.Contracts.Where(c=>c.ID== contractId).Include(c=>c.InvoiceBaseInformations.Where(i => i.IsDeleted == false)).FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateStatus(Guid contractId,Guid newId)
        {
            try
            {
                var model = await Get(contractId);
                if (model != null)
                {
                    model.StatusId= newId;
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Contract>> GetWithIsFromExecution()
        {
            try
            {
                var models = await _context.Contracts
                    .Include(c => c.ServiceExplanations)
                    .Include(c => c.ContratTimeProfile)
                   .Include(c => c.ContractFinancialDetails)
                   .Include(x => x.ContractCheckListValues)
                   .OrderByDescending(x => x.InsertContractDate)
                    .Where(c => c.ServiceExplanations.Any(se => se.IsForExecutionRequest == true))
                   .ToListAsync();// بارگذاری داده‌های مرتبط
                   
                if (models != null)
                {
                    return models;
                }
                return new List<Contract>();
            }
            catch (Exception)
            {
                throw;
            }

        }



public async Task<List<Contract>> SearchAllAsync(string? term)
{
    var q = _context.Contracts.Where(c => c.IsDeleted == false);

    if (!string.IsNullOrWhiteSpace(term))
    {
        var t = term.Trim();
        q = q.Where(c =>
            c.ContractTitle.Contains(t) ||                   // موجود است
            c.ContractNumber.Contains(t));                   // موجود است
    }

    return await q.OrderByDescending(c => c.InsertContractDate) // موجود است
                  .ThenBy(c => c.ID)                            // موجود است
                  .ToListAsync();
}

public async Task<List<Contract>> SearchMineAsync(Guid userId, string? term)
{
    var q = _context.Contracts
        .Where(c => c.IsDeleted == false && c.InsertContractBy == userId); // موجود است

    if (!string.IsNullOrWhiteSpace(term))
    {
        var t = term.Trim();
        q = q.Where(c => c.ContractTitle.Contains(t) || c.ContractNumber.Contains(t));
    }

    return await q.OrderByDescending(c => c.InsertContractDate)
                  .ThenBy(c => c.ID)
                  .ToListAsync();
}

public async Task<List<Contract>> SearchWaitForActionAsync(List<Guid> ids, string? term)
{
    var q = _context.Contracts.Where(c => c.IsDeleted == false && ids.Contains(c.ID));

    if (!string.IsNullOrWhiteSpace(term))
    {
        var t = term.Trim();
        q = q.Where(c => c.ContractTitle.Contains(t) || c.ContractNumber.Contains(t));
    }

    return await q.OrderByDescending(c => c.InsertContractDate)
                  .ThenBy(c => c.ID)
                  .ToListAsync();
}

public async Task<List<Contract>> SearchWithFinalApproveAsync(string? term)
{
    var q = _context.Contracts.Where(c => c.IsDeleted == false && c.IsFinalApprove == true);

    if (!string.IsNullOrWhiteSpace(term))
    {
        var t = term.Trim();
        q = q.Where(c => c.ContractTitle.Contains(t) || c.ContractNumber.Contains(t));
    }

    return await q.OrderByDescending(c => c.InsertContractDate)
                  .ThenBy(c => c.ID)
                  .ToListAsync();
}

        public async Task<Contract?> GetByContractNumber(string contractNumber)
        {
           var entity = await _context.Contracts.FirstOrDefaultAsync(c=>c.ContractNumber == contractNumber);
            if (entity == null)
                return null;
            return entity;
        }
    }
}
