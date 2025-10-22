using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.InvoiceDtos.InvoiceBaseInformationsDtos
{
    public class GetContractCorespondentInformationsDto
    {
        /// <summary>
        /// طرف معامله
        /// </summary>
        public string Corespondent { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string BirthCertificateNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string RegistrationNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string NationalCode { get; set; }
        public string NationalId { get; set; }
        public string Address { get; set; }
        /// <summary>
        /// شماره حساب
        /// </summary>
        public string BankAcountNumber { get; set; }
        /// <summary>
        /// شماره شبا
        /// </summary>
        public string ShabaNumber { get; set; }
        /// <summary>
        /// نام بانک
        /// </summary>
        public string BankName { get; set; }
        /// <summary>
        /// کد و نام شعبه
        /// </summary>
        public string BranchCodeAndName { get; set; }
        public bool IsLegal { get; set; }


    }
}
