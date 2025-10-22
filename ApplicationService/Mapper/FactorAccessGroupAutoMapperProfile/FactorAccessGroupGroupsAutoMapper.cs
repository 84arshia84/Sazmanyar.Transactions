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
    public static class FactorAccessGroupGroupsAutoMapper
    {
        public static async Task<List<FactorAccessGroupGroups>> DtosToEntities(this List<Guid> dtos, Guid FactorAccessGroupId, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entities = new List<FactorAccessGroupGroups>();
                foreach (var item in dtos)
                {
                    var entity = new FactorAccessGroupGroups();
                    entity.Id = Guid.NewGuid();
                    entity.ParentGroupId = FactorAccessGroupId;
                    entity.GroupId = item;
                    entities.Add(entity);
                }
                return entities;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new List<FactorAccessGroupGroups>();
            }
        }
        public static async Task<List<Guid>> EntitiesToDtos(List<FactorAccessGroupGroups> entities, IErrorLoggerService errorLoggerService)
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
