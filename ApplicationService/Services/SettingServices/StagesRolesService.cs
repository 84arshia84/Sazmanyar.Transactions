using ApplicationService.DtoModels.SettingDtos;
using AppCore.UnitOfWork;
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
    internal class StagesRolesService : IStagesRolesService
    {
        private IUnitOfWork _unitOfWork;
        private readonly IErrorLoggerService _errorLoggerService;
        public StagesRolesService(IUnitOfWork unitOfWork, IErrorLoggerService errorLoggerService)
        {
            _unitOfWork = unitOfWork;
            _errorLoggerService = errorLoggerService;
        }
        public async Task<(string message, bool isSuccss)> Add(StagesRolesDto stagesRoles)
        {
            try
            {
                var model = StagesRolesMapper.DtoToEntity(stagesRoles, _errorLoggerService);
                model.Id = Guid.NewGuid();
                return await _unitOfWork.StagesRolesRepository.Add(model);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("خطا در ثبت!", false);
            }
        }
        public async Task<(string message, bool isSuccss)> Delete(Guid stagesRolesId)
        {
            try
            {
                return await _unitOfWork.StagesRolesRepository.Delete(stagesRolesId);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("خطا در حذف!", false);
            }
        }
        public async Task<StagesRolesDto> Get(Guid currencyId)
        {
            try
            {
                var model = await _unitOfWork.StagesRolesRepository.Get(currencyId);
                return StagesRolesMapper.EntityToDto(model, _errorLoggerService);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new StagesRolesDto();
            }
        }
        public async Task<List<StagesRolesDto>> GetAll()
        {
            try
            {
                return StagesRolesMapper.EntitiesToDtos(await _unitOfWork.StagesRolesRepository.GetAll(), _errorLoggerService);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<StagesRolesDto>();
            }
        }
        public async Task<(string message, bool isSuccss)> Update(StagesRolesDto currency)
        {
            try
            {
                return await _unitOfWork.StagesRolesRepository.Update(StagesRolesMapper.DtoToEntity(currency, _errorLoggerService));
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("بروزرسانی با خطا مواجه شد.", false);
            }
        }
    }
}
