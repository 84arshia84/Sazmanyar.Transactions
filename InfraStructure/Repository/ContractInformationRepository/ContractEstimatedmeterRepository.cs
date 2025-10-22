using AppCore.Entities.ContractsInformation.ContractEstimatedmeters;
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
    internal class ContractEstimatedmeterRepository : IContractEstimatedmeterRepository
    {
        private readonly AppDbContext _context;
        public ContractEstimatedmeterRepository(AppDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// افزودن  متر برآورد 
        /// </summary>
        /// <param name="estimatedmeter"></param>
        /// <returns></returns>
        public async Task<bool> Add(ContractEstimatedmeter estimatedmeter)
        {
            try
            {
                await _context.ContractEstimatedmeters.AddAsync(estimatedmeter);
                //await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// افزودن لیست متر برآورد ها
        /// </summary>
        /// <param name="estimatedmeter"></param>
        /// <returns></returns>
        public async Task<bool> Add(List<ContractEstimatedmeter> estimatedmeters)
        {
            try
            {
                await _context.ContractEstimatedmeters.AddRangeAsync(estimatedmeters);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// حذف یکی از متربرآورد ها
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var model = await Get(id);
                if (model != null)
                {
                    _context.ContractEstimatedmeters.Remove(model);
                    // await _context.SaveChangesAsync();
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
        /// حذف تمامی متربرآورد ها
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAll(Guid contractId)
        {
            try
            {
                var models = await GetAll(contractId);
                if (models != null)
                {
                    foreach (var model in models)
                    {
                        model.IsDeleted = true;
                        model.DeleteDate = DateTime.Now;
                    }
                  //  _context.ContractEstimatedmeters.RemoveRange(models);
                    await _context.SaveChangesAsync();
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
        /// این مواردی را که در الحاقیه هستند را حذف میکند
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> DeleteInAddendum(ContractEstimatedmeter estimatedmeter)
        {
            try
            {
                var model = await Get(estimatedmeter.Id);
                if (model != null)
                {
                    _context.Entry(model).CurrentValues.SetValues(estimatedmeter);
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
        /// گرفتن متربرآورد
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ContractEstimatedmeter> Get(Guid id)
        {
            try
            {
                return await _context.ContractEstimatedmeters.FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// گرفتن تمامی متربرآورد یک قرارداد
        /// </summary>
        /// <param name="serviceExplanationId"></param>
        /// <returns></returns>
        public async Task<List<ContractEstimatedmeter>> GetAll(Guid serviceExplanationId)
        {
            try
            {
                return await _context.ContractEstimatedmeters.Where(x => x.ServiceExplanationId == serviceExplanationId && x.IsAddendum == false && x.AddendumId == null).OrderBy(x=>x.Order).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<ContractEstimatedmeter>> GetAllForAddendum(Guid ContractId)
        {
            try
            {
                return await _context.Contracts
                        .Where(c => c.ID == ContractId)
                        .SelectMany(c => c.ServiceExplanations)
                        .SelectMany(e => e.ContractEstimatedmeters)
                        .OrderBy(x => x.Order)
                        .ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// بروزرسانی متر برآورد
        /// </summary>
        /// <param name="estimatedmeter"></param>
        /// <returns></returns>
        public async Task<bool> Update(ContractEstimatedmeter estimatedmeter)
        {
            try
            {
                var model = await Get(estimatedmeter.Id);
                if (model != null)
                {
                    _context.Entry(model).CurrentValues.SetValues(estimatedmeter);
                    //_context.ContractEstimatedmeters.Update(estimatedmeter);
                    return true;
                }
                return await Add(estimatedmeter);
                //await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        ///  بروزرسانی تمامی متر برآوردها
        /// </summary>
        /// <param name="estimatedmeter"></param>
        /// <returns></returns>
        public async Task<bool> Update(List<ContractEstimatedmeter> estimatedmeters, Guid contractId)
        {
            try
            {
                var result = await DeleteAll(contractId);
                if (result)
                {
                    return await Add(estimatedmeters);
                }
                return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// زمانی که یک متر برآورد در قرارداد یا یک الحاقیه وجود داشته 
        /// و ما آن را در الحاقیه دیگر ویرایش می کنیم
        /// متر برآورد قبلی UpdatedInAddendum = true; 
        /// می شود.
        /// </summary>
        /// <param name="coefficientId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> UpdateInAddendum(ContractEstimatedmeter estimatedmeters)
        {
            try
            {
                var model = await Get(estimatedmeters.Id);
                if (model != null)
                {
                    _context.Entry(model).CurrentValues.SetValues(estimatedmeters);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
