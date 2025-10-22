using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Entities.ContractAccessGroups;
using AppCore.Entities.FactorAccessGroup;
using ApplicationService.DtoModels.ContractAccessGroupsDtos;
using ApplicationService.DtoModels.FactorAccessGroupDto;
using ApplicationService.ServicesContract.ExceptionHandling;

namespace ApplicationService.Mapper.FactorAccessGroupAutoMapperProfile
{
    public static class FactorAccessGroupPermissionsAutoMapper
    {
        public static async Task<FactorAccessGroupPermissions> DtoToEntity(FactorAccessGroupPermissionsDto dto, Guid factorAccessGroupId, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entity = new FactorAccessGroupPermissions();
                entity.Id = dto.Id;
                entity.FactorAccessGroupId = factorAccessGroupId;
                entity.AccessGroupSettings = dto.AccessGroupSettings;
                entity.BaseSettings = dto.BaseSettings;              
                entity.WorkFlow = dto.WorkFlow;
                entity.Factor=dto.Factor;
                return entity;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new FactorAccessGroupPermissions();
            }
        }

        public static async Task<FactorAccessGroupPermissionsDto> EntityToDto(FactorAccessGroupPermissions entity, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dto = new FactorAccessGroupPermissionsDto();
                dto.Id = entity.Id;
                dto.AccessGroupSettings = entity.AccessGroupSettings;
                dto.BaseSettings = entity.BaseSettings;              
                dto.WorkFlow = entity.WorkFlow;
                dto.FactorAccessGroupId = entity.FactorAccessGroupId;   
                dto.Factor = entity.Factor;
                return dto;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new FactorAccessGroupPermissionsDto();
            }
        }
    }
}
