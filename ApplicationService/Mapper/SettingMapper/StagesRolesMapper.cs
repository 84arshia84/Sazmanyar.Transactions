using ApplicationService.DtoModels.SettingDtos;
using AppCore.Entities.SettingEntities.StagesRoles;
using ApplicationService.ServicesContract.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Mapper.SettingMapper
{
    internal static class StagesRolesMapper
    {
        public static StagesRoles DtoToEntity(StagesRolesDto dto, IErrorLoggerService errorLogger)
        {
            try
            {
                var entity = new StagesRoles();
                entity.CanEdit = dto.CanEdit;
                entity.Id = dto.Key;
                entity.RoleId = dto.RoleId;
                entity.StageId = dto.StageId;
                return entity;
            }
            catch (Exception ex)
            {
                errorLogger.SaveError(ex);
                return new StagesRoles();
            }
        }
        public static StagesRolesDto EntityToDto(StagesRoles entity, IErrorLoggerService errorLogger)
        {
            var dto = new StagesRolesDto();
            dto.Key = entity.Id;
            dto.CanEdit = entity.CanEdit;
            dto.StageId = entity.StageId;
            dto.RoleId = entity.RoleId;
            return dto;
        }
        public static List<StagesRoles> DtosToEntities(List<StagesRolesDto> dtos, IErrorLoggerService errorLogger)
        {
            var entities = new List<StagesRoles>();
            for (int i = 0; i < dtos.Count; i++)
            {
                var entity = new StagesRoles();
                entity = DtoToEntity(dtos[i], errorLogger);
                entities.Add(entity);
            }
            return entities;
        }
        public static List<StagesRolesDto> EntitiesToDtos(List<StagesRoles> entities, IErrorLoggerService errorLogger)
        {
            var dtos = new List<StagesRolesDto>();
            for (int i = 0; i < entities.Count; i++)
            {
                var dto = new StagesRolesDto();
                dto = EntityToDto(entities[i], errorLogger);
                dto.Row = i;
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}
