using AppCore.UnitOfWork;
using ApplicationService.DtoModels.FactorDtos.FactorServiceExplanation;
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
    internal class FactorServiceExplanationService : IFactorServiceExplanationService
    {
        private IUnitOfWork _unitOfWork;
        private IErrorLoggerService _errorLoggerService;
        public FactorServiceExplanationService(IUnitOfWork unitOfWork, IErrorLoggerService errorLoggerService)
        {
            _unitOfWork = unitOfWork;
            _errorLoggerService = errorLoggerService;
        }
        public async Task<bool> Add(List<FactorServiceExplanationAddDto> serviceExplanation, Guid factorId)
        {
            try
            {
                var result =  await _unitOfWork.FactorServiceExplanationRepository.Add(
                    FactorServiceExplanationAutoMapperProfile.DtosToEntitesAdd(serviceExplanation, factorId, _errorLoggerService));
                return result;
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return false;
            }
        }

        public async Task<bool> Add(FactorServiceExplanationAddDto serviceExplanation, Guid factorId)
        {
            try
            {
                var result = await _unitOfWork.FactorServiceExplanationRepository.Add(
                    FactorServiceExplanationAutoMapperProfile.DtoToEntityAdd(serviceExplanation, factorId, _errorLoggerService));
                await _unitOfWork.Save();
                return result;
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return false;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                return await _unitOfWork.FactorServiceExplanationRepository.Delete(id);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return false;
            }
        }

        public async Task<FactorServiceExplanationGetDto> Get(Guid id)
        {
            try
            {
                return FactorServiceExplanationAutoMapperProfile.EntityToDto(
                    await _unitOfWork.FactorServiceExplanationRepository.Get(id), _errorLoggerService);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                throw;
            }
        }

        public async Task<List<FactorServiceExplanationGetDto>> GetAll(Guid factorId)
        {
            try
            {
                return FactorServiceExplanationAutoMapperProfile.EntitiesToDtos(
                    await _unitOfWork.FactorServiceExplanationRepository.GetAll(factorId), _errorLoggerService);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                throw;
            }
        }

        public async Task<bool> Update(List<FactorServiceExplanationUpdateDto> serviceExplanation)
        {
            try
            {
                foreach (var item in serviceExplanation)
                {
                    try
                    {
                        if (item.IsDeleted)
                        {
                            await Delete(item.Id);
                            continue;
                        }
                        if (item.IsUpdated)
                        {
                            await _unitOfWork.FactorServiceExplanationRepository.Update(
                               FactorServiceExplanationAutoMapperProfile.DtoToEntityUpdate(item, _errorLoggerService));
                        }
                    }
                    catch (Exception ex)
                    {
                        _errorLoggerService.SaveError(ex);
                        continue;
                    }

                }
                return true;
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return false;
            }
        }
    }
}
