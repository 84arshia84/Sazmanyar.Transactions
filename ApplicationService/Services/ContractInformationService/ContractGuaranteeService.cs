using AppCore.Entities.ContractsInformation.ContractGuarantees;
using AppCore.UnitOfWork;
using ApplicationService.DtoModels.ContractDtos;
using ApplicationService.Mapper.ContractInformationMapper;
using ApplicationService.ServicesContract.ContractInformation;
using ApplicationService.ServicesContract.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Services.ContractInformationService
{
    internal class ContractGuaranteeService : IContractGuaranteeService
    {
        private IUnitOfWork _unitOfWork;
        private IErrorLoggerService _errorLoggerService;
        public ContractGuaranteeService(IUnitOfWork unitOfWork, IErrorLoggerService errorLoggerService)
        {
            _unitOfWork = unitOfWork;
            _errorLoggerService = errorLoggerService;
        }
        public async Task<bool> AddGuarantee(List<ContractGuarantee> contractGuarantee)
        {
            try
            {
                return await _unitOfWork.ContractGuaranteeRepository.Add(contractGuarantee);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return false;
            }
        }

        public async Task<bool> DeleteGuarantee(Guid contractGuarantee)
        {
            try
            {
                return await _unitOfWork.ContractGuaranteeRepository.Delete(contractGuarantee);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return false;
            }
        }

        public async Task<List<ContractGuaranteeDto>> GetAll(Guid contractId)
        {
            try
            {
                return await ContractGuaranteeAutoMapperProfile.EntitiesToDtos(
                    await _unitOfWork.ContractGuaranteeRepository.GetAll(contractId),_errorLoggerService);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<ContractGuaranteeDto>();
            }
        }

        public async Task<bool> UpdateGuarantee(List<ContractGuarantee> contractGuarantee)
        {
            try
            {
                foreach (var item in contractGuarantee)
                {
                    if (item.IsDeleted == true)
                    {
                        await DeleteGuarantee(item.Id);
                        continue;
                    }
                    await _unitOfWork.ContractGuaranteeRepository.Update(item);
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
