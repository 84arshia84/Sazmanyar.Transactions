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
    public class StatusService :IStatusService
    {
        private IUnitOfWork _unitOfWork;
        private IErrorLoggerService _errorLoggerService;
        public StatusService(IUnitOfWork unitOfWork, IErrorLoggerService errorLoggerService)
        {
            _unitOfWork = unitOfWork;
            _errorLoggerService = errorLoggerService;
        }
        public async Task<(string message, bool isSuccess)> Add(StatusDto status)
        {
            try
            {
                var result = await _unitOfWork.StatusRepository.Add(StatusAutoMapperProfile.DtoToEntity(status));
                if (result == true)
                {
                    await _unitOfWork.Save();
                    return ("ثبت موفقیت آمیز بود", true);
                }
                return ("خطا در ثبت وضعیت قرارداد", false);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("خطا در ثبت وضعیت قرارداد", false);
            }
        }

        public async Task<(string message, bool isSuccess)> Delete(Guid statusId)
        {
            try
            {
                var result = await _unitOfWork.StatusRepository.Delete(statusId);
                if (result == true)
                {
                    await _unitOfWork.Save();
                    return ("حذف موقیت آمیز بود", true);
                }
                return ("این وضعیت قبلا در یک قرارداد استفاده شده و نمی توان حذفش کرد", false);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("خطا در حذف وضعیت قرارداد", false);
            }
        }

        public async Task<StatusDto> Get(Guid statusId)
        {
            try
            {
                return StatusAutoMapperProfile.EntityToDto(await _unitOfWork.StatusRepository.Get(statusId));
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new StatusDto();
            }
        }

        public async Task<List<StatusDto>> GetAll()
        {
            try
            {
                return StatusAutoMapperProfile.EntitiesToDtos(await _unitOfWork.StatusRepository.GetAll());
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<StatusDto>();
            }
        }

        public async Task<StatusDto> GetByContractId(Guid contractId)
        {
            try
            {
                var model = await _unitOfWork.StatusRepository.GetByContractId(contractId);
                if (model == null)
                {
                    return null;
                }
                return StatusAutoMapperProfile.EntityToDto(model);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new StatusDto();
            }
        }

        public async Task<(string message, bool isSuccess)> Update(StatusDto status)
        {
            try
            {
                var result = await _unitOfWork.StatusRepository.Update(StatusAutoMapperProfile.DtoToEntity(status));
                if (result == true)
                {
                    await _unitOfWork.Save();
                    return ("ویرایش وضعیت قرارداد موفقیت آمیز بود", true);
                }
                return ("مورد مد نظر جهت ویرایش پیدا نشد", false);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("خطا در بروزرسانی وضعیت قرارداد", false);
            }
        }
    }
}
