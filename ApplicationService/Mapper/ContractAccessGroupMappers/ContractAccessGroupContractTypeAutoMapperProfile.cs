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
    internal class ContractAccessGroupContractTypeAutoMapperProfile
    {
        public static async Task<List<ContractAccessGroupContractType>> DtosToEntities(List<Guid> dtos,Guid ContractAccessGroupId, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entities = new List<ContractAccessGroupContractType>();
                foreach (var item in dtos)
                {
                    var entity = new ContractAccessGroupContractType();
                    entity.Id = Guid.NewGuid();
                    entity.ContractAccessGroupId= ContractAccessGroupId;
                    entity.ContractTypeId = item;
                    entities.Add(entity);
                }
                return entities;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new List<ContractAccessGroupContractType>();
            }
        }
        public static async Task<List<Guid>> EntitiesToDtos(List<ContractAccessGroupContractType> entities, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dtos = entities.Select(x => x.ContractTypeId).ToList();
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
