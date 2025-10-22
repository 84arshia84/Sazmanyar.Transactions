using AppCore.Entities.ContractsInformation.Contratcs;
using AppCore.Entities.ContractsInformation.ContratTimeProfiles;
using AppCore.Entities.SettingEntities.BasisForStartingTheProjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.ContractsInformation.ContratTimeProfiles
{
    /// <summary>
    /// مشخصات زمانی قرارداد
    /// </summary>
    [Table("ContractTimeProfiles", Schema = "TAM")]
    public class ContractTimeProfile
    {
        #region properties
        [Key]
        public Guid ID { get; set; }
        /// <summary>
        /// تاریخ ابلاغ قرارداد
        /// </summary>
        public DateTime? ContractDateOfNotification { get; set; }
        /// <summary>
        /// تاریخ مبادله قرارداد
        /// </summary>
        public DateTime? ContractExchangeDate { get; set; }
        /// <summary>
        /// تاریخ شروع قرارداد
        /// </summary>
        public DateTime? ContractStartDate { get; set; }
        /// <summary>
        /// تاریخ پایان قرارداد
        /// </summary>
        public DateTime? ContractEndDate { get; set; }
        /// <summary>
        /// مدت معامله
        /// </summary>
        public string? ContractPeriod { get; set; }
        #endregion

        #region relation
        /// <summary>
        /// مبنای شروع پروژه
        /// </summary>
        public Guid? BasisForStartingProjectID { get; set; }
        public BasisForStartingTheProject? BasisForStartingTheProject { get; set; }
        public Guid ContractID { get; set; }
        public Contract Contract { get; set; }
        #endregion
    }
}
