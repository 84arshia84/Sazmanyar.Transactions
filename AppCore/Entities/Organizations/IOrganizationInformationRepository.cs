using AppCore.Entities.SettingEntities.CorespondentLegals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.Organizations
{
    public interface IOrganizationInformationRepository
    {
        public Task<(string message, bool isSuccess)> Add(OrganizationInformation organizationInformation);
        public Task<OrganizationInformation> Get();
        public Task<(string message, bool isSuccess)> Update(OrganizationInformation organizationInformation);
        public Task<(string message, bool isSuccess)> AddAcount(Account account);
        public Task<List<Account>> GetAccounts();
        public Task<(string message, bool isSuccess)> UpdateAccount(Account account);
        public Task<(string message, bool isSuccess)> DeleteAccount(Guid id);
    }
}
