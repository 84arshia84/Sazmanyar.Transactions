using AppCore.Entities.SettingEntities.ContractTypes;
using AppCore.Entities.User;
using AppCore.Enums;
using AppCore.UnitOfWork;
using ApplicationService.DtoModels.SettingDtos;
using ApplicationService.Mapper.SettingMapper;
using ApplicationService.ServicesContract.ContractAccessGroups;
using ApplicationService.ServicesContract.ExceptionHandling;
using ApplicationService.ServicesContract.FactorAccessGroups;
using ApplicationService.ServicesContract.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Services.SettingServices
{
    internal class RoleOfOrganizationService : IRoleOfOrganizationService
    {
        private IUnitOfWork _unitOfWork;
        private IErrorLoggerService _errorLoggerService;
        private IContractAccessGroupFilterService _contractAccessGroupFilterService;
        private IFactorAccessGroupFilterService _factorAccessGroupFilter;
        public RoleOfOrganizationService(IUnitOfWork unitOfWork, IErrorLoggerService errorLoggerService, IContractAccessGroupFilterService contractAccessGroupFilterService
            , IFactorAccessGroupFilterService factorAccessGroupFilter)
        {
            _unitOfWork = unitOfWork;
            _contractAccessGroupFilterService = contractAccessGroupFilterService;
            _errorLoggerService = errorLoggerService;
            _factorAccessGroupFilter = factorAccessGroupFilter;
        }
        public async Task<(string message, bool isSuccess)> Add(RoleOfOrganizationDto roleOfOrganization)
        {
            try
            {
                roleOfOrganization.key = Guid.NewGuid();
                var result = await _unitOfWork.RoleOfOrganizationRepository.Add(
                    RoleOfOrganizationAutoMapperProfile.DtoToEntity(roleOfOrganization, _errorLoggerService)
                    );
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
                return ("خطا در اتصال به دیتابیس", false);
            }
        }

        public async Task<(string message, bool isSuccess)> Delete(Guid roleOfOrganization)
        {
            try
            {
                var result = await _unitOfWork.RoleOfOrganizationRepository.Delete(roleOfOrganization);
                await _unitOfWork.Save();
                if (result == true)
                {
                    return ("حذف موفقیت آمیز بود.", true);
                }
                return ("این مقدار در قرارداد استفاده شده و نمی توان آن را حذف کرد.", false);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("خطا در اتصال به دیتابیس", false);
            }
        }

        public async Task<RoleOfOrganizationDto> Get(Guid roleOfOrganization)
        {
            try
            {
                return RoleOfOrganizationAutoMapperProfile.EntityToDto(
                    await _unitOfWork.RoleOfOrganizationRepository.Get(roleOfOrganization), _errorLoggerService);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new RoleOfOrganizationDto();
            }
        }

        public async Task<List<RoleOfOrganizationDto>> GetAll()
        {
            try
            {
                return RoleOfOrganizationAutoMapperProfile.EntitiesToDtos(
                    await _unitOfWork.RoleOfOrganizationRepository.GetAll(), _errorLoggerService);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<RoleOfOrganizationDto>();
            }
        }

        public async Task<List<RoleOfOrganizationDto>> GetAllWithAccessGroupEffect(Guid userId, int part, int property, int mode)
        {
            try
            {
                var allRoles = await _unitOfWork.RoleOfOrganizationRepository.GetAll();
                var filteredData = await _contractAccessGroupFilterService.FilterRoleOfOrganization(allRoles, userId, (SystemParts)part, (AccessGroupProperties)property, mode);
                if (filteredData != null)
                {
                    return RoleOfOrganizationAutoMapperProfile.EntitiesToDtos(filteredData,_errorLoggerService);
                }
                return new List<RoleOfOrganizationDto>();
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<RoleOfOrganizationDto>();
            }
        }

        public async Task<List<RoleOfOrganizationDto>> GetAllWithFactorAccessGroupEffect(Guid userId, int property, int mode)
        {
            try
            {
                var allRoles = await _unitOfWork.RoleOfOrganizationRepository.GetAll();
                var filteredData = await _factorAccessGroupFilter.FilterRoleOfOrganization(allRoles, userId, (EnumFactorAccessGroupProperties)property, mode);
                if (filteredData != null)
                {
                    return RoleOfOrganizationAutoMapperProfile.EntitiesToDtos(filteredData, _errorLoggerService);
                }
                return new List<RoleOfOrganizationDto>();
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<RoleOfOrganizationDto>();
            }
        }

        public async Task<(string message, bool isSuccess)> Update(RoleOfOrganizationDto roleOfOrganization)
        {
            try
            {
                var result = await _unitOfWork.RoleOfOrganizationRepository.Update(
                    RoleOfOrganizationAutoMapperProfile.DtoToEntity(roleOfOrganization, _errorLoggerService));
                await _unitOfWork.Save();
                if (result == true)
                {
                    return ("ویرایش موفقیت آمیز بود", true);
                }
                return ("مقدار تکراری نمی توان ثبت کرد", false);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("خطا در اتصال به دیتابیس", false);
            }
        }
    }
}
