using AppCore.Entities.SettingEntities.ContractTypes;
using ApplicationService.DtoModels.SettingDtos;
using ApplicationService.ServicesContract.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Mapper.SettingMapper
{
    internal static class ContractTypeAutoMapperProfile
    {
        public static ContractType DtoToEntity(ContractTypeDto dto,IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entity = new ContractType();
                entity.Title = dto.Title;
                entity.ID = dto.Id;
                entity.Order = (int)dto.row;
                return entity;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new ContractType();
            }
            
        }
        public static ContractTypeDto EntityToDto(ContractType entity,IErrorLoggerService errorLoggerService, int? row = null)
        {
            try
            {
                var dto = new ContractTypeDto();
                dto.Title = entity.Title;
                dto.Id = entity.ID;
                dto.OfficeOnlineDocumentId = entity.OfficeOnlineDocumentId;
                dto.row = row;
                return dto;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new ContractTypeDto();
            }
            
        }
        public static List<ContractType> DtosToEntities(List<ContractTypeDto> dtos, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entities = new List<ContractType>();
                for (int i = 0; i < dtos.Count; i++)
                {
                    var entity = new ContractType();
                    entity = DtoToEntity(dtos[i], errorLoggerService);
                    entities.Add(entity);
                }
                return entities;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new List<ContractType>();
            }
        }
        public static List<ContractTypeDto> EntitiesToDtos(List<ContractType> entities, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dtos = new List<ContractTypeDto>();
                for (int i = 0; i < entities.Count; i++)
                {
                    var dto = new ContractTypeDto();
                    dto = EntityToDto(entities[i], errorLoggerService, i + 1);
                    dtos.Add(dto);
                }
                return dtos;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new List<ContractTypeDto>();
            }
          
        }
    }
}
