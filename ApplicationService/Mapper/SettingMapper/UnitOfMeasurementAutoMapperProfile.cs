using ApplicationService.DtoModels.SettingDtos;
using AppCore.Entities.SettingEntities.UnitOfMeasurements;
using ApplicationService.ServicesContract.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Entities.Errors;

namespace ApplicationService.Mapper.SettingMapper
{
    internal static class UnitOfMeasurementAutoMapperProfile
    {
        public static UnitOfMeasurement DtoToEntity(UnitOfMeasurementDto dto, IErrorLoggerService errorLogger)
        {
            try
            {
                var entity = new UnitOfMeasurement();
                entity.Title = dto.title;
                entity.ID = dto.key;
                entity.Order = (int)dto.row;
                entity.IsDeleted = dto.IsDeleted == null ? false : (bool)dto.IsDeleted;
                return entity;
            }
            catch (Exception ex)
            {
                errorLogger.SaveError(ex);
                return new UnitOfMeasurement();
            }

        }
        public static UnitOfMeasurementDto EntityToDto(UnitOfMeasurement entity, IErrorLoggerService errorLogger,int? row = null)
        {
            try
            {
                var dto = new UnitOfMeasurementDto();
                dto.title = entity.Title;
                dto.key = entity.ID;
                dto.row = (int)row != null ? (int)row :1 ;
                return dto;
            }
            catch (Exception ex)
            {
                errorLogger.SaveError(ex);
                return new UnitOfMeasurementDto();
            }

        }
        public static List<UnitOfMeasurement> DtosToEntities(List<UnitOfMeasurementDto> dtos, IErrorLoggerService errorLogger)
        {
            var entities = new List<UnitOfMeasurement>();
            for (int i = 0; i < dtos.Count; i++)
            {
                var entity = new UnitOfMeasurement();
                entity = DtoToEntity(dtos[i], errorLogger);
                entities.Add(entity);
            }
            return entities;
        }
        public static List<UnitOfMeasurementDto> EntitiesToDtos(List<UnitOfMeasurement> entities, IErrorLoggerService errorLogger)
        {
            var dtos = new List<UnitOfMeasurementDto>();
            for (int i = 0; i < entities.Count; i++)
            {
                var dto = new UnitOfMeasurementDto();
                dto = EntityToDto(entities[i], errorLogger,i + 1);
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}
