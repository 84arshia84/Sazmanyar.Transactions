using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Entities.ContractAccessGroups;
using AppCore.Entities.FactorAccessGroup;
using ApplicationService.DtoModels.ContractAccessGroupsDtos;
using ApplicationService.DtoModels.FactorAccessGroupDto;
using ApplicationService.Mapper.ContractAccessGroupMappers;
using ApplicationService.ServicesContract.ExceptionHandling;

namespace ApplicationService.Mapper.FactorAccessGroupAutoMapperProfile
{
    public static class FactorAccessGroupAutoMapper
    {
        public static async Task<FactorAccessGroup> DtoToEntity(AllFactorAccessGroupsDto dto, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entity = new FactorAccessGroup();
                entity.Id = dto.FactorAccessGroupDto.Id;
                entity.Title = dto.FactorAccessGroupDto.Title;
                entity.Description = dto.FactorAccessGroupDto.Description;
                return entity;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new FactorAccessGroup();
            }
        }
        public static async Task<AllFactorAccessGroupsDto> EntityToDtoAllAccessGroup(FactorAccessGroup entity, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dto = new AllFactorAccessGroupsDto();

                dto.FactorAccessGroupDto = new FactorAccessGroupDto();
                dto.FactorAccessGroupDto.Title = entity.Title;
                dto.FactorAccessGroupDto.Id = entity.Id;
                dto.FactorAccessGroupDto.Description = entity.Description;

                dto.FactorAccessGroupPermissions = new FactorAccessGroupPermissionsDto();
                dto.FactorAccessGroupPermissions = await FactorAccessGroupPermissionsAutoMapper.EntityToDto(entity.factorAccessGroupPermissions, errorLoggerService);

                dto.FactorAccessGroupProperties = new FactorAccessGroupPropertiesDto();
                dto.FactorAccessGroupProperties = await FactorAccessGroupPropertiesAutoMapper.EntityToDto(entity.factorAccessGroupProperties, errorLoggerService);



                dto.FactorAccessGroupFactorType = await FactorAccessGroupFactorTypeAutoMapper.EntitiesToDtos(entity.factorAccessGroupFactorTypes.ToList(), errorLoggerService);

                dto.FactorAccessGroupRoleOfOrganizations = await FactorAccessGroupRoleOfOrganizationsAutoMapper.EntitiesToDtos(entity.factorAccessGroupRoleOfOrganizations.ToList(), errorLoggerService);

                dto.FactorAccessGroupUsers = await FactorAccessGroupUsersAutoMapper.EntitiesToDtos(entity.factorAccessGroupUsers.ToList(), errorLoggerService);

                dto.FactorAccessGroupsOrganizationUnits = await FactorAccessGroupOrganizationUnitsAutoMapper.EntitiesToDtos(entity.factorAccessGroupOrganizationUnits.ToList(), errorLoggerService);

                dto.FactorAccessGroupGroups = await FactorAccessGroupGroupsAutoMapper.EntitiesToDtos(entity.FactorAccessGroupGroupChildren.ToList(), errorLoggerService);

                return dto;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new AllFactorAccessGroupsDto();
            }
        }
        public static async Task<List<FactorAccessGroupDto>> EntitiesToDtos(List<FactorAccessGroup> entity, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dtos = new List<FactorAccessGroupDto>();
                foreach (var item in entity)
                {
                    var dto = new FactorAccessGroupDto();
                    dto.Id = item.Id;
                    dto.Title = item.Title;
                    dto.Description = item.Description;
                    dtos.Add(dto);
                }
                return dtos;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new List<FactorAccessGroupDto>();
            }
        }
    }

}
