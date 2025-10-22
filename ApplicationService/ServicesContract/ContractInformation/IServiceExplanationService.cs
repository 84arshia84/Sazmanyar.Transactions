using AppCore.Entities.ContractsInformation.ServiceExplanations;
using ApplicationService.DtoModels.ContractDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.ContractInformation
{
    public interface IServiceExplanationService
    {
        #region Crud
        public  Task<(string message, bool isSuccess)> AddServiceExplanation(List<ServiceExplanation> serviceExplanation);
        public  Task<List<ServiceExplanationDto>> GetAllServiceExplanations(Guid contractId);
        public Task<List<ServiceExplanationDto>> GetAllServiceExplenationForAddendum(Guid contractId, Guid? addendumId);
        public Task<List<ServiceExplanation>> GetAllServiceExplenationOfAddendumForInvoice(Guid contractId, Guid? addendumId);
        public Task<ServiceExplanationDto> GetById(Guid id);
        public Task<(string message, bool isSuccess)> UpdateServiceExplanation(List<ServiceExplanation> serviceExplanation);
        public Task<(string message, bool isSuccess)> UpdateServiceExplanationInAddendum(List<ServiceExplanation> serviceExplanation,Guid AddendumId);
        public Task<(string message, bool isSuccess)> DeleteServiceExplanation(Guid contractId); 
        public Task<(string message, bool isSuccess)> DeleteInAddendum(ServiceExplanation serviceExplanation,Guid addendumId);
        public Task<bool> DeleteAllServiceExplanationForThisAddendum(Guid addendumId);
        // get with IsFromExection
        public Task<List<ServiceExplanationDto>> GetWithIsFromExecution(Guid contractId);
        #endregion
    }
}
