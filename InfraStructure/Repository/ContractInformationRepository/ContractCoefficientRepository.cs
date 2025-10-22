using AppCore.Entities.ContractsInformation.ContractCoefficients;
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
    internal class ContractCoefficientRepository : IContractCoefficientRepository
    {
        private readonly AppDbContext _context;
        public ContractCoefficientRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<(string message, bool isSuccess)> Add(ContractCoefficient contractCoefficient)
        {
            try
            {
                await _context.ContractCoefficients.AddAsync(contractCoefficient);
                return ("ثبت یا موفقیت انجام شد.", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<(string message, bool isSuccess)> Add(List<ContractCoefficient> contractCoefficient)
        {
            try
            {
                await _context.ContractCoefficients.AddRangeAsync(contractCoefficient);
                return ("ثبت یا موفقیت انجام شد.", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<(string message, bool isSuccess)> Delete(Guid id)
        {
            try
            {
                var model = await Get(id);
                if (model != null)
                {
                    _context.ContractCoefficients.Remove(model);
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
        /// این ضرایبی را که در الحاقیه هستند را حذف میکند
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(string message, bool isSuccess)> DeleteInAddendum(ContractCoefficient contractCoefficient)
        {
            try
            {
                var model = await Get(contractCoefficient.Id);
                if (model != null)
                {
                    _context.Entry(model).CurrentValues.SetValues(contractCoefficient);
                    return ("حذف موفقیت آمیز بود.", true);
                }
                return ("مورد مد نظر جهت حذف پیدا نشد.", false);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<List<ContractCoefficient>> GetAll(Guid contractId)
        {
            try
            {
                return await _context.ContractCoefficients.Where(x => x.ContractId == contractId && x.IsAddendum == false  && x.AddendumId == null).OrderBy(x=>x.Order).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<ContractCoefficient>> GetAllForAddendum(Guid contractId)
        {
            try
            {
                return await _context.ContractCoefficients.Where(x => x.ContractId == contractId).OrderBy(x=>x.Order).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ContractCoefficient> Get(Guid id)
        {
            try
            {
                return await _context.ContractCoefficients.FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<(string message, bool isSuccess)> Update(ContractCoefficient contractCoefficient)
        {
            try
            {
                var model = await Get(contractCoefficient.Id);
                if (model != null)
                {
                    _context.Entry(model).CurrentValues.SetValues(contractCoefficient);
                    return ("ویرایش موفقیت آمیز بود.", true);
                }
                await Add(contractCoefficient);
                return ("ویرایش موفقیت آمیز بود.", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// زمانی که یک ضریب در قرارداد یا یک الحاقیه وجود داشته 
        /// و ما آن را در الحاقیه دیگر ویرایش می کنیم
        /// ضریب قبلی UpdatedInAddendum = true; 
        /// می شود.
        /// </summary>
        /// <param name="coefficientId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(string message, bool isSuccess)> UpdateInAddendum(ContractCoefficient contractCoefficient)
        {
            try
            {
                var model = await Get(contractCoefficient.Id);
                if(model != null)
                {
                    _context.Entry(model).CurrentValues.SetValues(contractCoefficient);
                    return ("ویرایش موفقیت آمیز بود.", true);
                }
                return ("مورد مد نظر جهت ویرایش وجود ندارد.", false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
