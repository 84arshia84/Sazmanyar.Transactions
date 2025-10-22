using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.WareHouseDtos
{
    public class CommodityDto
    {
        public Guid key { get; set; }
        public int Row { get; set; }
        public string Name { get; set; }
        public string CommodityCode { get; set; }
        public string SupplyDate { get; set; }
        public decimal Amount { get; set; }
        public decimal TheRequiredAmount { get; set; }
        public decimal FreeInventory { get; set; }
        public decimal ReserveInventory { get; set; }
        public decimal OnTheWayInventory { get; set; }
        public decimal ExitedInventory { get; set; }
        public Guid SupplyListId { get; set; }
        public string SupplyListName { get; set; }
    }
}
