using AppCore.Entities.Organizations;
using ApplicationService.DtoModels.AccountsDtos;
using System;
using System.Collections.Generic;

namespace ApplicationService.DtoModels.OrganizationInformationDtos
{
    public class OrganizationInformationDto
    {
        public Guid Key { get; set; }
        public string CompanyName { get; set; }
        public string? RegistrationNumber { get; set; }
        public string NationalID { get; set; }
        public string? Address { get; set; }
        public int? Province { get; set; }
        public int? City { get; set; }
        public int? County { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? PostalCode { get; set; }

        public List<AccountDto>? AccountDtos { get; set; }
    }
}
