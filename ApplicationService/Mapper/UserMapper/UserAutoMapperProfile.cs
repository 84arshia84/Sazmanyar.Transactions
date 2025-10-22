using AppCore.Entities.User;
using ApplicationService.DtoModels.UserDtos;
using ApplicationService.ServicesContract.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Mapper.UserMapper
{
    internal static class UserAutoMapperProfile
    {
        public static LoginUserDto EntityToDto(User entity ,IErrorLoggerService errorLoggerService)
        {
			try
			{
                var dto = new LoginUserDto();
                dto.ID = entity.ID;
                dto.FullQualifyName = entity.FullQualifyName;
                dto.FirstName = entity.FirstName;
                dto.LastName = entity.LastName;
                dto.FullName = entity.FirstName + " " + entity.LastName;    
                return dto;
			}
			catch (Exception ex)
			{
                errorLoggerService.SaveError(ex);
                return new LoginUserDto();
			}
        }
        public static List<LoginUserDto> EntitiesToDtos(List<User> entities,IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dtos = new List<LoginUserDto>();
                foreach (var item in entities)
                {
                    var dto = new LoginUserDto();
                    dto = EntityToDto(item, errorLoggerService);
                    dtos.Add(dto);
                }
                return dtos;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new List<LoginUserDto>();
            }
        }
    }
}
