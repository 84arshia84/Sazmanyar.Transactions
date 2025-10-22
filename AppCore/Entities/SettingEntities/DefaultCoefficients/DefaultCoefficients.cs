using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.SettingEntities.DefaultCoefficients
{
    /// <summary>
    /// ضرایب پیش فرض
    /// </summary>
    public class DefaultCoefficients
    {
        #region  properties
        public Guid Id { get; set; }
        /// <summary>
        /// عنوان ضریب
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        ///  ضریب پیش فرض
        /// </summary>
        public decimal? DefaultCoefficient { get; set; }
        /// <summary>
        /// ردیف پیش فرض
        /// </summary>
        public string? DefaultRows { get; set; }
        public int? Order {  get; set; }
        #endregion
        #region relation
        #endregion

    }
}
