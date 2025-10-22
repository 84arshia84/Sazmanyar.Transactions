using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.FactorAccessGroupDto
{
    public class FactorAccessGroupPropertiesDto
    {
        public Guid Id { get; set; }
        public bool FactorTypeSave { get; set; }
        public bool FactorTypeView { get; set; }
        public bool FactorTypeEdit { get; set; }
        public bool FactorTypeDelete { get; set; }
        public bool RoleOfOrganizationSave { get; set; }
        public bool RoleOfOrganizationView { get; set; }
        public bool RoleOfOrganizationEdit { get; set; }
        public bool RoleOfOrganizationDelete { get; set; }
        public bool OrganizationUnitSave { get; set; }
        public bool OrganizationUnitView { get; set; }
        public bool OrganizationUnitEdit { get; set; }
        public bool OrganizationUnitDelete { get; set; }
        public Guid FactorAccessGroupId { get; set; }
    }
}
