using AppCore.Entities.ContractAccessGroups;
using AppCore.Enums;
using ApplicationService.DtoModels.ContractAccessGroupsDtos;
using ApplicationService.ServicesContract.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Mapper.ContractAccessGroupMappers
{
    internal class ContractAccessGroupSystemPartAutoMapperProfile
    {
        public static async Task<List<ContractAccessGroupSystemParts>> DtosToEntities(List<ContractAccessGroupSystemPartDto> dtos, Guid ContractAccessGroupId, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entities = new List<ContractAccessGroupSystemParts>();
                foreach (var item in dtos)
                {
                    var entity = new ContractAccessGroupSystemParts();
                    entity.Id = Guid.NewGuid();
                    entity.SystemParts = (SystemParts)item.SystemKey;
                    entity.AccessGroupProperties = (AccessGroupProperties)item.PropertyKey;
                    entity.ContractAccessGroupId = ContractAccessGroupId;
                    entities.Add(entity);
                }
                return entities;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new List<ContractAccessGroupSystemParts>();
            }
        }
        public static async Task<List<ContractAccessGroupSystemPartDto>> EntitiesToDtos(List<ContractAccessGroupSystemParts> entities, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dtos = new List<ContractAccessGroupSystemPartDto>();
                foreach (var item in entities)
                {
                    var dto = new ContractAccessGroupSystemPartDto();
                    dto.SystemKey = (int)item.SystemParts;
                    dto.PropertyKey = (int)item.AccessGroupProperties;
                    dtos.Add(dto);
                }
                return dtos;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new List<ContractAccessGroupSystemPartDto>();
            }
        }
    }
}
