using AppCore.Entities.ContractsInformation.ContractAddendums;
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
    internal class ContractAddendumRepository : IContractAddendumRepository
    {
        private readonly AppDbContext _context;
        public ContractAddendumRepository(AppDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// افزودن الحاقیه
        /// </summary>
        /// <param name="contractAddendum"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> Add(ContractAddendum contractAddendum)
        {
            try
            {
                await _context.ContractAddendums.AddAsync(contractAddendum);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// افزودن تمامی الحاقیه ها
        /// </summary>
        /// <param name="contractAddendum"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<bool> Add(List<ContractAddendum> contractAddendum)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// حذف الحاقیه
        /// </summary>
        /// <param name="addendumId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> Delete(Guid addendumId, Guid userId)
        {
            try
            {
                var model = await Get(addendumId);
                if (model != null)
                {
                    model.IsDeleted = true;
                    model.DeleteAddendumBy = userId;
                    model.DeleteAddenDumDate = DateTime.Now;
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// حذف الحاقیه ای که در هنگام حذف با خطا مواجه شده
        /// </summary>
        /// <param name="addendumId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> DeleteExceptionalAddendum(Guid addendumId)
        {
            try
            {
                var model = await Get(addendumId);
                if (model != null)
                {
                    _context.ContractAddendums.Remove(model);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// گرفتن الحاقیه
        /// </summary>
        /// <param name="addendumId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ContractAddendum> Get(Guid addendumId)
        {
            try
            {
                return await _context.ContractAddendums.FirstOrDefaultAsync(x => x.Id == addendumId && x.IsDeleted == false);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<ContractAddendum> GetByContract(Guid addendumId)
        {
            try
            {
                return await _context.ContractAddendums.Include(x => x.Contract).ThenInclude(x => x.ContratTimeProfile).FirstOrDefaultAsync(x => x.Id == addendumId && x.IsDeleted == false);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// گرفتن تمامی الحاقیه ها
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<ContractAddendum>> GetAll()
        {
            try
            {
                return await _context.ContractAddendums.Where(x => x.IsDeleted == false).Include(x => x.AddendumType).Include(x => x.Contract).ThenInclude(x => x.ContratTimeProfile).OrderByDescending(x => x.InsertAddendumDate).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// گرفتن تمامی الحاقیه یک قرارداد 
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<ContractAddendum>> GetAll(Guid contractId)
        {
            try
            {
                return await _context.ContractAddendums.Where(CA => CA.IsDeleted == false && CA.ContractId == contractId)
                  .Include(c => c.Contract)
                  .ThenInclude(x => x.ContratTimeProfile)
                  .Include(c => c.AddendumType)
                  .OrderByDescending(x => x.InsertAddendumDate)
                  .ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// گرفتن تمامی الحاقیه 
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<ContractAddendum>> GetAll(List<Guid> addendumIds)
        {
            try
            {
                return await _context.ContractAddendums.Where(CA => CA.IsDeleted == false && (addendumIds == null || !addendumIds.Any() || addendumIds.Contains(CA.Id)))
                  .Include(c => c.Contract)
                  .ThenInclude(x => x.ContratTimeProfile)
                  .Include(c => c.AddendumType)
                  .OrderByDescending(x => x.InsertAddendumDate)
                  .ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// گرفتن تمامی الحاقیه های کاربر لاگین شده ثبت کرده
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<ContractAddendum>> GetAllMine(Guid userId)
        {
            try
            {
                return await _context.ContractAddendums.Where(C => C.IsDeleted == false && C.InsertAddendumBy == userId)
                   .Include(c => c.Contract)
                   .ThenInclude(c => c.ContratTimeProfile)
                   .Include(c => c.AddendumType)
                   .OrderByDescending(x => x.InsertAddendumDate)
                   .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// گرفتن تمامی الحاقیه های منتظر اقدام من
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<ContractAddendum>> GetAllWaitForAction(List<Guid> contractId)
        {
            try
            {
                return await _context.ContractAddendums.Where(C => C.IsDeleted == false && contractId.Contains(C.Id))
                   .Include(c => c.Contract)
                   .ThenInclude(c => c.ContratTimeProfile)
                   .Include(c => c.AddendumType)
                   .OrderByDescending(x => x.InsertAddendumDate)
                   .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// بروزرسانی الحاقیه
        /// </summary>
        /// <param name="contractAddendum"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> Update(ContractAddendum contractAddendum)
        {
            try
            {
                var model = await Get(contractAddendum.Id);
                if (model != null)
                {
                    _context.Entry(model).CurrentValues.SetValues(contractAddendum);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// آخرین الحاقیه یک قرارداد
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Guid> GetLastAddendumOfContract(Guid contractId)
        {
            try
            {
                return await _context.ContractAddendums
                            .Where(x => x.ContractId == contractId && x.IsDeleted == false && x.IsFinalApprove == true)
                            .OrderByDescending(e => e.InsertAddendumDate)
                            .Select(x => x.Id)
                            .FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// بروزرسانی آخرین تغییرات در الحاقیه تازه ثبت شده
        /// </summary>
        /// <param name="addendumId"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task UpdateAddendumChangedValue(Guid addendumId, string value)
        {
            try
            {
                var model = await Get(addendumId);
                if (model != null)
                {
                    model.RateOfContractPriceChanged = value;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
