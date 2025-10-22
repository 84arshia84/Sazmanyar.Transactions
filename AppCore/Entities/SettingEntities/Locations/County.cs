using AppCore.Entities.SettingEntities.CorespondentLegals;
using AppCore.Entities.SettingEntities.CorespondentReals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.SettingEntities.Locations
{
    /// <summary>
    /// شهرستان
    /// </summary>
    public class County
    {
        #region properties
        public int Id { get; set; }
        public string Name { get; set; }
        #endregion
        #region relation
        public int ProvinceId { get; set; }
        public Province? Province { get; set; }
        public List<City>? Cities { get; set;}
        public List<CorespondentLegal>? CorespondentLegal { get; set; }
        public List<CorespondentReal>? CorespondentReals { get; set; }
        #endregion
    }
}
