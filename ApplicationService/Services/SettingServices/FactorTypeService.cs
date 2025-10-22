using AppCore.Entities.SettingEntities.ContractTypes;
using AppCore.Entities.User;
using AppCore.Enums;
using AppCore.UnitOfWork;
using ApplicationService.DtoModels.SettingDtos;
using ApplicationService.Mapper.SettingMapper;
using ApplicationService.Services.ContractAccessGroupsServices;
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
    internal class FactorTypeService : IFactorTypeService
    {
        private IUnitOfWork _unitOfWork;
        private IErrorLoggerService _errorLoggerService;
        private IFactorAccessGroupFilterService _factorAccessGroupFilter;
        public FactorTypeService(IUnitOfWork unitOfWork, IErrorLoggerService errorLoggerService, IFactorAccessGroupFilterService factorAccessGroupFilter)
        {
            _unitOfWork = unitOfWork;
            _errorLoggerService = errorLoggerService;
            _factorAccessGroupFilter = factorAccessGroupFilter;
        }
        public async Task<(string message, bool isSuccess)> Add(FactorTypeDto factorType)
        {
            try
            {
                factorType.Id = Guid.NewGuid();
                var result = await _unitOfWork.FactorTypeRepository.Add(
                    FactorTypeAutoMapperProfile.DtoToEntity(factorType, _errorLoggerService)
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

        public async Task<(string message, bool isSuccess)> Delete(Guid factorType)
        {
            try
            {
                var result = await _unitOfWork.FactorTypeRepository.Delete(factorType);
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

        public async Task<FactorTypeDto> Get(Guid factorType)
        {
            try
            {
                return FactorTypeAutoMapperProfile.EntityToDto(
                    await _unitOfWork.FactorTypeRepository.Get(factorType), _errorLoggerService);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new FactorTypeDto();
            }
        }

        public async Task<List<FactorTypeDto>> GetAll()
        {
            try
            {
                return FactorTypeAutoMapperProfile.EntitiesToDtos(
                    await _unitOfWork.FactorTypeRepository.GetAll(), _errorLoggerService);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<FactorTypeDto>();
            }
        }

        public async Task<List<FactorTypeDto>> GetAllWithAccessGroupEffect(Guid userId, int property, int mode)
        {
            try
            {
                var allTypes = await _unitOfWork.FactorTypeRepository.GetAll();
                var filteredData = await _factorAccessGroupFilter.FilterFactorType(allTypes, userId, (EnumFactorAccessGroupProperties)property, mode);
                if (filteredData != null)
                {
                    return FactorTypeAutoMapperProfile.EntitiesToDtos(filteredData, _errorLoggerService);
                }
                return new List<FactorTypeDto>();
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<FactorTypeDto>();
            }
        }

        public async Task<(string message, bool isSuccess)> Update(FactorTypeDto factorType)
        {
            try
            {
                var result = await _unitOfWork.FactorTypeRepository.Update(
                    FactorTypeAutoMapperProfile.DtoToEntity(factorType, _errorLoggerService));
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

        public async Task<(string message, bool isSuccess)> UpdateOfficeOnline(Guid factorTypeId, Guid filenameId)
        {
            try
            {
                var result = await _unitOfWork.FactorTypeRepository.UpdateOfficeOnline(factorTypeId, filenameId);
                if (result == true)
                {
                    await _unitOfWork.Save();
                    return ("سند با موفقیت ثبت اضافه شد", true);

                }
                return ("نوع معمامله جهت افزودن سند یافت نشد", false);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("خطا در ثبت سند", false);
            }
        }
    }
}
