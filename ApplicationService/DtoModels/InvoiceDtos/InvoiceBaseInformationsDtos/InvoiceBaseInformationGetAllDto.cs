using ApplicationService.DtoModels.InvoiceDtos.InvoiceAmountDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.InvoiceDtos.InvoiceBaseInformationsDtos
{
    public class InvoiceBaseInformationGetAllDto
    {
        public Guid Key { get; set; }
        public long Row { get; set; }
        public string InvoiceTitle { get; set; }
        public string InvoiceCode { get; set; }
        public string ContractTitle { get; set; }
        public DateTime SendDate { get; set; }
        public string InvoicePrice { get; set; }
        public int? InvoiceNumber { get; set; }
        public DateTime? LeadingToDate { get; set; }
        public string LastContractAmount { get; set; }
        public string Corespondent { get; set; }
        public string CurrentSituation { get; set; }
        public string LastAction { get; set; }
        public Guid invoiceType { get; set; }
        /// <summary>
        /// وضعیت فعلی
        /// </summary>
        public string CurrentStateTitle { get; set; }
        /// <summary>
        /// آخرین اقدام صورت گرفته
        /// </summary>
        public string LastActionTitle { get; set; }
        public bool? IsfinalApproved { get; set; }
        public List<InvoiceAmountGetDto> LastInvoiceAmount { get; set; }
        
        /// اطلاهات قرارداد
        public string? contractNumber { get; set; }
        public Guid? contractExpertKey { get; set; }
        public Guid? consultantID { get; set; }
        public Guid contractTypeID { get; set; }
        public Guid transActionTypeID { get; set; }
        public Guid roleOFOrganizationID { get; set; }
        public Guid corespondentID { get; set; }
        public Guid organizationUnitID { get; set; }
        public Guid? creditSourceID { get; set; }
        public DateTime? ContractDateOfNotification { get; set; }
        public DateTime? ContractExchangeDate { get; set; }
        public DateTime? ContractStartDate { get; set; }
        public DateTime? ContractEndDate { get; set; }
        public Guid? BasisForStartingProjectID { get; set; }
    }
}
