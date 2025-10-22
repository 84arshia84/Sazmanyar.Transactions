using ApplicationService.DtoModels.FactorDtos.FactorFinancialDetaile;
using ApplicationService.DtoModels.FactorDtos.FactorServiceExplanation;
using ApplicationService.DtoModels.FactorDtos.FactorTimeProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.FactorDtos.Factor
{
    public class FactorGetDto
    {
        public Guid Id { get; set; }
        public string FactorTitle { get; set; }
        public string FactorNumber { get; set; }
        public Guid? FactorExpertID { get; set; }
        public bool RequirementToCloseTheAccount { get; set; }
        public Guid FactorTypeId { get; set; }
        public Guid? OrganizationalunitId { get; set; }
        public Guid RoleOfOrganizationId { get; set; }
        public Guid? CreditSourceID { get; set; }
        public Guid CorespondentID { get; set; }
        public bool CorespondentRealOrLegal { get; set; }
        public bool IsFinalApproved { get; set; }
        public FactorTimeProfileGetDto FactorTimeProfile { get; set; }
        public FactorFinancialDetaileGetDto FactorFinancialDetaile { get; set; }
    }
}
