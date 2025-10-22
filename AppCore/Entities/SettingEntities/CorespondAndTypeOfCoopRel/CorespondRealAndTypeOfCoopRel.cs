using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.SettingEntities.CorespondAndTypeOfCoopRel
{
    /// <summary>
    /// اتصال بین طرف معامله و نوع همکاری
    /// </summary>
    [Table("CorespondRealAntTypeOfCoopRel", Schema = "TAM")]
    public class CorespondRealAndTypeOfCoopRel
    {
        public Guid ID { get; set; }
        public Guid? CorespondentRealID { get; set; }
        public Guid? TypeOfCooperationID { get; set; }
    }
}
