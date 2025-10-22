using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Entities.FactorInformation.Factors;
using AppCore.Entities.SettingEntities.FactorTypes;
using AppCore.Entities.SettingEntities.Organizationalunits;
using AppCore.Entities.SettingEntities.RoleOfOrganizations;
using AppCore.Enums;

namespace ApplicationService.ServicesContract.FactorAccessGroups
{
    public interface IFactorAccessGroupFilterService
    {
        public Task<List<Factor>> FilterFactor(List<Factor> factors, List<Guid> FlowIds, Guid userId);
        public Task<List<FactorType>> FilterFactorType(List<FactorType> factors, Guid userId, EnumFactorAccessGroupProperties property, int mode);
        public Task<List<Organizationalunit>> FilterOrganizationalunit(List<Organizationalunit> organizationalunits, Guid userId, EnumFactorAccessGroupProperties property, int mode);
        public Task<List<RoleOfOrganization>> FilterRoleOfOrganization(List<RoleOfOrganization> roleOfOrganizations, Guid userId, EnumFactorAccessGroupProperties property, int mode);
        public Task<(List<Guid> accessGroupForFactorType, List<Guid> accessGroupForOrganizationalunit, List<Guid> accessGroupForRoleOfOrganization)> GetAccessGroupIds(Guid userId, EnumFactorAccessGroupProperties property, int mode);


    }
}
