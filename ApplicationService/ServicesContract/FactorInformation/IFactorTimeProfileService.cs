using ApplicationService.DtoModels.FactorDtos.FactorTimeProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.FactorInformation
{
    public interface IFactorTimeProfileService
    {
        public Task<bool> Add(FactorTimeProfileAddDto factorTimeProfile,Guid timeprofileId, Guid factorId);
        public Task<FactorTimeProfileGetDto> Get(Guid factorId);
        public Task<bool> Update(FactorTimeProfileUpdateDto factorTimeProfile);
    }
}
