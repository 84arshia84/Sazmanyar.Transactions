using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Entities.InvoiceAccessGroups.AccessGroupGroups;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroupContractTypes;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroupInvoiceTypes;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroupOrganizationUnits;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroupPermissions;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroupProperties;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroupRoleOfOrganizations;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroups;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroupUsers;
using ApplicationService.DtoModels.InvoiceDtos.InvoiceAccessGroupsDtos;
using ApplicationService.Services.ExceptionHandlingService;
using ApplicationService.ServicesContract.ExceptionHandling;

namespace ApplicationService.Mapper.InvoiceInformationsMappers
{
    public static class InvoiceAccessGroupMapper
    {
        public static GetInvoiceAccessGroupDto GetInvoiceAccessGroup(this InvoiceAccessGroup invoiceAccessGroup, IErrorLoggerService errorLoggerService)
        {
            try
            {
                return new GetInvoiceAccessGroupDto
                {
                    Id = invoiceAccessGroup.Id,
                    Title = invoiceAccessGroup.Title,
                    Description = invoiceAccessGroup.Description,
                    InvoiceAccessGroupInvoiceTypes = invoiceAccessGroup.InvoiceAccessGroupInvoiceTypes.ToList().GetInvoiceAccessGroupInvoiceTypes(),
                    InvoiceAccessGroupContractTypes = invoiceAccessGroup.InvoiceAccessGroupContractTypes.ToList().GetInvoiceAccessGroupContractTypes(),
                    InvoiceAccessGroupOrganizationUnits = invoiceAccessGroup.InvoiceAccessGroupOrganizationUnits.ToList().GetInvoiceAccessGroupOrganizationUnits(),
                    InvoiceAccessGroupRoleOfOrganizations = invoiceAccessGroup.InvoiceAccessGroupRoleOfOrganizations.ToList().GetInvoiceAccessGroupRoleOfOrganizations(),
                    InvoiceAccessGroupGroups = invoiceAccessGroup.InvoiceAccessGroupGroups.ToList().GetInvoiceAccessGroupGroups(),
                    InvoiceAccessGroupUsers = invoiceAccessGroup.InvoiceAccessGroupUsers.ToList().GetInvoiceAccessGroupUsers(),
                    InvoiceAccessGroupPermissions = invoiceAccessGroup.InvoiceAccessGroupPermissions.FirstOrDefault()?.InvoiceAccessGroupPermissions(),
                    InvoiceAccessGroupProperties = invoiceAccessGroup.InvoiceAccessGroupProperties.FirstOrDefault().GetInvoiceAccessGroupProperties(),
                };
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return (new GetInvoiceAccessGroupDto());
            }
        }
        public static List<AllInvoiceAccessGroupsDto> GetAllAccessGroupDto(this List<InvoiceAccessGroup> invoiceAccessGroups)
        {
            return invoiceAccessGroups.Select(a => new AllInvoiceAccessGroupsDto
            {
                Id = a.Id,
                Title = a.Title,
                Description = a.Description,
            }).ToList();
        }
        public static List<Guid> GetInvoiceAccessGroupInvoiceTypes(this List<InvoiceAccessGroupInvoiceType> invoiceAccessGroupInvoiceTypes)
        {
            return invoiceAccessGroupInvoiceTypes.Select(it => it.InvoiceTypeId).ToList();
        }
        public static List<Guid> GetInvoiceAccessGroupContractTypes(this List<InvoiceAccessGroupContractType> invoiceAccessGroupContractTypes)
        {
            return invoiceAccessGroupContractTypes.Select(ct => ct.ContractTypeId).ToList();
        }
        public static List<Guid> GetInvoiceAccessGroupOrganizationUnits(this List<InvoiceAccessGroupOrganizationUnit> invoiceAccessGroupOrganizationUnits)
        {
            return invoiceAccessGroupOrganizationUnits.Select(ou => ou.OrganizationUnitId).ToList();
        }
        public static List<Guid> GetInvoiceAccessGroupRoleOfOrganizations(this List<InvoiceAccessGroupRoleOfOrganization> invoiceAccessGroupRoleOfOrganizations)
        {
            return invoiceAccessGroupRoleOfOrganizations.Select(ro => ro.RoleOfOrganizationId).ToList();
        }
        public static List<Guid> GetInvoiceAccessGroupGroups(this List<InvoiceAccessGroupGroup> invoiceAccessGroupGroups)
        {
            return invoiceAccessGroupGroups.Select(agg => agg.GroupId).ToList();
        }
        public static List<Guid> GetInvoiceAccessGroupUsers(this List<InvoiceAccessGroupUser> invoiceAccessGroupUsers)
        {
            return invoiceAccessGroupUsers.Select(agu => agu.UserId).ToList();
        }
        public static InvoiceAccessGroupPermissionsDto InvoiceAccessGroupPermissions(this InvoiceAccessGroupPermissions invoiceAccessGroupPermissions)
        {
            return new InvoiceAccessGroupPermissionsDto
            {
                Invoice = invoiceAccessGroupPermissions.Invoice,
                AccessGroupSettings = invoiceAccessGroupPermissions.AccessGroupSettings,
                BaseSettings = invoiceAccessGroupPermissions.BaseSettings,
                WorkFlow = invoiceAccessGroupPermissions.WorkFlow,
            };
        }
        public static InvoiceAccessGroupPropertiesDto GetInvoiceAccessGroupProperties(this InvoiceAccessGroupProperties invoiceAccessGroupProperties)
        {
            return new InvoiceAccessGroupPropertiesDto
            {
                InvoiceTypeSave = invoiceAccessGroupProperties.InvoiceTypeSave,
                InvoiceTypeView = invoiceAccessGroupProperties.InvoiceTypeView,
                InvoiceTypeEdit = invoiceAccessGroupProperties.InvoiceTypeEdit,
                InvoiceTypeDelete = invoiceAccessGroupProperties.InvoiceTypeDelete,
                ContractTypeSave = invoiceAccessGroupProperties.ContractTypeSave,
                ContractTypeView = invoiceAccessGroupProperties.ContractTypeView,
                ContractTypeEdit = invoiceAccessGroupProperties.ContractTypeEdit,
                ContractTypeDelete = invoiceAccessGroupProperties.ContractTypeDelete,
                RoleOfOrganizationSave = invoiceAccessGroupProperties.RoleOfOrganizationSave,
                RoleOfOrganizationView = invoiceAccessGroupProperties.RoleOfOrganizationView,
                RoleOfOrganizationEdit = invoiceAccessGroupProperties.RoleOfOrganizationEdit,
                RoleOfOrganizationDelete = invoiceAccessGroupProperties.RoleOfOrganizationDelete,
                OrganizationUnitSave = invoiceAccessGroupProperties.OrganizationUnitSave,
                OrganizationUnitView = invoiceAccessGroupProperties.OrganizationUnitView,
                OrganizationUnitEdit = invoiceAccessGroupProperties.OrganizationUnitEdit,
                OrganizationUnitDelete = invoiceAccessGroupProperties.OrganizationUnitDelete,
            };
        }
        public static InvoiceAccessGroup InvoiceAccessGroupDtoToEntity(this AddInvoiceAccessGroupDto addInvoiceAccessGroupDto, Guid invoiceAccessGroupId, IErrorLoggerService errorLoggerService)
        {
            try
            {
                return new InvoiceAccessGroup
                {
                    Id = invoiceAccessGroupId,
                    Title = addInvoiceAccessGroupDto.Title,
                    Description = addInvoiceAccessGroupDto.Description
                };
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return (new InvoiceAccessGroup());
            }
        }
        public static List<InvoiceAccessGroupInvoiceType> InvoiceAccessGroupInvoiceTypeIdsToEntities(this List<Guid> InvoiceAccessGroupInvoiceTypeIds, Guid invoiceAccessGroupId, IErrorLoggerService errorLoggerService)
        {
            try
            {
                return InvoiceAccessGroupInvoiceTypeIds.Select(it => new InvoiceAccessGroupInvoiceType
                {
                    InvoiceAccessGroupId = invoiceAccessGroupId,
                    InvoiceTypeId = it,
                }).ToList();
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return (new List<InvoiceAccessGroupInvoiceType>());
            }
        }
        public static List<InvoiceAccessGroupContractType> InvoiceAccessGroupContractTypeIdsToEntities(this List<Guid> InvoiceAccessGroupContractTypeIds, Guid invoiceAccessGroupId, IErrorLoggerService errorLoggerService)
        {
            try
            {
                return InvoiceAccessGroupContractTypeIds.Select(it => new InvoiceAccessGroupContractType
                {
                    InvoiceAccessGroupId = invoiceAccessGroupId,
                    ContractTypeId = it,
                }).ToList();
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return (new List<InvoiceAccessGroupContractType>());
            }
        }
        public static List<InvoiceAccessGroupOrganizationUnit> InvoiceAccessGroupOrganizationUnitIdsToEntities(this List<Guid> InvoiceAccessGroupOrganizationUnitIds, Guid invoiceAccessGroupId, IErrorLoggerService errorLoggerService)
        {
            try
            {
                return InvoiceAccessGroupOrganizationUnitIds.Select(ou => new InvoiceAccessGroupOrganizationUnit
                {
                    InvoiceAccessGroupId = invoiceAccessGroupId,
                    OrganizationUnitId = ou,
                }).ToList();
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return (new List<InvoiceAccessGroupOrganizationUnit>());
            }
        }
        public static List<InvoiceAccessGroupRoleOfOrganization> InvoiceAccessGroupRoleOfOrganizationIdsToEntities(this List<Guid> InvoiceAccessGroupRoleOfOrganizationIds, Guid invoiceAccessGroupId, IErrorLoggerService errorLoggerService)
        {
            try
            {
                return InvoiceAccessGroupRoleOfOrganizationIds.Select(roo => new InvoiceAccessGroupRoleOfOrganization
                {
                    InvoiceAccessGroupId = invoiceAccessGroupId,
                    RoleOfOrganizationId = roo,
                }).ToList();
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return (new List<InvoiceAccessGroupRoleOfOrganization>());
            }
        }
        public static List<InvoiceAccessGroupGroup> InvoiceAccessGroupGroupIdsToEntities(this List<Guid> InvoiceAccessGroupGroupIds, Guid invoiceAccessGroupId, IErrorLoggerService errorLoggerService)
        {
            try
            {
                return InvoiceAccessGroupGroupIds.Select(agg => new InvoiceAccessGroupGroup
                {
                    GroupId = agg,
                    ParentGroupId = invoiceAccessGroupId,
                }).ToList();
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return (new List<InvoiceAccessGroupGroup>());
            }
        }
        public static List<InvoiceAccessGroupUser> InvoiceAccessGroupUserIdsToEntities(this List<Guid> InvoiceAccessGroupUserIds, Guid invoiceAccessGroupId, IErrorLoggerService errorLoggerService)
        {
            try
            {
                return InvoiceAccessGroupUserIds.Select(agu => new InvoiceAccessGroupUser
                {
                    UserId = agu,
                    AccessGroupId = invoiceAccessGroupId,
                }).ToList();
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return (new List<InvoiceAccessGroupUser>());
            }
        }
        public static InvoiceAccessGroupPermissions InvoiceAccessGroupPermissionsDtoToEntity(this InvoiceAccessGroupPermissionsDto invoiceAccessGroupPermissionsDto, Guid invoiceAccessGroupId, IErrorLoggerService errorLoggerService)
        {
            try
            {
                return new InvoiceAccessGroupPermissions
                {
                    InvoiceAccessGroupId = invoiceAccessGroupId,
                    Invoice = invoiceAccessGroupPermissionsDto.Invoice,
                    AccessGroupSettings = invoiceAccessGroupPermissionsDto.AccessGroupSettings,
                    BaseSettings = invoiceAccessGroupPermissionsDto.BaseSettings,
                    WorkFlow = invoiceAccessGroupPermissionsDto.WorkFlow
                };
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return (new InvoiceAccessGroupPermissions());
            }
        }
        public static InvoiceAccessGroupProperties InvoiceAccessGroupPropertiesDtoToEntity(this InvoiceAccessGroupPropertiesDto invoiceAccessGroupPropertiesDto, Guid invoiceAccessGroupId, IErrorLoggerService errorLoggerService)
        {
            try
            {
                return new InvoiceAccessGroupProperties
                {
                    InvoiceAccessGroupId = invoiceAccessGroupId,
                    InvoiceTypeSave = invoiceAccessGroupPropertiesDto.InvoiceTypeSave,
                    InvoiceTypeView = invoiceAccessGroupPropertiesDto.InvoiceTypeView,
                    InvoiceTypeEdit = invoiceAccessGroupPropertiesDto.InvoiceTypeEdit,
                    InvoiceTypeDelete = invoiceAccessGroupPropertiesDto.InvoiceTypeDelete,
                    ContractTypeSave = invoiceAccessGroupPropertiesDto.ContractTypeSave,
                    ContractTypeView = invoiceAccessGroupPropertiesDto.ContractTypeView,
                    ContractTypeEdit = invoiceAccessGroupPropertiesDto.ContractTypeEdit,
                    ContractTypeDelete = invoiceAccessGroupPropertiesDto.ContractTypeDelete,
                    RoleOfOrganizationSave = invoiceAccessGroupPropertiesDto.RoleOfOrganizationSave,
                    RoleOfOrganizationView = invoiceAccessGroupPropertiesDto.RoleOfOrganizationView,
                    RoleOfOrganizationEdit = invoiceAccessGroupPropertiesDto.RoleOfOrganizationEdit,
                    RoleOfOrganizationDelete = invoiceAccessGroupPropertiesDto.RoleOfOrganizationDelete,
                    OrganizationUnitSave = invoiceAccessGroupPropertiesDto.OrganizationUnitSave,
                    OrganizationUnitView = invoiceAccessGroupPropertiesDto.OrganizationUnitView,
                    OrganizationUnitEdit = invoiceAccessGroupPropertiesDto.OrganizationUnitEdit,
                    OrganizationUnitDelete = invoiceAccessGroupPropertiesDto.OrganizationUnitDelete
                };
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return (new InvoiceAccessGroupProperties());
            }
        }
    }
}
