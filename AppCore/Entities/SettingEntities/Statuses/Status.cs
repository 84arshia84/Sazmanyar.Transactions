using AppCore.Entities.ContractsInformation.Contratcs;
using AppCore.Entities.SettingEntities.PublicSettingEntities.SettingEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.SettingEntities.Statuses
{
    /// <summary>
    /// وضعیت
    /// </summary>
    public class Status:SettingEntity
    {
        #region relation
        public List<Contract>? Contracts { get; set; }
        #endregion
    }
}
