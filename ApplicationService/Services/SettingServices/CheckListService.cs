using AppCore.Entities.SettingEntities.CheckLists;
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
    internal class CheckListService : ICheckListService
    {
        private IUnitOfWork _unitOfWork;
        private IErrorLoggerService _errorLoggerService;
        public CheckListService(IUnitOfWork unitOfWork, IErrorLoggerService errorLoggerService)
        {
            _unitOfWork = unitOfWork;
            _errorLoggerService = errorLoggerService;
        }
        public async Task<(string message, bool isSuccess)> Add(CheckListDto checkList)
        {
            try
            {
                checkList.Id = Guid.NewGuid();
                var result = await _unitOfWork.CheckListRepository.Add(
                    CheckListAutoMapperProfile.DtoToEntity(checkList, _errorLoggerService)
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

        public async Task<(string message, bool isSuccess)> Delete(Guid checkList, Guid contractTypeGuid)
        {
            try
            {
                var result = await _unitOfWork.CheckListRepository.Delete(checkList, contractTypeGuid);
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

        public async  Task<CheckListDto> Get(Guid checkList)
        {
            try
            {
                return CheckListAutoMapperProfile.EntityToDto(
                    await _unitOfWork.CheckListRepository.Get(checkList), _errorLoggerService);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new CheckListDto();
            }
        }

        public async Task<List<CheckListDto>> GetAll(Guid contractType)
        {
            try
            {
                return CheckListAutoMapperProfile.EntitiesToDtos(
                    await _unitOfWork.CheckListRepository.GetAll(contractType), _errorLoggerService);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<CheckListDto>();
            }
        }

        public async Task<(string message, bool isSuccess)> Update(CheckListDto checkList)
        {
            try
            {
                var result = await _unitOfWork.CheckListRepository.Update(
                    CheckListAutoMapperProfile.DtoToEntity(checkList, _errorLoggerService));
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
        public async Task<List<LookUpTableInsideDto>> GetLookUpTreeByCheckListId(Guid checkListId)
        {
            try
            {
                var checkList = await _unitOfWork.CheckListRepository.Get(checkListId);
                if (checkList?.LookUpTableId == null)
                    return new List<LookUpTableInsideDto>();

                var lookUpItems = await _unitOfWork.LookUpTableRepository.GetAllInside(checkList.LookUpTableId.Value);

                return LookUpTableAutoMapperProfile.EntitiesToDtos(lookUpItems);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<LookUpTableInsideDto>();
            }
        }
    }
}
