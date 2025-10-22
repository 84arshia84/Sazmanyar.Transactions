using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationService.DtoModels.ContractAccessGroupsDtos;

namespace ApplicationService.DtoModels.FactorAccessGroupDto
{
    public class AllFactorAccessGroupsDto
    {
        public FactorAccessGroupDto FactorAccessGroupDto { get; set; }
        public List<Guid>? FactorAccessGroupFactorType { get; set; }
        public List<Guid>? FactorAccessGroupGroups { get; set; }
        public List<Guid>? FactorAccessGroupsOrganizationUnits { get; set; }
        public List<Guid>? FactorAccessGroupRoleOfOrganizations { get; set; }
        public List<Guid>? FactorAccessGroupUsers { get; set; }
       
        public FactorAccessGroupPermissionsDto FactorAccessGroupPermissions { get; set; }
        public FactorAccessGroupPropertiesDto FactorAccessGroupProperties  { get; set; }
    }
}
