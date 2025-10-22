using AppCore.Entities.SettingEntities.RoleOfOrganizations;
using ApplicationService.DtoModels.SettingDtos;
using ApplicationService.ServicesContract.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Mapper.SettingMapper
{
    internal static class RoleOfOrganizationAutoMapperProfile
    {
        public static RoleOfOrganization DtoToEntity(RoleOfOrganizationDto dto, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entity = new RoleOfOrganization();
                entity.Title = dto.title;
                entity.ID = dto.key;
                entity.Order = (int)dto.row;
                return entity;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new RoleOfOrganization();
            }

        }
        public static RoleOfOrganizationDto EntityToDto(RoleOfOrganization entity,IErrorLoggerService errorLoggerService, int? row = null)
        {
            try
            {
                var dto = new RoleOfOrganizationDto();
                dto.title = entity.Title;
                dto.key = entity.ID;
                dto.row = row;
                return dto;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new RoleOfOrganizationDto();
            }

        }
        public static List<RoleOfOrganization> DtosToEntities(List<RoleOfOrganizationDto> dtos, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entities = new List<RoleOfOrganization>();
                for (int i = 0; i < dtos.Count; i++)
                {
                    var entity = new RoleOfOrganization();
                    entity = DtoToEntity(dtos[i], errorLoggerService);
                    entities.Add(entity);
                }
                return entities;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new List<RoleOfOrganization>();
            }
        }
        public static List<RoleOfOrganizationDto> EntitiesToDtos(List<RoleOfOrganization> entities, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dtos = new List<RoleOfOrganizationDto>();
                for (int i = 0; i < entities.Count; i++)
                {
                    var dto = new RoleOfOrganizationDto();
                    dto = EntityToDto(entities[i], errorLoggerService, i + 1);
                    dtos.Add(dto);
                }
                return dtos;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new List<RoleOfOrganizationDto>();
            }

        }
    }
}
