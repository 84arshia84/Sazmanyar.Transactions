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
    internal class ContractAccessGroupUsersAutoMapperProfile
    {
        public static async Task<List<ContractAccessGroupUsers>> DtosToEntities(List<Guid> dtos, Guid ContractAccessGroupId, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entities = new List<ContractAccessGroupUsers>();
                foreach (var item in dtos)
                {
                    var entity = new ContractAccessGroupUsers();
                    entity.Id = Guid.NewGuid();
                    entity.UserId = item;
                    entity.AccessGroupId = ContractAccessGroupId;
                    entities.Add(entity);
                }
                return entities;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new List<ContractAccessGroupUsers>();
            }
        }
        public static async Task<List<Guid>> EntitiesToDtos(List<ContractAccessGroupUsers> entities, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dtos = entities.Select(x=>x.UserId).ToList();
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
