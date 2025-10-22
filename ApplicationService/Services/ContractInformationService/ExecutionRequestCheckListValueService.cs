using AppCore.Entities.ContractsInformation.ContractCheckListValues;
using AppCore.Entities.ContractsInformation.ExecutionRequestCheckListValues;
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
    public class ExecutionRequestCheckListValueService : IExecutionRequestCheckListValueService
    {
        private IUnitOfWork _unitOfWork;
        private IErrorLoggerService _errorLoggerService;
        public ExecutionRequestCheckListValueService(IUnitOfWork unitOfWork, IErrorLoggerService errorLoggerService)
        {
            _unitOfWork = unitOfWork;
            _errorLoggerService = errorLoggerService;
        }
        public Task<(string message, bool isSuccess)> AddCheckListValues(ExecutionRequestCheckListValue CheckListValue)
        {
            throw new NotImplementedException();
        }

        public async Task<(string message, bool isSuccess)> DeleteCheckListValues(Guid transactionId)
        {
            try
            {
                var result = await _unitOfWork.ExecutionRequestCheckListValueRepository.Delete(transactionId);
                if (result)
                {
                    return ("حذف موفقیت آمیز بود", true);
                }
                return ("موردی جهت حذف وجود ندارد", false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<ExecutionRequestCheckListValue> GetCheckListValues(Guid transactionId)
        {
            throw new NotImplementedException();
        }

        public async Task<(string message, bool isSuccess)> UpdateCheckListValues(ExecutionRequestCheckListValue CheckListValue)
        {
            try
            {
                var result = await _unitOfWork.ExecutionRequestCheckListValueRepository.Update(CheckListValue);
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
