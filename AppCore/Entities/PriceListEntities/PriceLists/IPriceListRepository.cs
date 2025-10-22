using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.PriceListEntities.PriceLists
{
    public interface IPriceListRepository
    {
        public Task<(string response, bool isSuccess)> ImportExcel(List<PriceList> priceList);
        public Task<(List<PriceList> models, string result)> GetAllPriceList();
        public Task<List<PriceList>> GetAllYears();
        public Task<PriceList> Get(Guid id);
        public Task<(string message,bool isSuccess)> Update(PriceList priceList);
        public Task<(Guid ID,string message, bool isSuccess)> Add(PriceList priceList);
        public Task<(string message,bool isSuccess)> Delete(Guid id);
    }
}
