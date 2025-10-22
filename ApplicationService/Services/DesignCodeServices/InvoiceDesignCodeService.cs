using AppCore.Entities.DesignCodesProperties.FactorDesignCodes;
using AppCore.UnitOfWork;
using ApplicationService.DtoModels.DesignCodeDtos.FactorDesignCodeDtos;
using ApplicationService.DtoModels.DesignCodeDtos.InvoiceDesignCodeDtos;
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
    public class InvoiceDesignCodeService : IInvoiceDesignCodeService
    {
        private IUnitOfWork _unitOfWork;
        private IErrorLoggerService _errorLoggerService;
        public InvoiceDesignCodeService(IUnitOfWork unitOfWork, IErrorLoggerService errorLoggerService)
        {
            _unitOfWork = unitOfWork;
            _errorLoggerService = errorLoggerService;
        }
        public async Task<(string message, bool isSuccess)> Add(InvoiceDesignCodeAddDto invoiceDesignCode)
        {
            try
            {
                await _unitOfWork.InvoiceDesignCodeRepository.Add(
                 InvoiceDesignCodeAutoMapper.DtoToEntityAdd(invoiceDesignCode, _errorLoggerService));
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
                await _unitOfWork.InvoiceDesignCodeRepository.Delete(id);
                await _unitOfWork.Save();
                return ("حذف موفقیت آمیز بود", true);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("خطا در حذف الگوی کد", false);
            }
        }

        public async Task<(string designcode, Guid designCodeId)> GenerateDesignCode(InvoiceDesignCodeSearchParameterDto parameterDto)
        {
            try
            {
                var designCode = await _unitOfWork.InvoiceDesignCodeRepository.GetByParameter(parameterDto.ContractTypeIds, parameterDto.OrganizationUnitId, parameterDto.RoleOfOrganizationId,parameterDto.InvoiceTypeId);
                var GeneratedCode = GenerateDesingCodeService.GenerateInvoiceDesingCodeAsync(designCode.DesignCode, designCode.Counter, parameterDto.ContractType, parameterDto.RoleOfOrganization, parameterDto.OrganizationUnit,parameterDto.InvoiceType);
                return (GeneratedCode, designCode.Id);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("", Guid.Empty);
            }
        }

        public async Task<List<InvoiceDesignCodeGetDto>> GetAll()
        {
            try
            {
                return InvoiceDesignCodeAutoMapper.EntitiesToDtos(
                  await _unitOfWork.InvoiceDesignCodeRepository.GetAll(), _errorLoggerService);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<InvoiceDesignCodeGetDto>();
            }
        }

        public async Task<List<InvoiceDesignCodeGetParameterDto>> GetAllParameter()
        {
            try
            {
                return InvoiceDesignCodeAutoMapper.EntitiesToDtosParameter(
                    await _unitOfWork.InvoiceDesignCodeRepository.GetAllParameters(), _errorLoggerService);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<InvoiceDesignCodeGetParameterDto>();
            }
        }

        public async Task<List<InvoiceDesignCodeGetParameterDto>> GetAllParameterById(Guid id)
        {
            try
            {
                return InvoiceDesignCodeAutoMapper.EntitiesToDtosParameter(
                    await _unitOfWork.InvoiceDesignCodeRepository.GetAllParameters(id), _errorLoggerService);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<InvoiceDesignCodeGetParameterDto>();
            }
        }

        public async Task<(string message, bool isSuccess)> Update(InvoiceDesignCodeUpdateDto invoiceDesignCode)
        {
            try
            {
                var model = InvoiceDesignCodeAutoMapper.DtoToEntityUpdate(invoiceDesignCode, _errorLoggerService);
                await _unitOfWork.InvoiceDesignCodeRepository.DeleteParameters(invoiceDesignCode.Id);
                await _unitOfWork.Save();
                await _unitOfWork.InvoiceDesignCodeRepository.Update(model);
                await _unitOfWork.InvoiceDesignCodeRepository.AddParameters(model.Parameters);
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
                await _unitOfWork.InvoiceDesignCodeRepository.UpdateCounter(id);
                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
            }
        }
    }
}
