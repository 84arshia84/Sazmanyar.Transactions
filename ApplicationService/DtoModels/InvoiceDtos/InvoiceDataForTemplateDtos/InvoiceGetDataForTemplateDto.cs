using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.InvoiceDtos.InvoiceDataForTemplateDtos
{
    public class InvoiceGetDataForTemplateDto
    {
        /// <summary>
        /// عنوان_صورت وضعیت
        /// </summary>
        public string InvoiceTitle { get; set; }
        /// <summary>
        /// شماره_سریال
        /// </summary>
        public string InvoiceCode { get; set; }
        /// <summary>
        /// شناسه سند
        /// </summary>
        public Guid InvoiceTypeDocumentId { get; set; }
        /// <summary>
        /// شماره_قرارداد
        /// </summary>
        public string ContractNumber { get; set; }
        /// <summary>
        /// تاریخ_صورت_وضعیت
        /// </summary>
        public string InvoiceInsertDate { get; set; }
        /// <summary>
        /// عنوان_معامله
        /// </summary>
        public string ContractTitle { get; set; }
        /// <summary>
        /// نام_فروشنده
        /// </summary>
        public string SellerInformationName { get; set; }
        /// <summary>
        /// شناسه_ملی_فروشنده
        /// </summary>
        public string SellerNationalId { get; set; }
        /// <summary>
        /// شماره_ثبت_فروشنده
        /// </summary>
        public string SellerRegistrationId { get; set; }
        /// <summary>
        /// استان_فروشنده
        /// </summary>
        public string SellerProvince { get; set; }
        /// <summary>
        /// شهرستان_فروشنده
        /// </summary>
        public string SellerCounty { get; set; }
        /// <summary>
        /// شهر_فروشنده
        /// </summary>
        public string SellerCity { get; set; }
        /// <summary>
        /// کد_پستی_فروشنده
        /// </summary>
        public string SellerZipCode { get; set; }
        /// <summary>
        /// نشانی_فروشنده
        /// </summary>
        public string SellerAddress { get; set; }
        /// <summary>
        /// شماره_تلفن_فروشنده
        /// </summary>
        public string SellerPhoneNumber { get; set; }
        /// <summary>
        /// نام_خریدار
        /// </summary>
        public string BuyerInformationName { get; set; }
        /// <summary>
        /// شناسه_ملی_خریدار
        /// </summary>
        public string BuyerNationalId { get; set; }
        /// <summary>
        /// شماره_ثبت_خریدار
        /// </summary>
        public string BuyerRegistrationId { get; set; }
        /// <summary>
        /// استان_خریدار
        /// </summary>
        public string BuyerProvince { get; set; }
        /// <summary>
        /// شهرستان_خریدار
        /// </summary>
        public string BuyerCounty { get; set; }
        /// <summary>
        /// شهر_خریدار
        /// </summary>
        public string BuyerCity { get; set; }
        /// <summary>
        /// کد_پستی_خریدار
        /// </summary>
        public string BuyerZipCode { get; set; }
        /// <summary>
        /// نشانی_خریدار
        /// </summary>
        public string BuyerAddress { get; set; }
        /// <summary>
        /// کد_اقتصادی_خریدار
        /// </summary>
        public string BuyerEconomicCode { get; set; }
        /// <summary>
        /// شماره_تلفن_خریدار
        /// </summary>
        public string BuyerPhoneNumber { get; set; }
        /// <summary>
        /// شرح_خدمات_صورت_وضعیت
        /// </summary>
        public List<InvoiceServiceExplanationTemplateDto> InvoiceServiceExplanations { get; set; }
        /// <summary>
        /// جمع_کل_شرح_خدمات
        /// </summary>
        public InvoiceServiceExplanationTemplateTotalAmountsDto TotalAmounts { get; set; }
        /// <summary>
        /// شماره_حساب
        /// </summary>
        public string AcountNumber { get; set; }
        /// <summary>
        /// نام_بانک
        /// </summary>
        public string BankName { get; set; }
        /// <summary>
        /// شعبه_بانک
        /// </summary>
        public string BranchCodeAndName { get; set; }
        /// <summary>
        /// شماره_شبا
        /// </summary>
        public string ShabaNumber { get; set; }
    }
}
