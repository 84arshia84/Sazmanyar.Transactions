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
    public static class FactorAccessGroupPropertiesAutoMapper
    {
        public static async Task<FactorAccessGroupProperties> DtoToEntity(FactorAccessGroupPropertiesDto dto, Guid factorAccessGroupId, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entity = new FactorAccessGroupProperties();
                entity.Id = dto.Id;
                entity.FactorAccessGroupId = factorAccessGroupId;
                entity.FactorTypeSave = dto.FactorTypeSave;
                entity.FactorTypeView = dto.FactorTypeView;
                entity.FactorTypeEdit = dto.FactorTypeEdit;
                entity.FactorTypeDelete = dto.FactorTypeDelete;
                entity.RoleOfOrganizationSave = dto.RoleOfOrganizationSave;
                entity.RoleOfOrganizationView = dto.RoleOfOrganizationView;
                entity.RoleOfOrganizationEdit = dto.RoleOfOrganizationEdit;
                entity.RoleOfOrganizationDelete = dto.RoleOfOrganizationDelete;
                entity.OrganizationUnitSave = dto.OrganizationUnitSave;
                entity.OrganizationUnitView = dto.OrganizationUnitView;
                entity.OrganizationUnitEdit = dto.OrganizationUnitEdit;
                entity.OrganizationUnitDelete = dto.OrganizationUnitDelete;
                return entity;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new FactorAccessGroupProperties();
            }
        }

        public static async Task<FactorAccessGroupPropertiesDto> EntityToDto(FactorAccessGroupProperties entity, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dto = new FactorAccessGroupPropertiesDto();
                dto.Id = entity.Id;
                dto.FactorTypeSave = entity.FactorTypeSave;
                dto.FactorTypeView = entity.FactorTypeView;
                dto.FactorTypeEdit = entity.FactorTypeEdit;
                dto.FactorTypeDelete = entity.FactorTypeDelete;
                dto.RoleOfOrganizationSave = entity.RoleOfOrganizationSave;
                dto.RoleOfOrganizationView = entity.RoleOfOrganizationView;
                dto.RoleOfOrganizationEdit = entity.RoleOfOrganizationEdit;
                dto.RoleOfOrganizationDelete = entity.RoleOfOrganizationDelete;
                dto.OrganizationUnitSave = entity.OrganizationUnitSave;
                dto.OrganizationUnitView = entity.OrganizationUnitView;
                dto.OrganizationUnitEdit = entity.OrganizationUnitEdit;
                dto.OrganizationUnitDelete = entity.OrganizationUnitDelete;
                return dto;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new FactorAccessGroupPropertiesDto();
            }
        }
    }
}
