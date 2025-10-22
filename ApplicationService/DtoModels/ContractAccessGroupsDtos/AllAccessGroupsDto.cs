using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.ContractAccessGroupsDtos
{
    public class AllAccessGroupsDto
    {
        public ContractAccessGroupDto ContractAccessGroup { get; set; }
        public List<Guid>? ContractAccessGroupContractType { get; set; }
        public List<Guid>? ContractAccessGroupGroups { get; set; }
        public List<Guid>? ContractAccessGroupsOrganizationUnits { get; set; }
        public List<Guid>? ContractAccessGroupRoleOfOrganizations { get; set; }
        public List<Guid>? ContractAccessGroupUsers { get; set; }
        public List<ContractAccessGroupSystemPartDto> ContractAccessGroupSystemParts { get; set; }
        public ContractAccessGroupPermissionsDto ContractAccessGroupPermissions { get; set; }
        public ContractAccessGroupPropertiesDto ContractAccessGroupProperties { get; set; }
    }
}
