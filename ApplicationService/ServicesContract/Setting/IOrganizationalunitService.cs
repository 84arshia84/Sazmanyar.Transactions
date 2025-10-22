using ApplicationService.DtoModels.SettingDtos;
using AppCore.Entities.SettingEntities.Organizationalunits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.Setting
{
    public interface IOrganizationalunitService
    {
        public Task<Tuple<string, bool>> Add(OrganizationalunitDto organizationalunit);
        public Task<List<OrganizationalunitDto>> GetAll();
        public Task<List<OrganizationalunitDto>> GetAllWithAccessGroupEffect(Guid userIid, int part, int property, int mode);
        public Task<List<OrganizationalunitDto>> GetAllWithFactorAccessGroupEffect(Guid userIid, int property, int mode);
        public Task<Tuple<string, bool>> Update(OrganizationalunitDto organizationalunit);
        public Task<Tuple<string, bool>> Delete(Guid organizationalunit);
    }
}
