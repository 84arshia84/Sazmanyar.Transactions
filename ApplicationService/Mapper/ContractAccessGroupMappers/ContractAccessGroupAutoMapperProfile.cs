using AppCore.Entities.ContractAccessGroups;
using ApplicationService.DtoModels.ContractAccessGroupsDtos;
using ApplicationService.ServicesContract.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Mapper.ContractAccessGroupMappers
{
    internal class ContractAccessGroupAutoMapperProfile
    {
        public static async Task<ContractAccessGroup> DtoToEntity(AllAccessGroupsDto dto, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entity = new ContractAccessGroup();
                entity.Id = dto.ContractAccessGroup.Id;
                entity.Title = dto.ContractAccessGroup.Title;
                entity.Description = dto.ContractAccessGroup.Description;
                return entity;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new ContractAccessGroup();
            }
        }
        public static async Task<ContractAccessGroupDto> EntityToDto(ContractAccessGroup entity, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dto = new ContractAccessGroupDto();
                dto.Id = entity.Id;
                dto.Title = entity.Title;
                dto.Description = entity.Description;
                return dto;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new ContractAccessGroupDto();
            }
        }
        public static async Task<AllAccessGroupsDto> EntityToDtoAllAccessGroup(ContractAccessGroup entity, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dto = new AllAccessGroupsDto();

                dto.ContractAccessGroup = new ContractAccessGroupDto();
                dto.ContractAccessGroup.Title = entity.Title;
                dto.ContractAccessGroup.Id = entity.Id;
                dto.ContractAccessGroup.Description = entity.Description;

                dto.ContractAccessGroupPermissions = new ContractAccessGroupPermissionsDto();
                dto.ContractAccessGroupPermissions = await ContractAccessGroupPermissionsAutoMapperProfile.EntityToDto(entity.ContractAccessGroupPermissions,errorLoggerService);

                dto.ContractAccessGroupProperties = new ContractAccessGroupPropertiesDto();
                dto.ContractAccessGroupProperties = await ContractAccessGroupPropertiesAutoMapperProfile.EntityToDto(entity.ContractAccessGroupProperties, errorLoggerService);

                dto.ContractAccessGroupSystemParts = new List<ContractAccessGroupSystemPartDto>();
                dto.ContractAccessGroupSystemParts = await ContractAccessGroupSystemPartAutoMapperProfile.EntitiesToDtos(entity.ContractAccessGroupSystemParts, errorLoggerService);

                dto.ContractAccessGroupContractType = await ContractAccessGroupContractTypeAutoMapperProfile.EntitiesToDtos(entity.ContractAccessGroupContractTypes.ToList(), errorLoggerService);

                dto.ContractAccessGroupRoleOfOrganizations = await ContractAccessGroupRoleOfOrganizationsAutoMapperProfile.EntitiesToDtos(entity.ContractAccessGroupRoleOfOrganizations.ToList(), errorLoggerService);

                dto.ContractAccessGroupUsers = await ContractAccessGroupUsersAutoMapperProfile.EntitiesToDtos(entity.ContractAccessGroupUsers.ToList(), errorLoggerService);

                dto.ContractAccessGroupsOrganizationUnits = await ContractAccessGroupOrganizationUnitsAutoMapperProfile.EntitiesToDtos(entity.ContractAccessGroupOrganizationUnits.ToList(), errorLoggerService);

                dto.ContractAccessGroupGroups = await ContractAccessGroupGroupsAutoMapperProfile.EntitiesToDtos(entity.ContractAccessGroupGroupChildren.ToList(), errorLoggerService);

                return dto;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new AllAccessGroupsDto();
            }
        }
        public static async Task<List<ContractAccessGroupDto>> EntitiesToDtos(List<ContractAccessGroup>entity, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dtos = new List<ContractAccessGroupDto>();
                foreach (var item in entity)
                {
                    var dto = new ContractAccessGroupDto(); 
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
                return new List<ContractAccessGroupDto>();
            }
        }
    }
}
