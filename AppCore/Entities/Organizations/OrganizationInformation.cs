using AppCore.Entities.SettingEntities.CorespondAndTypeOfCoopRel;
using AppCore.Entities.SettingEntities.Locations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.Organizations
{
    [Table("OrganizationInformation", Schema = "TAM")]

    public class OrganizationInformation
    {
        #region properties
        public Guid ID { get; set; }
        /// <summary>
        /// آدرس
        /// </summary>
        public string Address { get; set; }
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
        public string? Email { get; set; }
        /// <summary>
        /// کد پستی
        /// </summary>
        public string? PostalCode { get; set; }
        public ICollection<Account> Accounts { get; set; }  
        /// <summary>
        /// کد اقتصادی
        /// </summary>
        #endregion


        #region relations
        /// <summary>
        /// نوع همکاری
        /// </summary>
        public int? ProvincId { get; set; }
        public Province? Province { get; set; }
        public int? CountyId { get; set; }
        public County? County { get; set; }
        public int? CityId { get; set; }
        public City? City { get; set; }
        #endregion
    }

}

