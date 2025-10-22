using AppCore.Entities.SettingEntities.CheckLists;
using AppCore.Entities.SettingEntities.PublicSettingEntities.SettingEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.SettingEntities.LookUpTables
{
    /// <summary>
    /// جدول مراجعه ای
    /// </summary>
    public class LookUpTable:SettingEntity
    {
        #region relation
        public List<CheckList>? CheckLists {  get; set; } 
        public List<LookUpTableInside>? LookUpTableInsides { get; set; }
        #endregion
    }
}
