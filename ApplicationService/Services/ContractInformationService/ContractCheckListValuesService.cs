using AppCore.Entities.ContractsInformation.ContractCheckListValues;
using AppCore.UnitOfWork;
using ApplicationService.ServicesContract.ContractInformation;
using ApplicationService.ServicesContract.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Services.ContractInformationService
{
    internal class ContractCheckListValuesService : IContractCheckListValuesService
    {
        private IUnitOfWork _unitOfWork;
        private IErrorLoggerService _errorLoggerService;
        public ContractCheckListValuesService(IUnitOfWork unitOfWork, IErrorLoggerService errorLoggerService)
        {
            _unitOfWork = unitOfWork;
            _errorLoggerService = errorLoggerService;
        }
        public Task<(string message, bool isSuccess)> AddCheckListValues(ContractCheckListValue contractCheckListValue)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// زمانی که قرار داد چک لیست نداشته باشد اتفاقی نمی افتد 
        /// ولی اگر یک بار چک لیست ثبت کرده باشد و هنگام ویرایش چک لیست حذف بشود این تابع فراخوانی می شود.
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        public async Task<(string message, bool isSuccess)> DeleteCheckListValues(Guid contractId)
        {
            try
            {
                var result = await _unitOfWork.ContractCheckListValuesRepository.Delete(contractId);
                if (result)
                {
                    return ("حذف موفقیت آمیز بود",true);
                }
                return ("موردی جهت حذف وجود ندارد", false);
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

        public Task<ContractCheckListValue> GetCheckListValues(Guid contractId)
        {
            throw new NotImplementedException();
        }

        public async Task<(string message, bool isSuccess)> UpdateCheckListValues(ContractCheckListValue contractCheckListValue)
        {
            try
            {
                var result = await _unitOfWork.ContractCheckListValuesRepository.Update(contractCheckListValue);
                if (result)
                {
                    return ("بروزرسانی موفقیت آمیز بود", true);
                }
                return ("بروزرسانی با خطا مواجه شد", false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
