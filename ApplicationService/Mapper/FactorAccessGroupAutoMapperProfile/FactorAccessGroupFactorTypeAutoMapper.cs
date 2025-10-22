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
    public static class FactorAccessGroupFactorTypeAutoMapper
    {
        public static async Task<List<FactorAccessGroupFactorType>> DtosToEntities(List<Guid> dtos, Guid FactorAccessGroupId, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entities = new List<FactorAccessGroupFactorType>();
                foreach (var item in dtos)
                {
                    var entity = new FactorAccessGroupFactorType();
                    entity.Id = Guid.NewGuid();
                    entity.FactorAccessGroupId = FactorAccessGroupId;
                    entity.FactorTypeId = item;
                    entities.Add(entity);
                }
                return entities;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new List<FactorAccessGroupFactorType>();
            }
        }

        public static async Task<List<Guid>> EntitiesToDtos(List<FactorAccessGroupFactorType> entities, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dtos = entities.Select(x => x.FactorTypeId).ToList();
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
