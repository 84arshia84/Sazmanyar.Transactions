using ApplicationService.DtoModels.FactorDtos.FactorServiceExplanation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.FactorInformation
{
    public interface IFactorServiceExplanationService
    {
        public Task<List<FactorServiceExplanationGetDto>> GetAll(Guid factorId);
        public Task<FactorServiceExplanationGetDto> Get(Guid id);
        public Task<bool> Add(List<FactorServiceExplanationAddDto> serviceExplanation, Guid factorId);
        public Task<bool> Add(FactorServiceExplanationAddDto serviceExplanation,Guid factorId);
        public Task<bool> Delete(Guid id);
        public Task<bool> Update(List<FactorServiceExplanationUpdateDto> serviceExplanation);
    }
}
