using AppCore.Entities.ContractsInformation.Contratcs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.ContractsInformation.ContractFinancialDetailes
{
    /// <summary>
    /// مشخصات مالی قرارداد
    /// </summary>
    [Table("ContractFinancialDetails", Schema = "TAM")]
    public class ContractFinancialDetails
    {
        #region properties 
        public Guid ID { get; set; }
        /// <summary>
        /// درصد بیمه
        /// </summary>
        public decimal ContractInsurance_Percent { get; set; }
        /// <summary>
        /// درصد ارزش افزوده
        /// </summary>
        public decimal ContractValue_Added_Percent { get; set; }
        /// <summary>
        /// درصد مالیات
        /// </summary>
        public decimal ContractTax_Percent { get; set; }
        /// <summary>
        /// درصد مجاز تغییرات
        /// </summary>
        public decimal? PercentageOfChanges { get; set; }
        /// <summary>
        /// درصد حسن انجام کار
        /// </summary>
        public decimal GoodJob_Percent { get; set; }
        /// <summary>
        /// مقدار الحاقیه زمانی
        /// </summary>
        public string? Amount_of_timeExtension { get; set; }
        /// <summary>
        /// مبنای دریافت
        /// </summary>
        public string? Basis_of_Receipt { get; set; }
        #endregion

        #region relation

        public Guid ContractID { get; set; }
        public Contract Contract { get; set; }
        #endregion
    }
}
