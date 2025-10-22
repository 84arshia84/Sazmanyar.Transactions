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
    internal class UnitOfMeasurementService : IUnitOfMeasurementService
    {
        private IUnitOfWork _unitOfWork;
        private readonly IErrorLoggerService _errorLoggerService;
        public UnitOfMeasurementService(IUnitOfWork unitOfWork, IErrorLoggerService errorLoggerService)
        {
            _unitOfWork = unitOfWork;
            _errorLoggerService = errorLoggerService;
        }
        /// <summary>
        ///  افزودن واحد اندازه گیری
        /// </summary>
        /// <param name="unitDto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(string message, bool isSuccss)> Add(UnitOfMeasurementDto unitDto)
        {
            try
            {
                var model = UnitOfMeasurementAutoMapperProfile.DtoToEntity(unitDto, _errorLoggerService);
                model.ID = Guid.NewGuid();
                return await _unitOfWork.UnitOfMeasurementRepository.Add(model);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("خطا در ثبت واحد اندازه گیری", false);
            }
        }
        /// <summary>
        /// حذ ف کردن واحد اندازه گیری
        /// </summary>
        /// <param name="unitDto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(string message, bool isSuccss)> Delete(Guid unitDto)
        {
            try
            {
                return await _unitOfWork.UnitOfMeasurementRepository.Delete(unitDto);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("خطا در حذف واحد اندازه گیری", false);
            }
        }
        /// <summary>
        ///  گرفتن واحد اندازه گیری
        /// </summary>
        /// <param name="unitDto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<UnitOfMeasurementDto> Get(Guid unitDto)
        {
            try
            {
                var model = await _unitOfWork.UnitOfMeasurementRepository.Get(unitDto);
                return UnitOfMeasurementAutoMapperProfile.EntityToDto(model, _errorLoggerService);
            }
            catch (Exception ex)
            {

                return new UnitOfMeasurementDto();
            }
        }
        /// <summary>
        /// گرفتن تمامی واحد های اندازه گیری
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<UnitOfMeasurementDto>> GetAll()
        {
            try
            {
                return UnitOfMeasurementAutoMapperProfile.EntitiesToDtos(await _unitOfWork.UnitOfMeasurementRepository.GetAll(), _errorLoggerService);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<UnitOfMeasurementDto>();
            }
        }
        /// <summary>
        /// بروزرسانی واحد اندازه گیری
        /// </summary>
        /// <param name="unitDto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(string message, bool isSuccss)> Update(UnitOfMeasurementDto unitDto)
        {
            try
            {
                return await _unitOfWork.UnitOfMeasurementRepository.Update(UnitOfMeasurementAutoMapperProfile.DtoToEntity(unitDto, _errorLoggerService));
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("بروزرسانی با خطا مواجه شد.", false);
            }
        }
    }
}
