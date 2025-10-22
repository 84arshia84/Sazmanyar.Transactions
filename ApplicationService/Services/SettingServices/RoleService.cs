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
    internal class RoleService : IRoleService
    {
        private IUnitOfWork _unitOfWork;
        private readonly IErrorLoggerService _errorLoggerService;
        public RoleService(IUnitOfWork unitOfWork, IErrorLoggerService errorLoggerService)
        {
            _unitOfWork = unitOfWork;
            _errorLoggerService = errorLoggerService;
        }
        public async Task<List<RoleDto>> GetAll()
        {
            try
            {
                return RoleMapper.EntitiesToDtos(await _unitOfWork.RoleRepository.GetAll(), _errorLoggerService);
            }
            catch(Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<RoleDto>();
            }
        }
    }
}
