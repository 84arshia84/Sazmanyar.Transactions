using AppCore.Entities.SettingEntities.ContractTypes;
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
    internal class LookUpTableService : ILookUpTableService
    {
        private IUnitOfWork _unitOfWork;
        private IErrorLoggerService _errorLoggerService;
        public LookUpTableService(IUnitOfWork unitOfWork, IErrorLoggerService errorLoggerService)
        {
            _unitOfWork = unitOfWork;
            _errorLoggerService = errorLoggerService;
        }
        public async Task<(string message, bool isSuccess)> Add(LookUpTableDtos lookUpTable)
        {
            try
            {
                lookUpTable.Id = Guid.NewGuid();
                var result = await _unitOfWork.LookUpTableRepository.Add(
                    LookUpTableAutoMapperProfile.DtoToEntity(lookUpTable, _errorLoggerService)
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

        public async Task<(string message, bool isSuccess)> AddInside(LookUpTableInsideDto lookUpTable)
        {
            try
            {
                lookUpTable.key= Guid.NewGuid();
                var result = await _unitOfWork.LookUpTableRepository.AddInside(
                   LookUpTableAutoMapperProfile.DtoToEntity(lookUpTable, _errorLoggerService));
                await _unitOfWork.Save();
                if(result == true)
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

        public async Task<(string message, bool isSuccess)> Delete(Guid lookUpTable)
        {
            try
            {
                var result = await _unitOfWork.LookUpTableRepository.Delete(lookUpTable);
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

        public async Task<(string message, bool isSuccess)> DeleteInside(Guid lookUpTable)
        {
            try
            {
                var result = await _unitOfWork.LookUpTableRepository.DeleteInside(lookUpTable);
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

        public async Task<LookUpTableDtos> Get(Guid lookUpTable)
        {
            try
            {
                return LookUpTableAutoMapperProfile.EntityToDto(
                    await _unitOfWork.LookUpTableRepository.Get(lookUpTable), _errorLoggerService);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new LookUpTableDtos();
            }
        }

        public async Task<List<LookUpTableDtos>> GetAll()
        {
            try
            {
                return LookUpTableAutoMapperProfile.EntitiesToDtos(
                    await _unitOfWork.LookUpTableRepository.GetAll(), _errorLoggerService);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<LookUpTableDtos>();
            }
        }

        public async Task<List<LookUpTableInsideDto>> GetAllInside(Guid lookUpId)
        {
            try
            {
                return LookUpTableAutoMapperProfile.EntitiesToDtos(
                    await _unitOfWork.LookUpTableRepository.GetAllInside(lookUpId));
            }
            catch (Exception ex)
            {

                _errorLoggerService.SaveError(ex);
                return new List<LookUpTableInsideDto>();
            }
        }

        public async Task<(string message, bool isSuccess)> Update(LookUpTableDtos lookUpTable)
        {
            try
            {
                var result = await _unitOfWork.LookUpTableRepository.Update(
                    LookUpTableAutoMapperProfile.DtoToEntity(lookUpTable, _errorLoggerService));
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

        public async Task<(string message, bool isSuccess)> UpdateInside(LookUpTableInsideDto lookUpTable)
        {
            try
            {
                var result = await _unitOfWork.LookUpTableRepository.UpdateInside(
                    LookUpTableAutoMapperProfile.DtoToEntity(lookUpTable, _errorLoggerService));
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
