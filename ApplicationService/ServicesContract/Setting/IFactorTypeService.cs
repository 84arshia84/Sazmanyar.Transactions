using ApplicationService.DtoModels.SettingDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.Setting
{
    public interface IFactorTypeService
    {
        public Task<(string message, bool isSuccess)> Add(FactorTypeDto factorType);
        public Task<(string message, bool isSuccess)> Update(FactorTypeDto factorType);
        public Task<(string message, bool isSuccess)> UpdateOfficeOnline(Guid factorTypeId, Guid filenameId);
        public Task<(string message, bool isSuccess)> Delete(Guid factorType);
        public Task<List<FactorTypeDto>> GetAll();
        public Task<List<FactorTypeDto>> GetAllWithAccessGroupEffect(Guid userIid, int property, int mode);
        public Task<FactorTypeDto> Get(Guid factorType);
    }
}
