using AppCore.Entities.SettingEntities.CorespondAndTypeOfCoopRel;
using AppCore.Entities.SettingEntities.PublicSettingEntities.SettingEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.SettingEntities.TypeOfCooperations
{
    /// <summary>
    /// نوع همکاری
    /// </summary>
    [Table("TypeOfCooperation", Schema = "TAM")]
    public class TypeOfCooperation : SettingEntity
    {
        #region properties
        /// <summary>
        /// حرف اول
        /// </summary>
        public string FirstWord { get; set; }
        #endregion

        #region relation 
        public List<CoresponedAndTypeCoopRels>? CorespondAndTypeOfCoopRels { get; set; }
        public List<CorespondRealAndTypeOfCoopRel>? CorespondRealAndTypeOfCoopRels { get; set; }

        #endregion
    }
}
