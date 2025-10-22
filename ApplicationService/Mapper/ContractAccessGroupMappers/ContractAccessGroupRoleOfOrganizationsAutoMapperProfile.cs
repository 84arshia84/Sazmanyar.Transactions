using AppCore.Entities.ContractAccessGroups;
using ApplicationService.DtoModels.ContractAccessGroupsDtos;
using ApplicationService.ServicesContract.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Mapper.ContractAccessGroupMappers
{
    internal class ContractAccessGroupRoleOfOrganizationsAutoMapperProfile
    {
        public static async Task<List<ContractAccessGroupRoleOfOrganizations>> DtosToEntities(List<Guid> dtos, Guid ContractAccessGroupId, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entities = new List<ContractAccessGroupRoleOfOrganizations>();
                foreach (var item in dtos)
                {
                    var entity = new ContractAccessGroupRoleOfOrganizations();
                    entity.Id = Guid.NewGuid();
                    entity.RoleOfOrganizationId = item;
                    entity.ContractAccessGroupId = ContractAccessGroupId;
                    entities.Add(entity);
                }
                return entities;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new List<ContractAccessGroupRoleOfOrganizations>();
            }
        }
        public static async Task<List<Guid>> EntitiesToDtos(List<ContractAccessGroupRoleOfOrganizations> entities, IErrorLoggerService errorLoggerService)
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
