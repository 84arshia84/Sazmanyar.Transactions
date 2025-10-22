using ApplicationService.DtoModels.SettingDtos;
using AppCore.UnitOfWork;
using ApplicationService.Mapper.SettingMapper;
using ApplicationService.ServicesContract.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationService.ServicesContract.ExceptionHandling;
using AppCore.Entities.User;
using AppCore.Enums;
using ApplicationService.ServicesContract.ContractAccessGroups;
using ApplicationService.ServicesContract.FactorAccessGroups;

namespace ApplicationService.Services.SettingServices
{
    internal class OrganizationalunitService : IOrganizationalunitService
    {
        private IUnitOfWork _unitOfWork;
        private IErrorLoggerService _errorLoggerService;
        private IContractAccessGroupFilterService _contractAccessGroupFilterService;
        private IFactorAccessGroupFilterService _factorAccessGroupFilter;
        public OrganizationalunitService(IUnitOfWork unitOfWork,IErrorLoggerService errorLoggerService, IContractAccessGroupFilterService contractAccessGroupFilterService
            , IFactorAccessGroupFilterService factorAccessGroupFilter)
        {
            _unitOfWork = unitOfWork;
            _errorLoggerService = errorLoggerService;
            _contractAccessGroupFilterService = contractAccessGroupFilterService;
            _factorAccessGroupFilter = factorAccessGroupFilter;
        }
        /// <summary>
        /// اضافه کردن واحد سازمانی به دیتابیس
        /// </summary>
        /// <param name="organizationalunitDto"></param>
        /// <returns></returns>
        public async Task<Tuple<string, bool>> Add(OrganizationalunitDto organizationalunitDto)
        {
            try
            {
                organizationalunitDto.key = Guid.NewGuid();
                var model = OrganizationalunitAutoMapperProfile.DtoToEntity(organizationalunitDto);
                return await _unitOfWork.OrganizationalunitRepository.Add(model);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return Tuple.Create("در اتصال به دیتابیس با مشکل مواجه شدیم.", false);
            }
        }
        /// <summary>
        /// حذف  واحد سازمانی 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Tuple<string, bool>> Delete(Guid id)
        {
            try
            {
                return await _unitOfWork.OrganizationalunitRepository.Delete(id);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return Tuple.Create("در اتصال به دیتابیس با مشکل مواجه شدیم.", false);
            }
        }
        /// <summary>
        /// گرفتن تمام واحد سازمانی
        /// </summary>
        /// <returns></returns>
        public async Task<List<OrganizationalunitDto>> GetAll()
        {
            try
            {
                var models = await _unitOfWork.OrganizationalunitRepository.GetAll();
                return OrganizationalunitAutoMapperProfile.EntitiesToDtos(models);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<OrganizationalunitDto>();
            }
        }

        public async  Task<List<OrganizationalunitDto>> GetAllWithAccessGroupEffect(Guid userId, int part, int property, int mode)
        {
            try
            {
                var allOrganUnits = await _unitOfWork.OrganizationalunitRepository.GetAll();
                var filteredData = await _contractAccessGroupFilterService.FilterOrganizationalunit(allOrganUnits, userId, (SystemParts)part, (AccessGroupProperties)property, mode);
                if (filteredData != null)
                {
                    return OrganizationalunitAutoMapperProfile.EntitiesToDtos(filteredData);
                }
                return new List<OrganizationalunitDto>();
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<OrganizationalunitDto>();
            }
        }

        public async Task<List<OrganizationalunitDto>> GetAllWithFactorAccessGroupEffect(Guid userId, int property, int mode)
        {
            try
            {
                var allOrganUnits = await _unitOfWork.OrganizationalunitRepository.GetAll();
                var filteredData = await _factorAccessGroupFilter.FilterOrganizationalunit(allOrganUnits, userId, (EnumFactorAccessGroupProperties)property, mode);
                if (filteredData != null)
                {
                    return OrganizationalunitAutoMapperProfile.EntitiesToDtos(filteredData);
                }
                return new List<OrganizationalunitDto>();
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<OrganizationalunitDto>();
            }
        }

        /// <summary>
        /// بروزرسانی واحد سازمانی 
        /// </summary>
        /// <param name="organizationalunitDto"></param>
        /// <returns></returns>
        public async Task<Tuple<string, bool>> Update(OrganizationalunitDto organizationalunitDto)
        {
            try
            {
                var model = OrganizationalunitAutoMapperProfile.DtoToEntity(organizationalunitDto);
                return await _unitOfWork.OrganizationalunitRepository.Update(model);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return Tuple.Create("در اتصال به دیتابیس با مشکل مواجه شدیم.", false);
            }
        }
    }
}
