using ApplicationService.DtoModels.SettingDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.Setting
{
    public interface ITypeOfGuaranteeService
    {
        public Task<(string message, bool IsSuccess)> Add(TypeOfGuaranteeDto guarantee);
        public Task<(string message, bool IsSuccess)> Update(TypeOfGuaranteeDto guarantee);
        public Task<(string message, bool IsSuccess)> Delete(Guid guarantee);
        public Task<List<TypeOfGuaranteeDto>> GetAll();
        public Task<TypeOfGuaranteeDto> Get(Guid guaranteeId);
    }
}
