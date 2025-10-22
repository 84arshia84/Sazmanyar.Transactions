using AppCore.Entities.SettingEntities.CorespondentLegals;
using AppCore.Entities.SettingEntities.CorespondentReals;
using AppCore.Entities.SettingEntities.TypeOfCooperations;
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
    [Table("CorespondAndTypeOfCoopRel", Schema = "TAM")]
    public class CoresponedAndTypeCoopRels
    {
        public Guid ID { get; set; }
        public Guid? CoresponedLegalID { get; set; }
        public Guid? TypeOfCoopreationID { get; set; }

    }
}
