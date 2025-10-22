using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.FactorInformation.FactorServiceExplanations
{
    public interface IFactorServiceExplanationRepository
    {
        public Task<List<FactorServiceExplanation>> GetAll(Guid factorId);
        public Task<FactorServiceExplanation> Get(Guid id);
        public Task<bool> Add(List<FactorServiceExplanation> serviceExplanation);
        public Task<bool> Add(FactorServiceExplanation serviceExplanation);
        public Task<bool> Delete(Guid id);
        public Task<bool> Update(FactorServiceExplanation serviceExplanation);
    }
}
