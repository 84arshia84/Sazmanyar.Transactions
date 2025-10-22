using ApplicationService.DtoModels.SettingDtos;
using AppCore.Entities.SettingEntities.FinePaymentMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.Setting
{
    public interface IFinePaymentMethodService
    {
        public Task<Tuple<string, bool>> Add(FinePaymentMethodDto finePaymentMethod);
        public Task<List<FinePaymentMethodDto>> GetAll();
        public Task<Tuple<string, bool>> Update(FinePaymentMethodDto finePaymentMethod);
        public Task<Tuple<string, bool>> Delete(Guid finePaymentMethod);
    }
}
