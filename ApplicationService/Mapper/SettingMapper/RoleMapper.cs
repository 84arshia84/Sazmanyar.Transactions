using ApplicationService.DtoModels.SettingDtos;
using AppCore.Entities.SettingEntities.PaymentMethods;
using AppCore.Entities.SettingEntities.Roles;
using ApplicationService.ServicesContract.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Mapper.SettingMapper
{
    internal static class RoleMapper
    {
        public static RoleDto EntityToDto(Role entity, IErrorLoggerService errorLogger)
        {
            try
            {
                var dto = new RoleDto();
                dto.Title = entity.Title;
                dto.Key = entity.ID;
                return dto;
            }
            catch (Exception ex)
            {
                errorLogger.SaveError(ex);
                return new RoleDto();
            }
        }
        public static List<RoleDto> EntitiesToDtos(List<Role> entities, IErrorLoggerService errorLogger)
        {
            var dtos = new List<RoleDto>();
            for (int i = 0; i < entities.Count; i++)
            {
                var dto = new RoleDto();
                dto = EntityToDto(entities[i], errorLogger);
                dto.Row = i;
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}
