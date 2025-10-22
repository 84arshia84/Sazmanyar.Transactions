using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.WareHouseEntities
{
    /// <summary>
    /// کالا
    /// </summary>
    public class Commodity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// کد کالا
        /// </summary>
        public string CommodityCode {  get; set; }
        /// <summary>
        /// ناریخ تامین
        /// </summary>
        public string SupplyDate { get; set; }
        /// <summary>
        /// تعداد
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// تعداد مورد نیاز
        /// </summary>
        public decimal TheRequiredAmount { get; set; }
        /// <summary>
        /// موجودی آزاد
        /// </summary>
        public decimal FreeInventory { get; set; }
        /// <summary>
        /// موجودی رزور
        /// </summary>
        public decimal ReserveInventory { get; set; }
        /// <summary>
        /// موجودی در راه
        /// </summary>
        public decimal OnTheWayInventory { get; set; }
        /// <summary>
        /// موجودی خارج شده
        /// </summary>
        public decimal ExitedInventory { get; set; }
        public Guid SupplyListId { get; set; }
        public string SupplyListName { get; set; }
    }
}
