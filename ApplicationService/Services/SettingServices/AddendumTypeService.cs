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
    public class AddendumTypeService : IAddendumTypeService
    {
        private IUnitOfWork _unitOfWork;
        private IErrorLoggerService _errorLoggerService;
        public AddendumTypeService(IUnitOfWork unitOfWork ,IErrorLoggerService errorLoggerService)
        {
            _unitOfWork = unitOfWork;
            _errorLoggerService = errorLoggerService;
        }
        public async Task<(string message, bool isSuccess)> Add(AddendumTypeDto addendumType)
        {
            try
            {
                addendumType.Id = Guid.NewGuid();
                var result =await  _unitOfWork.AddendumTypeRepository.Add(AddendumTypeAutoMapperProfile.DtoToEntity(addendumType,_errorLoggerService));
                if (result == true)
                {
                    await _unitOfWork.Save();
                    return ("ثبت موفقیت آمیز بود", true);
                }
                return ("اجازه ثبت مجدد ندارید.", false);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("ثبت نوع الحاقیه با خطا روبروشد.", false);
            }
        }

        public async Task<(string message, bool isSuccess)> Delete(Guid id)
        {
            try
            {
                var result = await _unitOfWork.AddendumTypeRepository.Delete(id);
                if (result == true)
                {
                    await _unitOfWork.Save();
                    return ("حذف موفقیت آمیز بود", true);
                }
                return ("این نوع الحاقیه در یک الحاقیه استفاده شده است.", false);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("حذف نوع الحاقیه با خطا روبرو شد.", false);
            }
        }

        public async Task<AddendumTypeDto> Get(Guid id)
        {
            try
            {
                return AddendumTypeAutoMapperProfile.EntityToDto(await _unitOfWork.AddendumTypeRepository.Get(id),_errorLoggerService);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new AddendumTypeDto();
            }
        }

        public async Task<List<AddendumTypeDto>> GetAll()
        {
            try
            {
                return AddendumTypeAutoMapperProfile.EntitiesToDtos(
                    await _unitOfWork.AddendumTypeRepository.GetAll(), _errorLoggerService
                    );
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError (ex);
                return new List<AddendumTypeDto>();
            }
        }

        public async Task<(string message, bool isSuccess)> Update(AddendumTypeDto addendumType)
        {
            try
            {
                var result = await _unitOfWork.AddendumTypeRepository.Update(
                    AddendumTypeAutoMapperProfile.DtoToEntity(addendumType,_errorLoggerService)
                    );
                if(result == true)
                {
                    await _unitOfWork.Save();
                    return ("بروزرسانی موفقیت آمیز بود.", true);
                }
                return ("دیتا جهت بروزرسانی پیدا نشد.", false);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("بروزرسانی با خطا روبرو شد.", false);
            }
        }
    }
}
