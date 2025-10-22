using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.ContractsInformation.ContractCoefficients
{
    public interface IContractCoefficientRepository
    {
        public Task<(string message , bool isSuccess)> Add(ContractCoefficient contractCoefficient);
        public Task<(string message, bool isSuccess)> Add(List<ContractCoefficient> contractCoefficient);

        public Task<(string message , bool isSuccess)> Update(ContractCoefficient contractCoefficient);
        public Task<(string message, bool isSuccess)> UpdateInAddendum(ContractCoefficient contractCoefficient);
        public Task<List<ContractCoefficient>> GetAll(Guid contractId);
        public Task<List<ContractCoefficient>> GetAllForAddendum(Guid contractId);

        public Task<ContractCoefficient> Get(Guid id);
        public Task<(string message, bool isSuccess)> Delete(Guid id);
        public Task<(string message, bool isSuccess)> DeleteInAddendum(ContractCoefficient contractCoefficient);
    }
}
