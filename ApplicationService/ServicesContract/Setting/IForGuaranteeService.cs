using ApplicationService.DtoModels.SettingDtos;
using AppCore.Entities.SettingEntities.FinePaymentMethods;
using AppCore.Entities.SettingEntities.ForGuarantees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.Setting
{
    public interface IForGuaranteeService
    {
        public Task<Tuple<string, bool>> Add(ForGuaranteeDto forGuarantee);
        public Task<List<ForGuaranteeDto>> GetAll();
        public Task<Tuple<string, bool>> Update(ForGuaranteeDto forGuarantee);
        public Task<Tuple<string, bool>> Delete(Guid forGuarantee);
    }
}
