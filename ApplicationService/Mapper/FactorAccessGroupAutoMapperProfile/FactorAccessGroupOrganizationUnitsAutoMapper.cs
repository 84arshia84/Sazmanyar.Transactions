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
    public static class FactorAccessGroupOrganizationUnitsAutoMapper
    {
        public static async Task<List<FactorAccessGroupOrganizationUnits>> DtosToEntities(List<Guid> dtos, Guid FactorAccessGroupId, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entities = new List<FactorAccessGroupOrganizationUnits>();
                foreach (var item in dtos)
                {
                    var entity = new FactorAccessGroupOrganizationUnits();
                    entity.Id = Guid.NewGuid();
                    entity.FactorAccessGroupId = FactorAccessGroupId;
                    entity.OrganizationUnitId = item;
                    entities.Add(entity);
                }
                return entities;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new List<FactorAccessGroupOrganizationUnits>();
            }
        }
        public static async Task<List<Guid>> EntitiesToDtos(List<FactorAccessGroupOrganizationUnits> entities, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dtos = entities.Select(x => x.OrganizationUnitId).ToList();
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
