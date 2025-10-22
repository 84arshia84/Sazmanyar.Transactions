using AppCore.Entities.ContractsInformation.ContractAddendums;
using AppCore.Entities.SettingEntities.CheckLists;
using AppCore.Entities.SettingEntities.PublicSettingEntities.SettingEntity;
using AppCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.SettingEntities.AddendumTypes
{
    /// <summary>
    /// نوع الحاقیه
    /// </summary>
    public class AddendumType :SettingEntity
    {
        #region properties
        public AddendumChangeTypeEnum AddendumChangeType { get; set;}
        #endregion
        #region relation
        public List<ContractAddendum>? ContractAddendum {  get; set;}
        #endregion
    }
}
