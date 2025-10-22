using AppCore.Entities.ContractsInformation.ContractCoefficients;
using AppCore.Entities.ContractsInformation.ServiceExplanations;
using InfrStructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.ContractInformationRepository
{
    internal class ServiceExplanationRepository : IServiceExplanationRepository
    {
        private readonly AppDbContext _context;
        public ServiceExplanationRepository(AppDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// افزودن شرح خدمات قرارداد
        /// </summary>
        /// <param name="serviceExplanation"></param>
        /// <returns>{string,bool}</returns>
        /// <exception cref="string,false"></exception>
        public async Task<(string message, bool isSuccess)> Add(List<ServiceExplanation> serviceExplanation)
        {
            try
            {
                var result = _context.ServiceExplanations.AddRangeAsync(serviceExplanation);
                await _context.SaveChangesAsync();
                if (result.IsCompleted)
                {
                    return ("شرح خدمت  ها با موفقیت ثبت شد.", true);
                }
                return ("ثبت شرح خدمت  ها با خطا مواجه شد.", false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// افزودن یک شرح خدمت
        /// </summary>
        /// <param name="serviceExplanation"></param>
        /// <param name="contractId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(string message, bool isSuccess)> Add(ServiceExplanation serviceExplanation)
        {
            try
            {
                var result = await _context.ServiceExplanations.AddAsync(serviceExplanation);
                return ("شرح خدمت   با موفقیت ثبت شد.", true);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// حذف کردن شرح خدمات قرارداد
        /// </summary>
        /// <param name="id"></param>
        /// <returns>string,bool</returns>
        /// <exception cref="string,false"></exception>
        public async Task<(string message, bool isSuccess)> Delete(Guid id)
        {
            try
            {
                var model = await Get(id);
                if (model != null)
                {
                    _context.ServiceExplanations.Remove(model);
                    return ("حذف موفقیت آمیز بود", true);
                }

                return ("شرح خدمت جهت حذف پیدا نشد.", false);
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
        public async Task<(string message, bool isSuccess)> DeleteInAddendum(ServiceExplanation serviceExplanation)
        {
            try
            {
                var model = await Get(serviceExplanation.ID);
                if (model != null)
                {
                    _context.Entry(model).CurrentValues.SetValues(serviceExplanation);
                    return ("حذف موفقیت آمیز بود.", true);
                }
                return ("مورد مد نظر جهت حذف پیدا نشد.", false);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// گرفتن شرح خدمات  یک قرارداد
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ServiceExplanation</returns>
        /// <exception cref="ServiceExplanation"></exception>
        public async Task<ServiceExplanation> Get(Guid id)
        {
            try
            {

                var model = await _context.ServiceExplanations.FirstOrDefaultAsync(x => x.ID == id);
                if (model != null)
                {
                    return model;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// گرفتن شرح خدمات های یک قرارداد
        /// </summary>
        /// <param name="ContractId"></param>
        /// <returns>List<ServiceExplanation></returns>
        /// <exception cref="List<ServiceExplanation>"></exception>
        public async Task<List<ServiceExplanation>> GetAll(Guid ContractId)
        {
            try
            {
                var models = await _context.ServiceExplanations.Where(s => s.ContractID == ContractId && s.IsAddendum == false && s.ContractAddendumId == null)
                    .Include(x => x.ContractEstimatedmeters.Where(ce => ce.IsAddendum == false && ce.UpdatedInAddendum == false && ce.AddendumId == null))
                    .Include(x => x.Currency).OrderBy(x => x.Order).ToListAsync();
                if (models != null)
                {
                    return models;
                }
                return new List<ServiceExplanation>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// گرفتن تمامی شرح خدمات برای آخرین الحاقیه
        /// </summary>
        /// <param name="ContractId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<ServiceExplanation>> GetAllForAddendum(Guid ContractId)
        {
            try
            {
                return await _context.ServiceExplanations.Where(s => s.ContractID == ContractId).Include(x => x.Currency).OrderBy(x => x.Order).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// حذف کردن شرح خدمات الحاقیه ای که حذف شده
        /// </summary>
        /// <param name="addendumId"></param>
        /// <returns></returns>
        public async Task DeleteServiceExplanationOfAddendum(Guid addendumId)
        {
            try
            {
                var models = await _context.ServiceExplanations.Where(s => s.IsAddendum == true && s.ContractAddendumId == addendumId && s.IsDeleted == false).ToListAsync();
                var updatedModelInThisAddendum = await _context.ServiceExplanations.Where(s => s.IsAddendum == true && s.IsDeleted == false && s.UpdateInAddendumId == addendumId).ToListAsync();
                if (models != null && models.Count > 0)
                {
                    _context.RemoveRange(models);
                }
                if(updatedModelInThisAddendum != null)
                {
                    for (int i = 0; i < updatedModelInThisAddendum.Count; i++)
                    {
                        updatedModelInThisAddendum[i].UpdateInAddendumId = null;
                        updatedModelInThisAddendum[i].UpdatedInAddendum = false;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// بروزرسانی شرح خدمات قرارداد
        /// </summary>
        /// <param name="serviceExplanation"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(string message, bool isSuccess)> Update(ServiceExplanation serviceExplanation)
        {
            try
            {
                var model = await Get(serviceExplanation.ID);
                if (model != null)
                {
                    //_context.ServiceExplanations.Update(serviceExplanation);
                    _context.Entry(model).CurrentValues.SetValues(serviceExplanation);
                    return ("ویرایش با موفقیت انجام شد", true);
                }
                return await Add(serviceExplanation);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// زمانی که یک شرح خدمات در قرارداد یا یک الحاقیه وجود داشته 
        /// و ما آن را در الحاقیه دیگر ویرایش می کنیم
        /// شرح خدمات قبلی UpdatedInAddendum = true; 
        /// می شود.
        /// </summary>
        /// <param name="coefficientId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(string message, bool isSuccess)> UpdateInAddendum(ServiceExplanation serviceExplanation)
        {
            try
            {
                var model = await Get(serviceExplanation.ID);
                if (model != null)
                {
                    _context.Entry(model).CurrentValues.SetValues(serviceExplanation);
                    return ("ویرایش موفقیت آمیز بود.", true);
                }
                return ("مورد مد نظر جهت ویرایش وجود ندارد.", false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // to get serviceExplanation which has IsFromExection
        public async Task<List<ServiceExplanation>> GetWithIsFromExecution (Guid contractId)
        {
            try
            {
                var models = await _context.ServiceExplanations.Where(s => s.ContractID == contractId && s.IsForExecutionRequest == true).ToListAsync();
                if (models != null)
                {
                    return models;
                }
                return new List<ServiceExplanation>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
