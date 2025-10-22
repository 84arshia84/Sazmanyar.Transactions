using AppCore.Entities.SettingEntities.PublicSettingEntities.SettingEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.SettingEntities.LookUpTables
{
    /// <summary>
    /// مقدادیر داخل جدول مراجعه ای
    /// </summary>
    public class LookUpTableInside:SettingEntity
    {
        #region properties
        public Guid? ParentId { get; set; }
        #endregion
        #region relation
        public Guid LookUpTableId { get; set; }
        public LookUpTable? LookUpTable { get; set; }
        #endregion
    }
}
