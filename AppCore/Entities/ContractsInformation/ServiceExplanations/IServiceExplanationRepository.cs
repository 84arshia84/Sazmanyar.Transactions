using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.ContractsInformation.ServiceExplanations
{
    public interface IServiceExplanationRepository
    {
        #region Crud
        public Task<List<ServiceExplanation>> GetAll(Guid ContractId);
        public Task<List<ServiceExplanation>> GetAllForAddendum(Guid ContractId);
        public Task DeleteServiceExplanationOfAddendum(Guid addendumId);
        public Task<ServiceExplanation> Get(Guid id);
        public Task<(string message, bool isSuccess)> Add(List<ServiceExplanation> serviceExplanation);
        public Task<(string message, bool isSuccess)> Add(ServiceExplanation serviceExplanation);
        public Task<(string message, bool isSuccess)> Delete(Guid id); 
        public Task<(string message, bool isSuccess)> DeleteInAddendum(ServiceExplanation serviceExplanation);
        public Task<(string message, bool isSuccess)> Update(ServiceExplanation serviceExplanation);
        public Task<(string message, bool isSuccess)> UpdateInAddendum(ServiceExplanation serviceExplanation);
        // get isFromExection
        public Task<List<ServiceExplanation>> GetWithIsFromExecution(Guid contracId);
        #endregion

    }
}
