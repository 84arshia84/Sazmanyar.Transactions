using ApplicationService.DtoModels.InvoiceDtos.InvoiceAmountDtos;
using ApplicationService.DtoModels.InvoiceDtos.NettingProcessDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.InvoiceDtos.InvoiceBaseInformationsDtos
{
    public class InvoiceBaseInformationGetDto
    {
        public Guid Key { get; set; }
        public string InvoiceTitle { get; set; }
        public DateTime? LeadingToDate { get; set; }
        public DateTime SendDate { get; set; }
        public string? Description { get; set; }
        public string InvoiceCode { get; set; }
        public int? InvoiceNumber { get; set; }
        public Guid InvoiceTypeId { get; set; }
        public string InvoiceTypeTitle { get; set; }
        public Guid ContractId { get; set; }
        public string ContractTitle { get; set; }
        public string ContractNumber { get; set; }
        public Guid CreditSourceID { get; set; }
        public string CreditSourceTitle { get; set; }
        public string Corespondent { get; set; }
        public string? RegistrationNumber { get; set; }
        public string BirthCertificateNumber { get; set; }
        public string? NationalId { get; set; }
        public string NationalCode { get; set; }
        public string Address { get; set; }
        public string? ShabaNumber { get; set; }
        public string? BankName { get; set; }
        public string? BranchCodeAndName { get; set; }
        public string? BankAcountNumber { get; set; }
        public string ConsultantName { get; set; }
        public Guid OrganizationUnitId { get; set; }
        public string OrganizationUnitTitle { get; set; }
        public Guid? AccountId { get; set; }
        public List<InvoiceAmountGetDto> LastInvoiceAmount { get; set; }
        /// <summary>
        /// طرف معامله حقیقی هست یا حقوقی
        /// </summary>
        public bool IsLegal { get; set; }
        /// <summary>
        /// وضعیت فعلی
        /// </summary>
        public string CurrentStateTitle { get; set; }
        /// <summary>
        /// آخرین اقدام صورت گرفته
        /// </summary>
        public string LastActionTitle { get; set; }
    }
}
