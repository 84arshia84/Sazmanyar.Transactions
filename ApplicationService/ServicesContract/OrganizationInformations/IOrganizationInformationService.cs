using ApplicationService.DtoModels.AccountsDtos;
using ApplicationService.DtoModels.OrganizationInformationDtos;
using ApplicationService.DtoModels.SettingDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.OrganizationInformations
{
    public interface IOrganizationInformationService
    {
        public Task<(string message, bool isSuccess,Guid id)> Add(OrganizationInformationDto organizationInformation);
        public Task<OrganizationInformationDto> Get();
        public Task<(string message, bool isSuccess)> Update(OrganizationInformationDto organizationInformation);
        public Task<(string message, bool isSuccess)> AddAcount(AccountDto account);
        public Task<List<AccountDto>> GetAccounts();
        public Task<(string message, bool isSuccess)> UpdateAccount(AccountDto account);
        public Task<(string message, bool isSuccess)> DeleteAccount(Guid id);

    }
}
