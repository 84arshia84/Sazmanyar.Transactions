using ApplicationService.DtoModels.SettingDtos;
using AppCore.Entities.SettingEntities.Activitycenters;
using AppCore.Entities.SettingEntities.Currencies;
using ApplicationService.ServicesContract.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Mapper.SettingMapper
{
    internal static class CurrencyAutoMapperProfile
    {
        public static Currency DtoToEntity(CurrencyDto dto ,IErrorLoggerService errorLogger)
        {
            try
            {
                var entity = new Currency();
                entity.Title = dto.title;
                entity.ID = dto.key;
                entity.IsDeleted = dto.IsDeleted==null?false: (bool)dto.IsDeleted;
                return entity;
            }
            catch (Exception ex)
            {
                errorLogger.SaveError(ex);
                return new Currency();
            }
          
        }
        public static CurrencyDto EntityToDto(Currency entity, IErrorLoggerService errorLogger,int? row =null)
        {
            try
            {
                var dto = new CurrencyDto();
                dto.title = entity.Title;
                dto.key = entity.ID;
                dto.row = (int)row != null ? (int)row : 1;
                return dto;
            }
            catch (Exception ex)
            {
                errorLogger.SaveError(ex);
                return new CurrencyDto();
            }
        }
        public static List<Currency> DtosToEntities(List<CurrencyDto> dtos, IErrorLoggerService errorLogger)
        {
            var entities = new List<Currency>();
            for (int i = 0; i < dtos.Count; i++)
            {
                var entity = new Currency();
                entity = DtoToEntity(dtos[i], errorLogger);
                entities.Add(entity);
            }
            return entities;
        }
        public static List<CurrencyDto> EntitiesToDtos(List<Currency> entities, IErrorLoggerService errorLogger)
        {
            var dtos = new List<CurrencyDto>();
            for (int i = 0; i < entities.Count; i++)
            {
                var dto = new CurrencyDto();
                dto = EntityToDto(entities[i],  errorLogger, i + 1);
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}
