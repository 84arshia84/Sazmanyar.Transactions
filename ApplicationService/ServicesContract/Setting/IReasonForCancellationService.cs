using ApplicationService.DtoModels.SettingDtos;
using AppCore.Entities.SettingEntities.Organizationalunits;
using AppCore.Entities.SettingEntities.ReasonForCancellations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.Setting
{
    public interface IReasonForCancellationService
    {
        public Task<Tuple<string, bool>> Add(ReasonForCancellationDto reasonForCancellation);
        public Task<List<ReasonForCancellationDto>> GetAll();
        public Task<Tuple<string, bool>> Update(ReasonForCancellationDto reasonForCancellation);
        public Task<Tuple<string, bool>> Delete(Guid reasonForCancellation);
    }
}
