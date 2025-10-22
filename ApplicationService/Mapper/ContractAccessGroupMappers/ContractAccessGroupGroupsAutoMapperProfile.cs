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
    internal class ContractAccessGroupGroupsAutoMapperProfile
    {
        public static async Task<List<ContractAccessGroupGroups>> DtosToEntities(List<Guid> dtos, Guid ContractAccessGroupId, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entities = new List<ContractAccessGroupGroups>();
                foreach (var item in dtos)
                {
                    var entity = new ContractAccessGroupGroups();
                    entity.Id = Guid.NewGuid();
                    entity.ParentGroupId = ContractAccessGroupId;
                    entity.GroupId = item;
                    entities.Add(entity);
                }
                return entities;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new List<ContractAccessGroupGroups>();
            }
        }
        public static async Task<List<Guid>> EntitiesToDtos(List<ContractAccessGroupGroups> entities, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dtos = entities.Select(x => x.GroupId).ToList();
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
