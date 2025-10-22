using ApplicationService.DtoModels.SettingDtos;
using AppCore.Entities.SettingEntities.ReasonForCancellations;
using AppCore.Entities.SettingEntities.ReasonForTerminations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.Setting
{
    public interface IReasonForTerminationService
    {
        public Task<Tuple<string, bool>> Add(ReasonForTerminationDto reasonForTermination);
        public Task<List<ReasonForTerminationDto>> GetAll();
        public Task<Tuple<string, bool>> Update(ReasonForTerminationDto reasonForTermination);
        public Task<Tuple<string, bool>> Delete(Guid reasonForTermination);
    }
}
