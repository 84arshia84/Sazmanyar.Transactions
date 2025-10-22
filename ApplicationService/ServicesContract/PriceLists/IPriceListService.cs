using ApplicationService.DtoModels.PriceListsDtos;
using AppCore.Entities.PriceListEntities.PriceLists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.PriceLists
{
    public interface IPriceListService
    {
        public Task<(string message,bool isSuccess)> AddPriceListByExcel(Stream file);
        public Task<(Guid ID,string message,bool isSuccess)> Add(PriceListDtos priceListDtos);
        public Task<(string message, bool isSuccess)> Update(PriceListDtos priceListDtos);
        public Task<(string message, bool isSuccess)> Delete(Guid id);
        public Task<(List<PriceList>, string)> GetAllPriceLists();
        public Task<List<PriceList>> GetAllYears();


    }
}
