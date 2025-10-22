using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.SettingDtos
{
    public class CorespondentLegalDto
    {
        public Guid key { get; set; }
        public Int64? row { get; set; }
        public string CompanyName { get; set; }
        public string? RegistrationNumber { get; set; }
        public string NationalID { get; set; }
        public string? Address { get; set; }
        public string? BankAcountNumber { get; set; }
        public string? BankName { get; set; }
        public string? BranchCodeAndName { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsReal { get; set; } = true;
        public string? ShabaNumber { get; set; }
        public List<Guid>? TypeOfCooperations { get; set; }
        public int? Province {  get; set; }
        public int? City { get; set; }
        public int? County { get; set; }
        public string? EconomicCode {  get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set;}
        public string? PostalCode { get; set; }
        
    }
}
