using AppCore.Entities.PriceListEntities.PriceLists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.PriceListEntities.PriceListFields
{
    public interface IPriceListFieldRepository
    {
        public Task<List<PriceListField>> GetAll();
        public Task<List<PriceListField>> GetAllByYearId(Guid id);
        public Task<PriceListField> Get(Guid id);
        public Task<(string message, bool isSuccess)> Update(PriceListField priceList);
        public Task<(Guid ID,string message, bool isSuccess)> Add(PriceListField priceList);
        public Task<(string message, bool isSuccess)> Delete(Guid id);
    }
}
