using AppCore.Entities.SettingEntities.AddendumTypes;
using AppCore.Enums;
using ApplicationService.DtoModels.SettingDtos;
using ApplicationService.ServicesContract.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Mapper.SettingMapper
{
    internal static class AddendumTypeAutoMapperProfile
    {
        public static AddendumType DtoToEntity(AddendumTypeDto dto, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entity = new AddendumType();
                entity.ID = dto.Id;
                entity.Title = dto.Title;
                entity.AddendumChangeType = (AddendumChangeTypeEnum)dto.AddendumChangeType;
                entity.Order = dto.Row;
                return entity;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new AddendumType();
            }
        }
        public static AddendumTypeDto EntityToDto(AddendumType entity, IErrorLoggerService errorLoggerService, int? row=null)
        {
            try
            {
                var dto = new AddendumTypeDto();
                dto.Id = entity.ID;
                dto.Title = entity.Title;
                dto.AddendumChangeType = (int)entity.AddendumChangeType;
                dto.Row = (int)row != null ? (int)row : 1;
                return dto;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new AddendumTypeDto();
            }
        }
        public static List<AddendumType> DtosToEntites(List<AddendumTypeDto> dtos, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entities = new List<AddendumType>();
                foreach (var item in dtos)
                {
                    var entity = new AddendumType();
                    entity = DtoToEntity(item, errorLoggerService);
                    entities.Add(entity);
                }
                return entities;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new List<AddendumType>();
            }
        }
        public static List<AddendumTypeDto> EntitiesToDtos(List<AddendumType> entities, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dtos = new List<AddendumTypeDto>();
                int count = 1;
                foreach (var item in entities)
                {
                    var dto = new AddendumTypeDto();
                    dto = EntityToDto(item, errorLoggerService, count);
                    dtos.Add(dto);
                    count++;
                }
                return dtos;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new List<AddendumTypeDto>();
            }
        }
    }
}
