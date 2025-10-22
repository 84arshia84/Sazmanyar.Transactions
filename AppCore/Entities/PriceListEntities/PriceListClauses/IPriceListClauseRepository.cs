using AppCore.Entities.PriceListEntities.PriceListExplanations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.PriceListEntities.PriceListClauses
{
    public interface IPriceListClauseRepository
    {
        public Task<(Guid ID,string message, bool isSuccess)> Add(PriceListClause priceListExplanation);
        public Task<List<PriceListClause>> GetAll();
        public Task<List<PriceListClause>> GetAllByFieldId(Guid id);
        public Task<PriceListClause> Get(Guid id);
        public Task<(string message, bool isSuccess)> Update(PriceListClause priceListExplanation);
        public Task<(string message, bool isSuccess)> Delete(Guid id);
    }
}
