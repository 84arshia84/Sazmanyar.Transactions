using AppCore.Entities.SettingEntities.PublicSettingEntities.SettingEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.SettingEntities.ReasonForTerminations
{
    /// <summary>
    /// علت خاتمه
    /// </summary>
    [Table("ReasonForTermination", Schema = "TAM")]
    public class ReasonForTermination : SettingEntity
    {
    }
}
