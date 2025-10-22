using AppCore.Entities.DesignCodesProperties.ContractDesignCodes;
using AppCore.UnitOfWork;
using ApplicationService.DtoModels.DesignCodeDtos.TransactionExecutionDesignCodeDtos;
using ApplicationService.Mapper.DesignCodeMapper;
using ApplicationService.ServicesContract.DesignCode;
using ApplicationService.ServicesContract.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Services.DesignCodeServices
{
    public class TransactionExecutionDesignCodeService : ITransactionExecutionDesignCodeService
    {
        private IUnitOfWork _unitOfWork;
        private IErrorLoggerService _errorLoggerService;
        public TransactionExecutionDesignCodeService(IUnitOfWork unitOfWork, IErrorLoggerService errorLoggerService)
        {
            _unitOfWork = unitOfWork;
            _errorLoggerService = errorLoggerService;
        }
        public async Task<(string message, bool isSuccess)> Add(TransactionDesignCodeAddDto transactionDesignCode)
        {
            try
            {
                await _unitOfWork.TransActionExecutionDesignCodeRepository.Add(
                  TransactionDesignCodeAutoMapper.DtoToEntityAdd(transactionDesignCode, _errorLoggerService));
                await _unitOfWork.Save();
                return ("ثبت موفقیت آمیز بود", true);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("خطا در ذخیره سازه الگوی کد", false);
            }
        }

        public async Task<(string message, bool isSuccess)> Delete(Guid id)
        {
            try
            {
                await _unitOfWork.TransActionExecutionDesignCodeRepository.Delete(id);
                await _unitOfWork.Save();
                return ("حذف موفقیت آمیز بود", true);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("خطا در حذف الگوی کد", false);
            }
        }

        public async Task<(string designcode, Guid designCodeId)> GenerateDesignCode(TransactionDesignCodeSearchParameterDto parameterDto)
        {
            try
            {

                var designCode = await _unitOfWork.TransActionExecutionDesignCodeRepository.GetByParameter(parameterDto.ContractTypeIds, parameterDto.OrganizationUnitId);
                var GeneratedCode = GenerateDesingCodeService.GenerateTransactionDesingCodeAsync(designCode.DesignCode, designCode.Counter, parameterDto.ContractType, parameterDto.OrganizationUnit);
                return (GeneratedCode, designCode.Id);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("", Guid.Empty);
            }
        }

        public async Task<List<TransactionDesignCodeGetDto>> GetAll()
        {
            try
            {
                return TransactionDesignCodeAutoMapper.EntitiesToDtos(
                   await _unitOfWork.TransActionExecutionDesignCodeRepository.GetAll(), _errorLoggerService);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<TransactionDesignCodeGetDto>();
            }
        }

        public async Task<List<TransactionDesignCodeGetParameterDto>> GetAllParameter()
        {
            try
            {
                return TransactionDesignCodeAutoMapper.EntitiesToDtosParameter(
                     await _unitOfWork.TransActionExecutionDesignCodeRepository.GetAllParameters(), _errorLoggerService);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<TransactionDesignCodeGetParameterDto> ();
            }
        }

        public async Task<List<TransactionDesignCodeGetParameterDto>> GetAllParameterById(Guid id)
        {
            try
            {
                return TransactionDesignCodeAutoMapper.EntitiesToDtosParameter(
                    await _unitOfWork.TransActionExecutionDesignCodeRepository.GetAllParameters(id), _errorLoggerService);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<TransactionDesignCodeGetParameterDto>();
            }
        }

        public async Task<(string message, bool isSuccess)> Update(TransactionDesignCodeUpdateDto transactionDesignCode)
        {
            try
            {
                var model = TransactionDesignCodeAutoMapper.DtoToEntityUpdate(transactionDesignCode, _errorLoggerService);
                await _unitOfWork.TransActionExecutionDesignCodeRepository.DeleteParameters(transactionDesignCode.Id);
                await _unitOfWork.Save();
                await _unitOfWork.TransActionExecutionDesignCodeRepository.Update(model);
                await _unitOfWork.TransActionExecutionDesignCodeRepository.AddParameters(model.Parameters);
                await _unitOfWork.Save();
                return (" بروزرسانی الگوی کد موفقیت آمیز بود", true);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("خطا در بروزرسانی الگوی کد", false);
            }
        }

        public async Task UpdateCounter(Guid id)
        {
            try
            {
                await _unitOfWork.TransActionExecutionDesignCodeRepository.UpdateCounter(id);
                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                
            }
        }
    }
}
