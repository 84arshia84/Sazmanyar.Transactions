using ApplicationService.DtoModels.SettingDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.Setting
{
    public interface IRoleOfOrganizationService
    {
        public Task<(string message, bool isSuccess)> Add(RoleOfOrganizationDto roleOfOrganization);
        public Task<(string message, bool isSuccess)> Update(RoleOfOrganizationDto roleOfOrganization);
        public Task<(string message, bool isSuccess)> Delete(Guid roleOfOrganization);
        public Task<List<RoleOfOrganizationDto>> GetAll();
        public Task<List<RoleOfOrganizationDto>> GetAllWithAccessGroupEffect(Guid userIid, int part, int property, int mode); 
        public Task<List<RoleOfOrganizationDto>> GetAllWithFactorAccessGroupEffect(Guid userIid, int property, int mode);
        public Task<RoleOfOrganizationDto> Get(Guid roleOfOrganization);
    }
}
