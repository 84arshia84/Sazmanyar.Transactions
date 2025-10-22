using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.WareHouseEntities
{
    /// <summary>
    /// لیست تامین
    /// </summary>
    public class SupplyList
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ProjectId { get; set; }
        public string ProjectName { get; set; }
        public List<Commodity> Commodity { get; set; }
    }
}
