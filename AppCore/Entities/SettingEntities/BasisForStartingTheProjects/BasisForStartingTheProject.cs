using AppCore.Entities.ContractsInformation.ContratTimeProfiles;
using AppCore.Entities.SettingEntities.PublicSettingEntities.SettingEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.SettingEntities.BasisForStartingTheProjects
{
    /// <summary>
    /// مبنای شروع پروژه
    /// </summary>
    [Table("BasisForStartingTheProject", Schema = "TAM")]
    public class BasisForStartingTheProject : SettingEntity
    {
        #region relation
        public List<ContractTimeProfile> ContractTimeProfile { get; set; }
        #endregion
    }
}
