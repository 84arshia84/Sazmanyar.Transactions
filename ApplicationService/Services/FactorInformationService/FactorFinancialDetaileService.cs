using AppCore.UnitOfWork;
using ApplicationService.DtoModels.FactorDtos.FactorFinancialDetaile;
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
    internal class FactorFinancialDetaileService : IFactorFinancialDetaileService
    {
        private IUnitOfWork _unitOfWork;
        private IErrorLoggerService _errorLoggerService;
        public FactorFinancialDetaileService(IUnitOfWork unitOfWork, IErrorLoggerService errorLoggerService)
        {
            _unitOfWork = unitOfWork;
            _errorLoggerService = errorLoggerService;
        }
        public async Task<bool> Add(FactorFinancialDetaileAddDto factorFinancialDetaile,Guid financialId, Guid factorId)
        {
            try
            {
                return await _unitOfWork.FactorFinancialDetaileRepository.Add(
                    FactorFinancialDetaileAutoMapperProfile.DtoToEntityAdd(factorFinancialDetaile, financialId, factorId, _errorLoggerService));
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return false;
            }
        }

        public async Task<FactorFinancialDetaileGetDto> Get(Guid factorId)
        {
            try
            {
                return FactorFinancialDetaileAutoMapperProfile.EntityToDto(await _unitOfWork.FactorFinancialDetaileRepository.Get(factorId), _errorLoggerService);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new FactorFinancialDetaileGetDto();
            }
        }

        public async Task<bool> Update(FactorFinancialDetaileUpdateDto factorFinancialDetaile)
        {
            try
            {
                return await _unitOfWork.FactorFinancialDetaileRepository.Update(
                    FactorFinancialDetaileAutoMapperProfile.DtoToEntityUpdate(factorFinancialDetaile, _errorLoggerService));
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
