using AppCore.UnitOfWork;
using ApplicationService.DtoModels.SettingDtos;
using ApplicationService.Mapper.SettingMapper;
using ApplicationService.ServicesContract.ExceptionHandling;
using ApplicationService.ServicesContract.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Services.SettingServices
{
    internal class TypeOfGuaranteeService : ITypeOfGuaranteeService
    {
        private IUnitOfWork _unitOfWork;
        private readonly IErrorLoggerService _errorLoggerService;
        public TypeOfGuaranteeService(IUnitOfWork unitOfWork, IErrorLoggerService errorLoggerService)
        {
            _unitOfWork = unitOfWork;
            _errorLoggerService = errorLoggerService;
        }
        public async Task<(string message, bool IsSuccess)> Add(TypeOfGuaranteeDto guarantee)
        {
            try
            {
                guarantee.Id = Guid.NewGuid();
                var result = await _unitOfWork.TypeOfGuaranteeRepository.Add(TypeOfGuaranteeAutoMapperProfiler.DtoToEntity(guarantee,_errorLoggerService));
                await _unitOfWork.Save();
                if (result == true)
                {
                    return ("ثبت موفقیت آمیز بود", true);
                }
                return ("مقدار تکراری نمی توان ثبت کرد", false);
            }
            catch (Exception ex)
            {
               _errorLoggerService.SaveError(ex);
                return ("در اتصال به دیتابیس با خطا مواجه شدیم", false);
                
            }
        }

        public async Task<(string message, bool IsSuccess)> Delete(Guid guarantee)
        {
            try
            {
                var result = await _unitOfWork.TypeOfGuaranteeRepository.Delete(guarantee);
                await _unitOfWork.Save();
                if(result == true)
                {
                    return ("حذف موفقیت آمیز بود", true);
                }
                return ("این مورد در یک قرارداد استفاده شده",false);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("در اتصال به دیتابیس با خطا مواجه شدیم", false);

            }
        }

        public async Task<TypeOfGuaranteeDto> Get(Guid guaranteeId)
        {
            try
            {
                return TypeOfGuaranteeAutoMapperProfiler.EntityToDto(
                    await _unitOfWork.TypeOfGuaranteeRepository.Get(guaranteeId),_errorLoggerService);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new TypeOfGuaranteeDto();

            }
        }

        public async Task<List<TypeOfGuaranteeDto>> GetAll()
        {
            try
            {
                return TypeOfGuaranteeAutoMapperProfiler.EntitiesToDtos(
                    await _unitOfWork.TypeOfGuaranteeRepository.GetAll(), _errorLoggerService);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<TypeOfGuaranteeDto>();

            }
        }

        public async Task<(string message, bool IsSuccess)> Update(TypeOfGuaranteeDto guarantee)
        {
            try
            {
                var result = await _unitOfWork.TypeOfGuaranteeRepository.Update(
                    TypeOfGuaranteeAutoMapperProfiler.DtoToEntity(guarantee,_errorLoggerService));
                await _unitOfWork.Save();
                if(result == true)
                {
                    return ("ویرایش موفقیت آمیز بود", true);
                }
                return ("مقدار تکراری نمی توان ثبت کرد", false);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("در اتصال به دیتابیس با خطا مواجه شدیم", false);

            }
        }
    }
}
