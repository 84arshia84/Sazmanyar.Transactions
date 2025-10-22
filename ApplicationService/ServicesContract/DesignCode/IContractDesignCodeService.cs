using ApplicationService.DtoModels.DesignCodeDtos.ContractDesignCodeDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.DesignCode
{
    public interface IContractDesignCodeService
    {
        public Task<(string message, bool isSuccess)> Add(ContractDesignCodeAddDto contractDesignCode);
        public Task<(string message , bool isSuccess)> Update(ContractDesignCodeUpdateDto contractDesignCode);
        public Task UpdateCounter(Guid id);
        public Task<(string message, bool isSuccess)> Delete(Guid id);
        public Task<List<ContractDesignCodeGetDto>> GetAll();
        public Task<List<ContractDesignCodeGetParameterDto>> GetAllParameter();
        public Task<List<ContractDesignCodeGetParameterDto>> GetAllParameterById(Guid id);
        public Task<(string designcode, Guid designCodeId)> GenerateDesignCode(ContractDesignCodeSearchParameterDto parameterDto);
    }
}
