using ApplicationService.DtoModels.DesignCodeDtos.FactorDesignCodeDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.DesignCode
{
    public interface IFactorDesignCodeService
    {
        public Task<(string message, bool isSuccess)> Add(FactorDesignCodeAddDto factorDesignCode);
        public Task<(string message, bool isSuccess)> Update(FactorDesignCodeUpdateDto factorDesignCode);
        public Task UpdateCounter(Guid id);
        public Task<(string message, bool isSuccess)> Delete(Guid id);
        public Task<List<FactorDesignCodeGetDto>> GetAll();
        public Task<List<FactorDesignCodeGetParameterDto>> GetAllParameter();
        public Task<List<FactorDesignCodeGetParameterDto>> GetAllParameterById(Guid id);
        public Task<(string designcode, Guid designCodeId)> GenerateDesignCode(FactorDesignCodeSearchParameterDto parameterDto);
    }
}
