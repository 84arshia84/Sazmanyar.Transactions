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
    internal class DefaultCoefficientsService : IDefaultCoefficientsService
    {
        private IUnitOfWork _unitOfWork;
        private IErrorLoggerService _errorLoggerService;

        public DefaultCoefficientsService(IUnitOfWork unitOfWork, IErrorLoggerService errorLoggerService)
        {
            _unitOfWork = unitOfWork;
            _errorLoggerService = errorLoggerService;
        }
        public async Task<(string message, bool isSuccess)> Add(DefaultCoefficientsDto defaultCoefficients)
        {
            try
            {
                defaultCoefficients.Id = Guid.NewGuid();
                await _unitOfWork.DefaultCoefficientsRepository.Add(DefaultCoefficientsAutoMapperProfile.DtoToEntity(defaultCoefficients, _errorLoggerService));
                return ("ثبت یا موفقیت انجام شد.", true);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("ثبت یا خطا مواجه شد.", true);
            }
        }

        public async Task<(string message, bool isSuccess)> Delete(Guid id)
        {
            try
            {
                var result = await _unitOfWork.DefaultCoefficientsRepository.Delete(id);
                if (result)
                {
                    return ("حذف موفقیت آمیز بود.", true);
                }
                return ("حذف با خطا روبرو شد.", false);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("حذف با خطا روبرو شد.", false);
            }
        }

        public async Task<DefaultCoefficientsDto> Get(Guid id)
        {
            try
            {
                return DefaultCoefficientsAutoMapperProfile.EntityToDto
                    (await _unitOfWork.DefaultCoefficientsRepository.Get(id), _errorLoggerService);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new DefaultCoefficientsDto();
            }
        }

        public async Task<List<DefaultCoefficientsDto>> GetAll()
        {
            try
            {
                return DefaultCoefficientsAutoMapperProfile.EntitiesToDtos
                    (await _unitOfWork.DefaultCoefficientsRepository.GetAll(), _errorLoggerService);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<DefaultCoefficientsDto>();
            }
        }

        public async Task<(string message, bool isSuccess)> Update(DefaultCoefficientsDto defaultCoefficients)
        {
            try
            {
                var result = await _unitOfWork.DefaultCoefficientsRepository.Update
                    (DefaultCoefficientsAutoMapperProfile.DtoToEntity(defaultCoefficients, _errorLoggerService));
                if (result)
                {
                    return ("ویرایش موفقیت آمیز بود.", true);
                }
                return ("ویرایش با خطا رویرو شد.", false);

            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("ویرایش با خطا رویرو شد.", false);
            }
        }
    }
}
