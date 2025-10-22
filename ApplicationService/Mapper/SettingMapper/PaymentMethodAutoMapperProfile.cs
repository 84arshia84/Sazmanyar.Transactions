using ApplicationService.DtoModels.SettingDtos;
using AppCore.Entities.SettingEntities.PaymentMethods;
using ApplicationService.ServicesContract.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Mapper.SettingMapper
{
    internal static class PaymentMethodAutoMapperProfile
    {
        public static PaymentMethod DtoToEntity(PaymentMethodDto dto, IErrorLoggerService errorLogger)
        {
            try
            {
                var entity = new PaymentMethod();
                entity.Title = dto.Title;
                entity.ID = dto.Key;
                entity.Order = (int)dto.Row;
                return entity;
            }
            catch (Exception ex)
            {
                errorLogger.SaveError(ex);
                return new PaymentMethod();
            }
        }
        public static PaymentMethodDto EntityToDto(PaymentMethod entity, IErrorLoggerService errorLogger, int? row=null)
        {
            try
            {
                var dto = new PaymentMethodDto();
                dto.Title = entity.Title;
                dto.Key = entity.ID;
                dto.Row = (int)row != null ? (int)row : 1;
                return dto;
            }
            catch (Exception ex)
            {
                errorLogger.SaveError(ex);
                return new PaymentMethodDto();
            }
        }
        public static List<PaymentMethod> DtosToEntities(List<PaymentMethodDto> dtos, IErrorLoggerService errorLogger)
        {
            var entities = new List<PaymentMethod>();
            for (int i = 0; i < dtos.Count; i++)
            {
                var entity = new PaymentMethod();
                entity = DtoToEntity(dtos[i], errorLogger);
                entities.Add(entity);
            }
            return entities;
        }
        public static List<PaymentMethodDto> EntitiesToDtos(List<PaymentMethod> entities, IErrorLoggerService errorLogger)
        {
            var dtos = new List<PaymentMethodDto>();
            for (int i = 0; i < entities.Count; i++)
            {
                var dto = new PaymentMethodDto();
                dto = EntityToDto(entities[i],errorLogger, i + 1);
                dto.Row = i;
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}
