using AppCore.Entities.ContractsInformation.ContractCoefficients;
using ApplicationService.DtoModels.ContractDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.ContractInformation
{
    public interface IContractCoefficientService
    {
        public Task<(string message, bool isSuccess)> AddCoefficient(ContractCoefficientDto contractCoefficient);
        public Task<(string message, bool isSuccess)> AddCoefficient(List<ContractCoefficient> contractCoefficient);
        public Task<(string message, bool isSuccess)> UpdateCoefficient(ContractCoefficientDto contractCoefficient);
        public Task<(string message, bool isSuccess)> UpdateCoefficient(List<ContractCoefficient> contractCoefficient);
        public Task<(string message, bool isSuccess)> UpdateCoefficientInAddendum(List<ContractCoefficient> contractCoefficient,Guid addendumId);
        public Task<List<ContractCoefficientDto>> GetAllCoefficient(Guid contractId);
        public Task<List<ContractCoefficientDto>> GetAllCoefficientForAddendum(Guid contractId,Guid? addendumId);

        public Task<ContractCoefficientDto> GetCoefficient(Guid id);
        public Task<(string message, bool isSuccess)> DeleteCoefficient(Guid id);
        public Task<(string message, bool isSuccess)> DeleteCoefficientInAddendum(ContractCoefficient contractCoefficient, Guid addendumId);
    }
}
