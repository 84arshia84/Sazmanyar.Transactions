using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.WareHouseEntities
{
    public interface IWareHouseRepository
    {
        public Task<List<SupplyList>> GetAllSupplyList(string query, string connectionString);
        public Task<List<Commodity>> GetAllCommodity(List<Guid> supplyListId,string query, string connectionString);
    }
}
