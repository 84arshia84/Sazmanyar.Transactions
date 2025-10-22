using ApplicationService.DtoModels.DesignCodeDtos.TransactionExecutionDesignCodeDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.DesignCode
{
    public interface ITransactionExecutionDesignCodeService
    {
        public Task<(string message, bool isSuccess)> Add(TransactionDesignCodeAddDto transactionDesignCode);
        public Task<(string message, bool isSuccess)> Update(TransactionDesignCodeUpdateDto transactionDesignCode);
        public Task UpdateCounter(Guid id);
        public Task<(string message, bool isSuccess)> Delete(Guid id);
        public Task<List<TransactionDesignCodeGetDto>> GetAll();
        public Task<List<TransactionDesignCodeGetParameterDto>> GetAllParameter();
        public Task<List<TransactionDesignCodeGetParameterDto>> GetAllParameterById(Guid id);
        public Task<(string designcode, Guid designCodeId)> GenerateDesignCode(TransactionDesignCodeSearchParameterDto parameterDto);
    }
}
