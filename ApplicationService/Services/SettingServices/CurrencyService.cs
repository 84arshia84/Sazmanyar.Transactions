using ApplicationService.DtoModels.SettingDtos;
using AppCore.UnitOfWork;
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
    internal class CurrencyService : ICurrencyService
    {
        private IUnitOfWork _unitOfWork;
        private readonly IErrorLoggerService _errorLoggerService;
        public CurrencyService(IUnitOfWork unitOfWork, IErrorLoggerService errorLoggerService)
        {
            _unitOfWork = unitOfWork;
            _errorLoggerService = errorLoggerService;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(string message, bool isSuccss)> Add(CurrencyDto currency)
        {
            try
            {
                var model = CurrencyAutoMapperProfile.DtoToEntity(currency, _errorLoggerService);
                model.ID=Guid.NewGuid();
                return await _unitOfWork.CurrencyRepository.Add(model);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("خطا در ثبت ارز", false);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="currencyId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(string message, bool isSuccss)> Delete(Guid currencyId)
        {
            try
            {
                return await _unitOfWork.CurrencyRepository.Delete(currencyId);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("خطا در حذف ارز", false);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="currencyId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<CurrencyDto> Get(Guid currencyId)
        {
            try
            {
                var model=await _unitOfWork.CurrencyRepository.Get(currencyId);
                return CurrencyAutoMapperProfile.EntityToDto(model, _errorLoggerService);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new CurrencyDto();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<CurrencyDto>> GetAll()
        {
            try
            {
                return CurrencyAutoMapperProfile.EntitiesToDtos(await _unitOfWork.CurrencyRepository.GetAll(), _errorLoggerService);
            }
            catch (Exception ex)
            {
               _errorLoggerService.SaveError(ex);
               return new List<CurrencyDto>();  
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(string message, bool isSuccss)> Update(CurrencyDto currency)
        {
            try
            {
                return await _unitOfWork.CurrencyRepository.Update(CurrencyAutoMapperProfile.DtoToEntity(currency, _errorLoggerService));
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("بروزرسانی با خطا مواجه شد.",false);
            }
        }
    }
}
