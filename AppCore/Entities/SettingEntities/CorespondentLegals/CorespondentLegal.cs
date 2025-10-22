using AppCore.Entities.SettingEntities.Locations;
using AppCore.Entities.SettingEntities.CorespondAndTypeOfCoopRel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.SettingEntities.CorespondentLegals
{
    /// <summary>
    /// طرف معامله حقوقی
    /// </summary>
    [Table("CorespondentLegal", Schema = "TAM")]
    public class CorespondentLegal 
    {

        #region properties
        public Guid ID { get; set; }
        /// <summary>
        /// آدرس
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// شماره حساب
        /// </summary>
        public string? BankAcountNumber { get; set; }
        /// <summary>
        /// بانک
        /// </summary>
        public string? BankName { get; set; }
        /// <summary>
        /// کد و نام شعبه
        /// </summary>
        public string? BranchCodeAndName { get; set; }
        /// <summary>
        /// حذف
        /// </summary>
        public bool IsDeleted { get; set; }
        /// <summary>
        /// آیا حقیقی است
        /// </summary>
        public bool IsReal { get; set; }
        /// <summary>
        /// شماره شبا
        /// </summary>
        public string? ShabaNumber { get; set; }
        /// <summary>
        /// نام شرکت
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// شماره ثبت
        /// </summary>
        public string? RegistrationNumber { get; set; }
        /// <summary>
        /// شناسه ملی
        /// </summary>
        public string? NationalID { get; set; }
        /// <summary>
        /// شماره تماس
        /// </summary>
        public string? PhoneNumber { get; set; }
        /// <summary>
        /// آدرس ایمیل
        /// </summary>
        public string? Email {  get; set; }
        /// <summary>
        /// کد پستی
        /// </summary>
        public string? PostalCode { get; set; }
        /// <summary>
        /// کد اقتصادی
        /// </summary>
        public string? EconomicCode { get; set; }
        #endregion


        #region relations
        /// <summary>
        /// نوع همکاری
        /// </summary>
        public List<CoresponedAndTypeCoopRels>? CorespondAndTypeOfCoopRels { get; set; }
        public int? ProvincId { get; set; }
        public Province? Province { get; set; }
        public int? CountyId { get; set; }
        public County? County { get; set; }
        public int? CityId { get; set; }
        public City? City { get; set; }
        #endregion
    }
}
