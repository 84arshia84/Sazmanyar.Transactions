using AppCore.Entities.ContractsInformation.Contratcs;
using AppCore.Entities.SettingEntities.PublicSettingEntities.SettingEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.SettingEntities.TransActionTypes
{
    /// <summary>
    /// تیپ معامله
    /// </summary>
    [Table("TransActionTypes", Schema = "TAM")]
    public class TransActionType : SettingEntity
    {
        #region relation

        public List<Contract> Contract { get; set; }
        #endregion
    }
}
