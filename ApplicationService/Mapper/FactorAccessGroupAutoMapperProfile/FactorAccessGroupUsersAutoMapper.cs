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
    public static class FactorAccessGroupUsersAutoMapper
    {
        public static async Task<List<FactorAccessGroupUsers>> DtosToEntities(List<Guid> dtos, Guid FactorAccessGroupId, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entities = new List<FactorAccessGroupUsers>();
                foreach (var item in dtos)
                {
                    var entity = new FactorAccessGroupUsers();
                    entity.Id = Guid.NewGuid();
                    entity.UserId = item;
                    entity.AccessGroupId = FactorAccessGroupId;
                    entities.Add(entity);
                }
                return entities;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new List<FactorAccessGroupUsers>();
            }
        }

        public static async Task<List<Guid>> EntitiesToDtos(List<FactorAccessGroupUsers> entities, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dtos = entities.Select(x => x.UserId).ToList();
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
