using AppCore.Entities.SettingEntities.PublicSettingEntities.SettingEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.SettingEntities.BasisFortheEndOftheProjects
{
    /// <summary>
    /// مبنای پایان پروژه
    /// </summary>
    [Table("BasisFortheEndOftheProject", Schema = "TAM")]
    public class BasisFortheEndOftheProject : SettingEntity
    {
    }
}
