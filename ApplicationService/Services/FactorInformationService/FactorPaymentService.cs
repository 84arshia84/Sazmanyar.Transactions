using AppCore.Entities.User;
using AppCore.UnitOfWork;
using ApplicationService.DtoModels.FactorDtos.FactorPayment;
using ApplicationService.Mapper.FactorMappers;
using ApplicationService.ServicesContract.ExceptionHandling;
using ApplicationService.ServicesContract.FactorInformation;
using ApplicationService.ServicesContract.Users;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Services.FactorInformationService
{
    internal class FactorPaymentService : IFactorPaymentService
    {
        private IUnitOfWork _unitOfWork;
        private IErrorLoggerService _errorLoggerService;
        private IUserService _userService;
        private IConfiguration _configuration;
        public FactorPaymentService(IUnitOfWork unitOfWork, IErrorLoggerService errorLoggerService, IUserService userService, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _errorLoggerService = errorLoggerService;
            _userService = userService;
            _configuration = configuration;
        }
        public async Task<(string message, bool isSuccess)> Add(FactorPaymentAddDto payment, string userName)
        {
            try
            {
                var user = await _userService.GetByFullQualifyName(userName, _configuration["ConnectionStrings:DbConnection"]);
                var result = await _unitOfWork.FactorPaymentRepository.Add(
                    FactorPaymentAutoMapperProfile.DtoToEntityAdd(payment, user.ID, _errorLoggerService));
                if (result)
                {
                    await _unitOfWork.Save();
                    return ("ثبت موفقیت آمیز بود", true);
                }
                return ("خطا در ثبت ", false);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("خطا در ثبت پرداختی", false);
            }
        }

        public async Task<(string message, bool isSuccess)> Delete(Guid paymentId, string userName)
        {
            try
            {
                var user = await _userService.GetByFullQualifyName(userName, _configuration["ConnectionStrings:DbConnection"]);
                var result = await _unitOfWork.FactorPaymentRepository.Delete(paymentId, user.ID);

                if (result)
                {
                    await _unitOfWork.Save();
                    return ("حذف موفقیت آمیز بود", true);
                }
                return ("خطا در حذف مقدار پرداختی", false);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("خطا در حذف پرداختی", false);
            }
        }

        public async Task<FactorPaymentGetDto> Get(Guid paymentId)
        {
            try
            {
                return FactorPaymentAutoMapperProfile.EntityToDto(
                    await _unitOfWork.FactorPaymentRepository.Get(paymentId), _errorLoggerService);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new FactorPaymentGetDto();
            }
        }

        public async Task<List<FactorPaymentGetDto>> GetAll(Guid factorId)
        {
            try
            {
                return FactorPaymentAutoMapperProfile.EntitiesToDtos(
                    await _unitOfWork.FactorPaymentRepository.GetAll(factorId), _errorLoggerService);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<FactorPaymentGetDto>();
            }
        }

        public async Task<(string message, bool isSuccess)> Update(FactorPaymentUpdateDto payment)
        {
            try
            {
                var lastData = await _unitOfWork.FactorPaymentRepository.Get(payment.Id);
                var result = await _unitOfWork.FactorPaymentRepository.Update(
                    FactorPaymentAutoMapperProfile.DtoToEntityUpdate(payment, lastData, _errorLoggerService));
                if (result)
                {
                    await _unitOfWork.Save();
                    return ("بروزرسانی با موفقیت انجام شد", true);
                }
                return ("در بروزرسانی مقدار پرداختی با خطا مواجه شد ", false);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("خطا در بروزرسانی", false);
            }
        }
        
    }
}
