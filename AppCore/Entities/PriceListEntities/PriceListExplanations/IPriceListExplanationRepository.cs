using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.PriceListEntities.PriceListExplanations
{
    public interface IPriceListExplanationRepository
    {
        public Task<(string message, bool isSuccess)> Add(PriceListExplanation priceListExplanation);
        public Task<List<PriceListExplanation>> GetPriceListExplanationByClauseID(Guid id);
        public Task<PriceListExplanation> Get(Guid id);
        public Task<(string message,bool isSuccess)> Update(PriceListExplanation priceListExplanation);
        public Task<(string message, bool isSuccess)> Delete(Guid id);

    }
}
