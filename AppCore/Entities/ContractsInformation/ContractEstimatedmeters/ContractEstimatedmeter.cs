using AppCore.Entities.ContractsInformation.ContractAddendums;
using AppCore.Entities.ContractsInformation.Contratcs;
using AppCore.Entities.ContractsInformation.ServiceExplanations;
using AppCore.Entities.PriceListEntities.PriceListClauses;
using AppCore.Entities.PriceListEntities.PriceListExplanations;
using AppCore.Entities.PriceListEntities.PriceListFields;
using AppCore.Entities.PriceListEntities.PriceLists;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.ContractsInformation.ContractEstimatedmeters
{
    public interface IAggregateRoot;
    /// <summary>
    /// متربرآورد
    /// </summary>
    [Table("ContractEstimatedmeters", Schema = "TAM")]
    public class ContractEstimatedmeter : IAggregateRoot
    {
        #region properties
        public Guid Id { get; set; }
        /// <summary>
        /// سال
        /// </summary>
        public string Year { get; set; }   
        /// <summary>
        /// رشته
        /// </summary>
        public string Field { get; set; }
        /// <summary>
        /// فصل
        /// </summary>
        public string Clause { get; set; }
        /// <summary>
        /// شرح
        /// </summary>
        public string Explenation { get; set; }
        /// <summary>
        /// حجم
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// شرح ضرایب
        /// </summary>
        public string? CoefficientTitle { get; set; }
        /// <summary>
        /// حاصل ضرایب
        /// </summary>
        public decimal SumOfCoefficients { get; set; }
        /// <summary>
        /// مبلغ خام
        /// </summary>
        public decimal RawPrice { get; set; }
        /// <summary>
        /// مبلغ کل با اعمال ضریب
        /// </summary>
        public decimal RowPrice { get; set; }
        /// <summary>
        /// توضیحات
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// متربرآورد در الحاقیه حذف شده
        /// </summary>
        public bool? IsDeleted { get; set; }
        /// <summary>
        /// زمان حذف متر برآورد
        /// </summary>
        public DateTime? DeleteDate { get; set; }
        /// <summary>
        /// برای زمانی هست که متربرآورد در قرارداد ثبت شده باشد
        /// و بعد در الحاقیه ویرایش شود
        /// توی قرارداد باید متربرآورد ویرایش نشده نمایش داده شود
        /// ولی توی الحاقیه باید ویرایش شده نمایش داده شود.
        /// </summary>
        public bool? UpdatedInAddendum { get; set; }
        /// <summary>
        /// این شرح خدمت در کدام الحاقیه ویرایش شده
        /// </summary>
        public Guid? UpdateInAddendumId { get; set; }
        /// <summary>
        /// این متربرآورد در کدام الحاقیه حذف شده
        /// </summary>
        public Guid? DeletedInAddedumId { get; set; }
        /// <summary>
        /// متربرآوردی که در الحاقیه ثبت شده.
        /// </summary>
        public bool? IsAddendum {  get; set; }
        public int? Order {  get; set; }
        #endregion
        #region relation
        public Guid ServiceExplanationId { get; set; }
        public ServiceExplanation ServiceExplanation { get; set; }
        public Guid YearId { get; set; }
        public PriceList PriceList{ get; set; }
        public Guid FieldId { get; set; }
        public PriceListField PriceListField{ get; set; }
        public Guid ClauseId { get; set; }
        public PriceListClause PriceListClause { get; set; }
        public Guid ExplenationId { get; set; }
        public PriceListExplanation PriceListExplanation { get; set; }
        public Guid? AddendumId { get; set; }
        public ContractAddendum? ContractAddendum { get; set; }
        #endregion
    }
}
