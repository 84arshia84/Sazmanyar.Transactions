using AppCore.UnitOfWork;
using ApplicationService.DtoModels.DesignCodeDtos.FactorDesignCodeDtos;
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
    public class FactorDesignCodeService : IFactorDesignCodeService
    {
        private IUnitOfWork _unitOfWork;
        private IErrorLoggerService _errorLoggerService;
        public FactorDesignCodeService(IUnitOfWork unitOfWork, IErrorLoggerService errorLoggerService)
        {
            _unitOfWork = unitOfWork;
            _errorLoggerService = errorLoggerService;
        }
        public async Task<(string message, bool isSuccess)> Add(FactorDesignCodeAddDto factorDesignCode)
        {
            try
            {
                await _unitOfWork.FactorDesignCodeRepository.Add(
                 FactorDesignCodeAutoMapper.DtoToEntityAdd(factorDesignCode, _errorLoggerService));
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
                await _unitOfWork.FactorDesignCodeRepository.Delete(id);
                await _unitOfWork.Save();
                return ("حذف موفقیت آمیز بود", true);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("خطا در حذف الگوی کد", false);
            }
        }

        public async Task<(string designcode, Guid designCodeId)> GenerateDesignCode(FactorDesignCodeSearchParameterDto parameterDto)
        {
            try
            {
                var designCode = await _unitOfWork.FactorDesignCodeRepository.GetByParameter(parameterDto.FactorTypeId, parameterDto.OrganizationUnitId,parameterDto.RoleOfOrganizationId);
                var GeneratedCode = GenerateDesingCodeService.GenerateFactorDesingCodeAsync(designCode.DesignCode, designCode.Counter, parameterDto.FactorType, parameterDto.RoleOfOrganization,parameterDto.OrganizationUnit);
                return (GeneratedCode, designCode.Id);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("", Guid.Empty);
            }
        }

        public async Task<List<FactorDesignCodeGetDto>> GetAll()
        {
            try
            {
                return FactorDesignCodeAutoMapper.EntitiesToDtos(
                  await _unitOfWork.FactorDesignCodeRepository.GetAll(), _errorLoggerService);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<FactorDesignCodeGetDto>();
            }
        }

        public async Task<List<FactorDesignCodeGetParameterDto>> GetAllParameter()
        {
            try
            {
                return FactorDesignCodeAutoMapper.EntitiesToDtosParameter(
                    await _unitOfWork.FactorDesignCodeRepository.GetAllParameters(), _errorLoggerService);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<FactorDesignCodeGetParameterDto>();
            }
        }

        public async Task<List<FactorDesignCodeGetParameterDto>> GetAllParameterById(Guid id)
        {
            try
            {
                return FactorDesignCodeAutoMapper.EntitiesToDtosParameter(
                    await _unitOfWork.FactorDesignCodeRepository.GetAllParameters(id), _errorLoggerService);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<FactorDesignCodeGetParameterDto>();
            }
        }

        public async Task<(string message, bool isSuccess)> Update(FactorDesignCodeUpdateDto factorDesignCode)
        {
            try
            {
                var model = FactorDesignCodeAutoMapper.DtoToEntityUpdate(factorDesignCode, _errorLoggerService);
                await _unitOfWork.FactorDesignCodeRepository.DeleteParameters(factorDesignCode.Id);
                await _unitOfWork.Save();
                await _unitOfWork.FactorDesignCodeRepository.Update(model);
                await _unitOfWork.FactorDesignCodeRepository.AddParameters(model.Parameters);
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
                await _unitOfWork.FactorDesignCodeRepository.UpdateCounter(id);
                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
            }
        }
    }
}
