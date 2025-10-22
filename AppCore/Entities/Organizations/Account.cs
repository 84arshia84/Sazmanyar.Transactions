using AppCore.Entities.InvoiceInformations.InvoiceBaseInformations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.Organizations
{
    [Table("Accounts", Schema = "TAM")]

    public class Account
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string AccountNumber { get; set; }
        public string Bank {  get; set; }
        public string Branch { get; set; }
        public bool IsActive { get; set; }
        public string Sheba { get; set; }
        public Guid OrganizationInformationId { get; set; }
        public OrganizationInformation Information { get; set; }
        public List<InvoiceBaseInformation>? InvoiceBaseInformation { get; set; }
    }
}
