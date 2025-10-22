using AppCore.Entities.FactorInformation.Factors;
using AppCore.Entities.SettingEntities.PublicSettingEntities.SettingEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.SettingEntities.FactorTypes
{
    /// <summary>
    /// نوع فاکتور
    /// </summary>
    public class FactorType:SettingEntity
    {
        #region property
        public Guid? OfficeOnlineDocumentId { get; set; }
        #endregion
        #region relation 
        public List<Factor>? Factors { get; set; }
        #endregion
    }
}
