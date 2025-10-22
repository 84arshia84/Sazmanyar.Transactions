using ApplicationService.DtoModels.SettingDtos;
using AppCore.Entities.SettingEntities.ForGuarantees;
using AppCore.Entities.SettingEntities.HowToPays;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.Setting
{
    public interface IHowToPayService
    {
        public Task<Tuple<string, bool>> Add(HowToPayDto howToPay);
        public Task<List<HowToPayDto>> GetAll();
        public Task<Tuple<string, bool>> Update(HowToPayDto howToPay);
        public Task<Tuple<string, bool>> Delete(Guid howToPay);
    }
}
