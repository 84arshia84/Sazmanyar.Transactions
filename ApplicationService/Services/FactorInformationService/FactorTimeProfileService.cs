using AppCore.UnitOfWork;
using ApplicationService.DtoModels.FactorDtos.FactorTimeProfile;
using ApplicationService.Mapper.FactorMappers;
using ApplicationService.ServicesContract.ExceptionHandling;
using ApplicationService.ServicesContract.FactorInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Services.FactorInformationService
{
    internal class FactorTimeProfileService : IFactorTimeProfileService
    {
        private IUnitOfWork _unitOfWork;
        private IErrorLoggerService _errorLoggerService;
        public FactorTimeProfileService(IUnitOfWork unitOfWork, IErrorLoggerService errorLoggerService)
        {
            _unitOfWork = unitOfWork;
            _errorLoggerService = errorLoggerService;
        }
        public async Task<bool> Add(FactorTimeProfileAddDto factorTimeProfile, Guid timeprofileId, Guid factorId)
        {
            try
            {
                return await _unitOfWork.FactorTimeProfileRepository.Add(
                    FactorTimeProfileAutoMapperProfile.DtoToEntityAdd(factorTimeProfile, timeprofileId, factorId, _errorLoggerService));
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return false;
            }
        }

        public async Task<FactorTimeProfileGetDto> Get(Guid factorId)
        {
            try
            {
                return FactorTimeProfileAutoMapperProfile.EntityToDto(await _unitOfWork.FactorTimeProfileRepository.Get(factorId), _errorLoggerService);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new FactorTimeProfileGetDto();
            }
        }

        public async Task<bool> Update(FactorTimeProfileUpdateDto factorTimeProfile)
        {
            try
            {
                return await _unitOfWork.FactorTimeProfileRepository.Update(
                    FactorTimeProfileAutoMapperProfile.DtoToEntityUpdate(factorTimeProfile, _errorLoggerService));
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return false;
            }
        }
    }
}
