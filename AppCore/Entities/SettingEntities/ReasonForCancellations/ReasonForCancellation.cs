using AppCore.Entities.SettingEntities.PublicSettingEntities.SettingEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.SettingEntities.ReasonForCancellations
{
    /// <summary>
    /// علت فسخ
    /// </summary>
    [Table("ReasonForCancellation", Schema = "TAM")]
    public class ReasonForCancellation : SettingEntity
    {
    }
}
