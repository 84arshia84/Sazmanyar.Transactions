using AppCore.Entities.WareHouseEntities;
using ApplicationService.DtoModels.WareHouseDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.WareHouses
{
    public interface IWareHouseService
    {
        public Task<List<SupplyListDto>> GetAllSupplyList();
        public Task<List<CommodityDto>> GetAllCommodity(List<Guid> supplyListId);
    }
}
