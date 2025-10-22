using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Entities.ContractAccessGroups;
using AppCore.Entities.FactorAccessGroup;
using ApplicationService.ServicesContract.ExceptionHandling;

namespace ApplicationService.Mapper.FactorAccessGroupAutoMapperProfile
{
    public static class FactorAccessGroupRoleOfOrganizationsAutoMapper
    {
        public static async Task<List<FactorAccessGroupRoleOfOrganizations>> DtosToEntities(List<Guid> dtos, Guid FactorAccessGroupId, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entities = new List<FactorAccessGroupRoleOfOrganizations>();
                foreach (var item in dtos)
                {
                    var entity = new FactorAccessGroupRoleOfOrganizations();
                    entity.Id = Guid.NewGuid();
                    entity.RoleOfOrganizationId = item;
                    entity.FactorAccessGroupId = FactorAccessGroupId;
                    entities.Add(entity);
                }
                return entities;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new List<FactorAccessGroupRoleOfOrganizations>();
            }
        }

        public static async Task<List<Guid>> EntitiesToDtos(List<FactorAccessGroupRoleOfOrganizations> entities, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dtos = entities.Select(x => x.RoleOfOrganizationId).ToList();
                return dtos;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new List<Guid>();
            }
        }
    }
}
