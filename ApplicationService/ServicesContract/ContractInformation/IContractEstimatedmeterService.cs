using ApplicationService.DtoModels.ContractDtos;
using AppCore.Entities.ContractsInformation.ContractEstimatedmeters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.ContractInformation
{
    public interface IContractEstimatedmeterService
    {
        public Task<(string message,bool isSuccess)> AddEstimatedmeter(ContractEstimatedmeter estimatedmeter);
        public Task<(string message, bool isSuccess)> AddEstimatedmeter(List<ContractEstimatedmeter> estimatedmeters);
        public Task<(string message, bool isSuccess)> Update(ContractEstimatedmeter estimatedmeter);
        public Task<(string message, bool isSuccess)> Update(List<ContractEstimatedmeter> estimatedmeter);
        public Task<(string message, bool isSuccess)> UpdateInAddendum(List<ContractEstimatedmeter> estimatedmeter,Guid AddendumId);
        public Task<(string message, bool isSuccess)> Delete(Guid id);
        public Task<(string message, bool isSuccess)> DeleteInAddendum(ContractEstimatedmeter estimatedmeter,Guid addendumId);
        public Task<(string message, bool isSuccess)> DeleteAll(Guid serviceExplanationId); 
        public Task<EstimatedmeterDto> Get(Guid id);
        public Task<List<EstimatedmeterDto>> GetAll(Guid serviceExplanationId);
        public Task<List<EstimatedmeterDto>> GetAllForAddendum(Guid contractId,Guid? addendumId);

    }
}
