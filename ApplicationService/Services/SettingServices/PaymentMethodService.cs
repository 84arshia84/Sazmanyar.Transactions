using ApplicationService.DtoModels.SettingDtos;
using AppCore.Entities.SettingEntities.PaymentMethods;
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
    internal class PaymentMethodService : IPaymentMethodService
    {
        private IUnitOfWork _unitOfWork;
        private readonly IErrorLoggerService _errorLoggerService;
        public PaymentMethodService(IUnitOfWork unitOfWork, IErrorLoggerService errorLoggerService)
        {
            _unitOfWork = unitOfWork;
            _errorLoggerService = errorLoggerService;
        }
        public async Task<(string message, bool isSuccss)> Add(PaymentMethodDto paymentMethod)
        {
            try
            {
                var model = PaymentMethodAutoMapperProfile.DtoToEntity(paymentMethod, _errorLoggerService);
                model.ID = Guid.NewGuid();
                return await _unitOfWork.PaymentMethodRepository.Add(model);
            }
            catch (Exception ex)
            {
                await _errorLoggerService.SaveError(ex);
                return ("خطا در ثبت روش پرداخت", false);
            }
        }
        public async Task<(string message, bool isSuccss)> Delete(Guid paymentMethodId)
        {
            try
            {
                return await _unitOfWork.PaymentMethodRepository.Delete(paymentMethodId);
            }
            catch (Exception ex)
            {
                await _errorLoggerService.SaveError(ex);
                return ("خطا در حذف ارز", false);
            }
        }
        public async Task<PaymentMethodDto> Get(Guid paymentMethodId)
        {
            try
            {
                var model = await _unitOfWork.PaymentMethodRepository.Get(paymentMethodId);
                return PaymentMethodAutoMapperProfile.EntityToDto(model, _errorLoggerService);
            }
            catch (Exception ex)
            {
                return new PaymentMethodDto();
            }
        }
        public async Task<List<PaymentMethodDto>> GetAll()
        {
            try
            {
                return PaymentMethodAutoMapperProfile.EntitiesToDtos(await _unitOfWork.PaymentMethodRepository.GetAll(), _errorLoggerService);
            }
            catch (Exception ex)
            {
                return new List<PaymentMethodDto>();
            }
        }
        public async Task<(string message, bool isSuccss)> Update(PaymentMethodDto paymentMethod)
        {
            try
            {
                return await _unitOfWork.PaymentMethodRepository.Update(PaymentMethodAutoMapperProfile.DtoToEntity(paymentMethod, _errorLoggerService));
            }
            catch (Exception ex)
            {
                return ("بروزرسانی با خطا مواجه شد.", false);
            }
        }
    }
}
