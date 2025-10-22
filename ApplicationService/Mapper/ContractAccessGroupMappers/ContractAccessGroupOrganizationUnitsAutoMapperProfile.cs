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
    internal class ContractAccessGroupOrganizationUnitsAutoMapperProfile
    {
        public static async Task<List<ContractAccessGroupOrganizationUnits>> DtosToEntities(List<Guid> dtos, Guid ContractAccessGroupId, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entities = new List<ContractAccessGroupOrganizationUnits>();
                foreach (var item in dtos)
                {
                    var entity = new ContractAccessGroupOrganizationUnits();
                    entity.Id = Guid.NewGuid();
                    entity.OrganizationUnitId = item;
                    entity.ContractAccessGroupId = ContractAccessGroupId;
                    entities.Add(entity);
                }
                return entities;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new List<ContractAccessGroupOrganizationUnits>();
            }
        }
        public static async Task<List<Guid>> EntitiesToDtos(List<ContractAccessGroupOrganizationUnits> entities, IErrorLoggerService errorLoggerService)
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
