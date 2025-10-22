using AppCore.Entities.ContractsInformation.ExecutionRequestServiceExplanations;
using AppCore.Entities.ContractsInformation.ServiceExplanations;
using AppCore.UnitOfWork;
using ApplicationService.DtoModels.ContractDtos;
using ApplicationService.Mapper.ContractInformationMapper;
using ApplicationService.ServicesContract.ContractInformation;
using ApplicationService.ServicesContract.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Services.ContractInformationService
{
    public class ExecutionRequestServiceExplanationService : IExecutionRequestServiceExplanationService
    {
        private IUnitOfWork _unitOfWork;
        private IErrorLoggerService _errorLoggerService;
        public ExecutionRequestServiceExplanationService(IUnitOfWork unitOfWork, IErrorLoggerService errorLoggerService)
        {
            _unitOfWork = unitOfWork;
            _errorLoggerService = errorLoggerService;
        }
        public async Task<(string message, bool isSuccess)> AddServiceExplanation(List<ExecutionRequestServiceExplanation> serviceExplanation)
        {
            try
            {
                var result = await _unitOfWork.ExecutionRequestServiceExplanationRepository.Add(serviceExplanation);
                return result;
            }
            catch (Exception ex)
            {

                _errorLoggerService.SaveError(ex);
                return ("در اتصال به دیتابیس با مشکل مواجه شدیم.", false);
            }
        }

        public async Task<(string message, bool isSuccess)> DeleteServiceExplanation(Guid serviceExplanationId)
        {
            try
            {
                return await _unitOfWork.ExecutionRequestServiceExplanationRepository.Delete(serviceExplanationId);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("در اتصال به دیتابیس با مشکل مواجه شدیم.", false);
            }
        }

        public async Task<List<ExecutionRequestServiceExplanationDto>> GetAllServiceExplanations(Guid transactionId)
        {
            try
            {
                return ExecutionRequestServiceExplanationAutoMapperProfile.EntitiesToDtos(await _unitOfWork.ExecutionRequestServiceExplanationRepository.GetAll(transactionId), _errorLoggerService);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<ExecutionRequestServiceExplanationDto>();
            }
        }

        public async Task<(string message, bool isSuccess)> UpdateServiceExplanation(List<ExecutionRequestServiceExplanation> serviceExplanation)
        {
            try
            {
                foreach (var item in serviceExplanation)
                {
                    if (item.IsDeleted == true)
                    {
                        await DeleteServiceExplanation(item.ID);
                        continue;
                    }
                    await _unitOfWork.ExecutionRequestServiceExplanationRepository.Update(item);
                }
                return ("ویرایش شرح خدمت موفقیت آمیز بود.", true);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("در اتصال به دیتابیس با مشکل مواجه شدیم.", false);
            }
        }


        // to get all the serviceExplanations of a FinalApproved TransactionExecutionRequest
        public async Task<List<ExecutionRequestServiceExplanationDto>> GetAllWithTransactionIsFinalApproved(Guid transactionId)
        {
            try
            {
                return ExecutionRequestServiceExplanationAutoMapperProfile.EntitiesToDtos(await _unitOfWork.ExecutionRequestServiceExplanationRepository.GetAllWithTransnactionApproved(transactionId), _errorLoggerService);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<ExecutionRequestServiceExplanationDto>();
            }
        }
        // GetAllFromContract With CommodityId=> servicesExplanations that came from WareHouse 
        public async Task<List<ExecutionRequestServiceExplanationDto>> GetAllFromContract(Guid transactionId)
        {
            try
            {
                List<ServiceExplanation> se = await _unitOfWork.ServiceExplanationRepository.GetAll(transactionId);

                List<ExecutionRequestServiceExplanation> tse = await _unitOfWork.ExecutionRequestServiceExplanationRepository.GetWithComodity(transactionId);

                List<Guid> seIds = se.Select(s => s.ID).ToList();

                var filteredTse = tse.Where(t => seIds.Contains(t.ID)).ToList();

                var dtos = ExecutionRequestServiceExplanationAutoMapperProfile.EntitiesToDtos(filteredTse, _errorLoggerService);
                return dtos;
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<ExecutionRequestServiceExplanationDto>();
            }
        }
    }
}
