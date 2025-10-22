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
    internal class ContractAccessGroupPropertiesAutoMapperProfile
    {
        public static async Task<ContractAccessGroupProperties> DtoToEntity(ContractAccessGroupPropertiesDto dto,Guid contractAccessGroup, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entity = new ContractAccessGroupProperties();
                entity.Id = dto.Id;
                entity.ContractAccessGroupId = contractAccessGroup;
                entity.ContractTypeSave = dto.ContractTypeSave;
                entity.ContractTypeView = dto.ContractTypeView;
                entity.ContractTypeEdit = dto.ContractTypeEdit;
                entity.ContractTypeDelete = dto.ContractTypeDelete;
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
                return new ContractAccessGroupProperties();
            }
        }
        public static async Task<ContractAccessGroupPropertiesDto> EntityToDto(ContractAccessGroupProperties entity, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dto = new ContractAccessGroupPropertiesDto();
                dto.Id = entity.Id;
                dto.ContractTypeSave = entity.ContractTypeSave;
                dto.ContractTypeView = entity.ContractTypeView;
                dto.ContractTypeEdit = entity.ContractTypeEdit;
                dto.ContractTypeDelete = entity.ContractTypeDelete;
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
                return new ContractAccessGroupPropertiesDto();
            }
        }
    }
}
