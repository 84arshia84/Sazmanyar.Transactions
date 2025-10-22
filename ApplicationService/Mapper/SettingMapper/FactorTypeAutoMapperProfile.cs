using AppCore.Entities.SettingEntities.FactorTypes;
using ApplicationService.DtoModels.SettingDtos;
using ApplicationService.ServicesContract.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Mapper.SettingMapper
{
    internal class FactorTypeAutoMapperProfile
    {
        public static FactorType DtoToEntity(FactorTypeDto dto, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entity = new FactorType();
                entity.Title = dto.Title;
                entity.ID = dto.Id;
                entity.Order = (int)dto.row;
                entity.OfficeOnlineDocumentId = dto.OfficeOnlineDocumentId;
                return entity;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new FactorType();
            }

        }
        public static FactorTypeDto EntityToDto(FactorType entity, IErrorLoggerService errorLoggerService, int? row = null)
        {
            try
            {
                var dto = new FactorTypeDto();
                dto.Title = entity.Title;
                dto.Id = entity.ID;
                dto.row = row;
                dto.OfficeOnlineDocumentId = entity.OfficeOnlineDocumentId;
                return dto;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new FactorTypeDto();
            }

        }
        public static List<FactorType> DtosToEntities(List<FactorTypeDto> dtos, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entities = new List<FactorType>();
                for (int i = 0; i < dtos.Count; i++)
                {
                    var entity = new FactorType();
                    entity = DtoToEntity(dtos[i], errorLoggerService);
                    entities.Add(entity);
                }
                return entities;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new List<FactorType>();
            }
        }
        public static List<FactorTypeDto> EntitiesToDtos(List<FactorType> entities, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dtos = new List<FactorTypeDto>();
                for (int i = 0; i < entities.Count; i++)
                {
                    var dto = new FactorTypeDto();
                    dto = EntityToDto(entities[i], errorLoggerService, i + 1);
                    dtos.Add(dto);
                }
                return dtos;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new List<FactorTypeDto>();
            }

        }
    }
}
