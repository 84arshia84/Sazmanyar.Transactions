using AppCore.Entities.DesignCodesProperties.ContractDesignCodes;
using AppCore.UnitOfWork;
using ApplicationService.DtoModels.DesignCodeDtos.ContractDesignCodeDtos;
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
    public class ContractDesignCodeService : IContractDesignCodeService
    {
        private IUnitOfWork _unitOfWork;
        private IErrorLoggerService _errorLoggerService;
        public ContractDesignCodeService(IUnitOfWork unitOfWork, IErrorLoggerService errorLoggerService)
        {
            _unitOfWork = unitOfWork;
            _errorLoggerService = errorLoggerService;
        }
        public async Task<(string message, bool isSuccess)> Add(ContractDesignCodeAddDto contractDesignCode)
        {
            try
            {
                await _unitOfWork.ContractDesignCodeRepository.Add(
                    ContractDesignCodeAutoMapper.DtoToEntityAdd(contractDesignCode, _errorLoggerService));
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

                await _unitOfWork.ContractDesignCodeRepository.Delete(id);
                await _unitOfWork.Save();
                return ("حذف موفقیت آمیز بود", true);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("خطا در حذف الگوی کد", false);
            }
        }

        public async Task<List<ContractDesignCodeGetDto>> GetAll()
        {
            try
            {
                return ContractDesignCodeAutoMapper.EntitiesToDtos(
                    await _unitOfWork.ContractDesignCodeRepository.GetAll(), _errorLoggerService);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<ContractDesignCodeGetDto>();
            }
        }

        public async Task<List<ContractDesignCodeGetParameterDto>> GetAllParameter()
        {
            try
            {
                return ContractDesignCodeAutoMapper.EntitiesToDtosParameter(
                     await _unitOfWork.ContractDesignCodeRepository.GetAllParameters(), _errorLoggerService);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<ContractDesignCodeGetParameterDto>();
            }
        }

        public async Task<List<ContractDesignCodeGetParameterDto>> GetAllParameterById(Guid id)
        {
            try
            {
                return ContractDesignCodeAutoMapper.EntitiesToDtosParameter(
                    await _unitOfWork.ContractDesignCodeRepository.GetAllParameters(id), _errorLoggerService);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<ContractDesignCodeGetParameterDto>();
            }
        }

        public async Task<(string message, bool isSuccess)> Update(ContractDesignCodeUpdateDto contractDesignCode)
        {
            try
            {
                var model = ContractDesignCodeAutoMapper.DtoToEntityUpdate(contractDesignCode, _errorLoggerService);
                await _unitOfWork.ContractDesignCodeRepository.DeleteParameters(contractDesignCode.Id);
                await _unitOfWork.Save();
                await _unitOfWork.ContractDesignCodeRepository.Update(model);
                await _unitOfWork.ContractDesignCodeRepository.AddParameters(model.Parameters);
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
                await _unitOfWork.ContractDesignCodeRepository.UpdateCounter(id);
                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
            }
        }
        public async Task<(string designcode, Guid designCodeId)> GenerateDesignCode(ContractDesignCodeSearchParameterDto parameterDto)
        {
            try
            {
                var designCode = await _unitOfWork.ContractDesignCodeRepository.GetByParameter(parameterDto.ContractTypeIds, parameterDto.OrganizationUnitId, parameterDto.RoleOfOrganizationId);
                var GeneratedCode = GenerateDesingCodeService.GenerateContractDesingCodeAsync(designCode.DesignCode, designCode.Counter, parameterDto.ContractType, parameterDto.RoleOfOrganization, parameterDto.OrganizationUnit);
                return (GeneratedCode, designCode.Id);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("", Guid.Empty);
            }
        }
    }
}
