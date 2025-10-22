using AppCore.Enums;
using AppCore.UnitOfWork;
using ApplicationService.DtoModels.SettingDtos;
using ApplicationService.Mapper.SettingMapper;
using ApplicationService.ServicesContract.ContractAccessGroups;
using ApplicationService.ServicesContract.ExceptionHandling;
using ApplicationService.ServicesContract.Setting;
using ApplicationService.ServicesContract.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Services.SettingServices
{
    internal class ContractTypeService : IContractTypeService
    {
        private IUnitOfWork _unitOfWork;
        private IErrorLoggerService _errorLoggerService;
        private IContractAccessGroupFilterService _contractAccessGroupFilterService;
        public ContractTypeService(IUnitOfWork unitOfWork, IErrorLoggerService errorLoggerService, IContractAccessGroupFilterService contractAccessGroupFilterService)
        {
            _unitOfWork = unitOfWork;
            _errorLoggerService = errorLoggerService;
            _contractAccessGroupFilterService = contractAccessGroupFilterService;
        }
        public async Task<(string message, bool isSuccess)> Add(ContractTypeDto contractType)
        {
            try
            {
                contractType.Id = Guid.NewGuid();
                var result = await _unitOfWork.ContractTypeRepository.Add(
                    ContractTypeAutoMapperProfile.DtoToEntity(contractType, _errorLoggerService)
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

        public async Task<(string message, bool isSuccess)> Delete(Guid contractType)
        {
            try
            {
                var result = await _unitOfWork.ContractTypeRepository.Delete(contractType);
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

        public async Task<ContractTypeDto> Get(Guid contractType)
        {
            try
            {
                return ContractTypeAutoMapperProfile.EntityToDto(
                    await _unitOfWork.ContractTypeRepository.Get(contractType), _errorLoggerService);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new ContractTypeDto();
            }
        }

        public async Task<List<ContractTypeDto>> GetAll()
        {
            try
            {
                return ContractTypeAutoMapperProfile.EntitiesToDtos(
                    await _unitOfWork.ContractTypeRepository.GetAll(), _errorLoggerService);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<ContractTypeDto>();
            }
        }

        public async Task<List<ContractTypeDto>> GetAllWithAccessGroupEffect(Guid userId, int part, int property, int mode)
        {
            try
            {
                var allTypes = await _unitOfWork.ContractTypeRepository.GetAll();
                var filteredData = await _contractAccessGroupFilterService.FilterContractType(allTypes, userId, (SystemParts)part, (AccessGroupProperties)property, mode);
                if (filteredData != null)
                {
                    return ContractTypeAutoMapperProfile.EntitiesToDtos(filteredData, _errorLoggerService);
                }
                return new List<ContractTypeDto>();
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<ContractTypeDto>();
            }
        }

        public async Task<(string message, bool isSuccess)> Update(ContractTypeDto contractType)
        {
            try
            {
                var result = await _unitOfWork.ContractTypeRepository.Update(
                    ContractTypeAutoMapperProfile.DtoToEntity(contractType, _errorLoggerService));
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

        public async Task<(string message, bool isSuccess)> UpdateOfficeOnline(Guid contractTypeId, Guid? filenameId)
        {
            try
            {
                var result = await _unitOfWork.ContractTypeRepository.UpdateOfficeOnline(contractTypeId, filenameId);
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
