using AppCore.UnitOfWork;
using ApplicationService.DtoModels.AccountsDtos;
using ApplicationService.DtoModels.OrganizationInformationDtos;
using ApplicationService.DtoModels.SettingDtos;
using ApplicationService.Mapper.AccountMapperProfile;
using ApplicationService.Mapper.OrganizationInformationMapperProfile;
using ApplicationService.Mapper.SettingMapper;
using ApplicationService.ServicesContract.ExceptionHandling;
using ApplicationService.ServicesContract.OrganizationInformations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Services.OrganizationInformationServices
{
    public class OrganizationInformationService : IOrganizationInformationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IErrorLoggerService _errorLoger;
        public OrganizationInformationService(IUnitOfWork unitOfWork, IErrorLoggerService errorLogger)
        {
            _unitOfWork = unitOfWork;
            _errorLoger = errorLogger;
        }
        public async Task<(string message, bool isSuccess, Guid id)> Add(OrganizationInformationDto organizationInformation)
        {
            try
            {
                organizationInformation.Key = Guid.NewGuid();
                var model = OrganizationInformationMapperProfile.DtoToEntity(organizationInformation);
                var result = await _unitOfWork.OrganizationInformationRepository.Add(model);
                return (result.message, result.isSuccess, organizationInformation.Key);
            }
            catch (Exception ex)
            {
                _errorLoger.SaveError(ex);
                return ("در اتصال به دیتابیس با مشکل مواجه شدیم.", false, Guid.Empty);
            }
        }

        public async Task<(string message, bool isSuccess)> AddAcount(AccountDto account)
        {
            try
            {
                account.Id = Guid.NewGuid();
                var result = await _unitOfWork.OrganizationInformationRepository.AddAcount(
                    AccountMapperProfile.DtoToEntity(account, _errorLoger)
                    );
                await _unitOfWork.Save();
                return result;
            }
            catch (Exception ex)
            {
                _errorLoger.SaveError(ex);
                return ("در اتصال به دیتابیس با مشکل مواجه شدیم.", false);
            }
        }

        public async Task<(string message, bool isSuccess)> DeleteAccount(Guid id)
        {
            try
            {
                var result = await _unitOfWork.OrganizationInformationRepository.DeleteAccount(id);
                if (result.isSuccess)
                {
                    await _unitOfWork.Save();
                    return ("حذف موفقیت آمیز بود", true);
                }
                return result;

            }
            catch (Exception ex)
            {
                _errorLoger.SaveError(ex);
                return ("در اتصال به دیتابیس با مشکل مواجه شدیم.", false);
            }
        }

        /// <summary>
        /// گرفتن تمام طرف معامله حقوقی ها
        /// </summary>
        /// <returns></returns>
        public async Task<OrganizationInformationDto> Get()
        {
            try
            {
                var models = await _unitOfWork.OrganizationInformationRepository.Get();
                if (models != null)
                {
                    return OrganizationInformationMapperProfile.EntityToDto(models);
                }
                return null;
            }
            catch (Exception ex)
            {
                _errorLoger.SaveError(ex);
                return new OrganizationInformationDto();
            }
        }

        public async Task<List<AccountDto>> GetAccounts()
        {
            try
            {
                return AccountMapperProfile.EntitiesToDtos(
                    await _unitOfWork.OrganizationInformationRepository.GetAccounts(), _errorLoger
                    );
            }
            catch (Exception ex)
            {
                _errorLoger.SaveError(ex);
                return new List<AccountDto>();
            }
        }

        /// <summary>
        /// بروزرسانی طرف معامله حقوقی 
        /// </summary>
        /// <param name="basisForEndingTheProject"></param>
        /// <returns></returns>
        public async Task<(string message, bool isSuccess)> Update(OrganizationInformationDto organizationInformationDto)
        {
            try
            {
                var model = OrganizationInformationMapperProfile.DtoToEntity(organizationInformationDto);
                return await _unitOfWork.OrganizationInformationRepository.Update(model);
            }
            catch (Exception ex)
            {
                _errorLoger.SaveError(ex);
                return ("در اتصال به دیتابیس با مشکل مواجه شدیم.", false);
            }
        }

        public async Task<(string message, bool isSuccess)> UpdateAccount(AccountDto account)
        {
            try
            {
                var result = await _unitOfWork.OrganizationInformationRepository.UpdateAccount(
                    AccountMapperProfile.DtoToEntity(account, _errorLoger)
                    );
                if (result.isSuccess)
                {
                    await _unitOfWork.Save();
                }
                return result;
            }
            catch (Exception ex)
            {
                _errorLoger.SaveError(ex);
                return ("در اتصال به دیتابیس با مشکل مواجه شدیم.", false);
            }
        }
    }
}
