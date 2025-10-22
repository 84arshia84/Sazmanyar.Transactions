using AppCore.Entities.SettingEntities.TypeOfGuarantees;
using ApplicationService.DtoModels.SettingDtos;
using ApplicationService.ServicesContract.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Mapper.SettingMapper
{
    internal static class TypeOfGuaranteeAutoMapperProfiler
    {
        public static TypeOfGuarantee DtoToEntity(TypeOfGuaranteeDto dto, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entity = new TypeOfGuarantee();
                entity.Title = dto.Title;
                entity.ID = dto.Id;
                entity.Order = dto.Order;
                return entity;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new TypeOfGuarantee();
            }

        }
        public static TypeOfGuaranteeDto EntityToDto(TypeOfGuarantee entity, IErrorLoggerService errorLoggerService, int? row = null)
        {
            try
            {
                var dto = new TypeOfGuaranteeDto();
                dto.Title = entity.Title;
                dto.Id = entity.ID;
                dto.Order = (int)row != null ? (int)row : 1;
                return dto;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new TypeOfGuaranteeDto();
            }

        }
        public static List<TypeOfGuarantee> DtosToEntities(List<TypeOfGuaranteeDto> dtos, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entities = new List<TypeOfGuarantee>();
                for (int i = 0; i < dtos.Count; i++)
                {
                    var entity = new TypeOfGuarantee();
                    entity = DtoToEntity(dtos[i], errorLoggerService);
                    entities.Add(entity);
                }
                return entities;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new List<TypeOfGuarantee>();
            }

        }
        public static List<TypeOfGuaranteeDto> EntitiesToDtos(List<TypeOfGuarantee> entities,IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dtos = new List<TypeOfGuaranteeDto>();
                for (int i = 0; i < entities.Count; i++)
                {
                    var dto = new TypeOfGuaranteeDto();
                    dto = EntityToDto(entities[i], errorLoggerService, i + 1);
                    dtos.Add(dto);
                }
                return dtos;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError (ex);
                return new List<TypeOfGuaranteeDto>();
            }
            
        }
    }
}
