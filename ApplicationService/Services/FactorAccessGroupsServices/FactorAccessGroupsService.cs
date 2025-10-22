using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Entities.ContractAccessGroups;
using AppCore.Entities.ContractsInformation.ContractAddendums;
using AppCore.Enums;
using AppCore.UnitOfWork;
using ApplicationService.DtoModels.ContractAccessGroupsDtos;
using ApplicationService.DtoModels.FactorAccessGroupDto;
using ApplicationService.DtoModels.UserDtos;
using ApplicationService.Mapper.ContractAccessGroupMappers;
using ApplicationService.Mapper.FactorAccessGroupAutoMapperProfile;
using ApplicationService.Mapper.FactorMappers;
using ApplicationService.ServicesContract.ContractAccessGroups;
using ApplicationService.ServicesContract.ExceptionHandling;
using ApplicationService.ServicesContract.FactorAccessGroups;

namespace ApplicationService.Services.FactorAccessGroupsServices
{
    public class FactorAccessGroupsService : IFactorAccessGroupsService
    {
        private IUnitOfWork _unitOfWork;
        private IErrorLoggerService _errorLoggerService;
       private IFactorAccessGroupFilterService _filterService;
        public FactorAccessGroupsService(
               IUnitOfWork unitOfWork,
               IErrorLoggerService errorLoggerService,
              IFactorAccessGroupFilterService filterService
               )
        {
            _unitOfWork = unitOfWork;
            _errorLoggerService = errorLoggerService;
            _filterService = filterService;
        }
        public async Task<(string message, bool isSuccess)> AddAccessGroup(AllFactorAccessGroupsDto factorAccessGroup)
        {
            try
            {
                factorAccessGroup.FactorAccessGroupDto.Id = Guid.NewGuid();

                var Core = await FactorAccessGroupAutoMapper.DtoToEntity(factorAccessGroup , _errorLoggerService);
                (string, bool) Group;
                (string, bool) Roles;
                (string, bool) Types;
                (string, bool) Units;
                (string, bool) Users;
                (string, bool) Permission;
                (string, bool) Properties;


                await _unitOfWork.FactorAccessGroupRepository.AddAccessGroup(Core);

                await _unitOfWork.Save();

                if (factorAccessGroup.FactorAccessGroupGroups != null)
                {
                    Group = await AddAccessGroupGroups(factorAccessGroup.FactorAccessGroupGroups, Core.Id);
                    if (!Group.Item2)
                    {
                        return Group;
                    }
                }
                if (factorAccessGroup.FactorAccessGroupFactorType != null)
                {
                    Types = await AddAccessGroupFactorTypes(factorAccessGroup.FactorAccessGroupFactorType, Core.Id);
                    if (!Types.Item2)
                    {
                        return Types;
                    }
                }
                if (factorAccessGroup.FactorAccessGroupRoleOfOrganizations != null)
                {
                    Roles = await AddAccessGroupRoleOfOrganizations(factorAccessGroup.FactorAccessGroupRoleOfOrganizations, Core.Id);
                    if (!Roles.Item2)
                    {
                        return Roles;
                    }
                }
                if (factorAccessGroup.FactorAccessGroupsOrganizationUnits != null)
                {
                    Units = await AddAccessGroupOrganizationUnits(factorAccessGroup.FactorAccessGroupsOrganizationUnits, Core.Id);
                    if (!Units.Item2)
                    {
                        return Units;
                    }
                }
                if (factorAccessGroup.FactorAccessGroupUsers != null)
                {
                    Users = await AddAccessGroupUsers(factorAccessGroup.FactorAccessGroupUsers, Core.Id);
                    if (!Users.Item2)
                    {
                        return Users;
                    }
                }
                if (factorAccessGroup.FactorAccessGroupPermissions != null)
                {
                    Permission = await AddAccessGroupPermissions(factorAccessGroup.FactorAccessGroupPermissions, Core.Id);
                    if (!Permission.Item2)
                    {
                        return Permission;
                    }
                }
                if (factorAccessGroup.FactorAccessGroupProperties != null)
                {
                    Properties = await AddAccessGroupProperties(factorAccessGroup.FactorAccessGroupProperties, Core.Id);
                    if (!Properties.Item2)
                    {
                        return Properties;
                    }
                }

                await _unitOfWork.Save();
                return ("افزودن گروه دسترسی با موفقیت انجام شد.", true);
            }
            catch (Exception ex)
            {

                return ("افزودن گروه دسترسی با خطا مواجه شد.", false);
            }
        }

        public async Task<(string message, bool isSuccess)> AddAccessGroupFactorTypes(List<Guid> factorAccessGroupFactorTypes, Guid CoreId)
        {
            try
            {
                await _unitOfWork.FactorAccessGroupRepository.AddAccessGroupFactorTypes(
                    await FactorAccessGroupFactorTypeAutoMapper.DtosToEntities(factorAccessGroupFactorTypes, CoreId, _errorLoggerService));
                return ("افزودن نوع فاکتور موفقیت آمیز بود", true);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("افزودن نوع فاکتور با خطا مواجه شد.", false);
            }
        }

        public async Task<(string message, bool isSuccess)> AddAccessGroupGroups(List<Guid> factorAccessGroupGroups, Guid CoreId)
        {
            try
            {
                await _unitOfWork.FactorAccessGroupRepository.AddAccessGroupGroups(
                    await FactorAccessGroupGroupsAutoMapper.DtosToEntities(factorAccessGroupGroups, CoreId, _errorLoggerService));
                return ("افزودن گروه ها موفقیت آمیز بود", true);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("خطا در افزودن گروه ها ", false);
            }
        }

        public async Task<(string message, bool isSuccess)> AddAccessGroupOrganizationUnits(List<Guid> factorAccessGroupOrganizations, Guid CoreId)
        {
            try
            {
                await _unitOfWork.FactorAccessGroupRepository.AddAccessGroupOrganizationUnits(
                    await FactorAccessGroupOrganizationUnitsAutoMapper.DtosToEntities(factorAccessGroupOrganizations, CoreId, _errorLoggerService));
                return ("افزودن گروه ها موفقیت آمیز بود", true);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("خطا در افزودن گروه ها ", false);
            }
        }

        public async Task<(string message, bool isSuccess)> AddAccessGroupPermissions(FactorAccessGroupPermissionsDto factorAccessGroupPermissions, Guid CoreId)
        {
            try
            {
                await _unitOfWork.FactorAccessGroupRepository.AddAccessGroupPermissions(
                    await FactorAccessGroupPermissionsAutoMapper.DtoToEntity(factorAccessGroupPermissions, CoreId, _errorLoggerService));
                return ("افزودن دسترسی های سامانه موفقیت آمیز بود", true);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("خطا در افزودن دسترسی های سامانه  ", false);
            }
        }

        public async Task<(string message, bool isSuccess)> AddAccessGroupProperties(FactorAccessGroupPropertiesDto factorAccessGroupProperties, Guid CoreId)
        {

            try
            {
                await _unitOfWork.FactorAccessGroupRepository.AddAccessGroupProperties(
                    await FactorAccessGroupPropertiesAutoMapper.DtoToEntity(factorAccessGroupProperties, CoreId, _errorLoggerService));
                return ("افزودن جزئیات گروه دسترسی موفقیت آمیز بود", true);

            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("خطا در افزودن جزئیات گروه دسترسی  ", false);
            }
        }

        public async Task<(string message, bool isSuccess)> AddAccessGroupRoleOfOrganizations(List<Guid> factorAccessGroupRoles, Guid CoreId)
        {
            try
            {
                await _unitOfWork.FactorAccessGroupRepository.AddAccessGroupRoleOfOrganizations(
                    await FactorAccessGroupRoleOfOrganizationsAutoMapper.DtosToEntities(factorAccessGroupRoles, CoreId, _errorLoggerService));
                return ("افزودن نقش ها موفقیت آمیز بود", true);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("خطا در افزودن نقش ها ", false);

            }
        }

        public async Task<(string message, bool isSuccess)> AddAccessGroupUsers(List<Guid> factorAccessGroups, Guid CoreId)
        {
            try
            {
                await _unitOfWork.FactorAccessGroupRepository.AddAccessGroupUsers(
                    await FactorAccessGroupUsersAutoMapper.DtosToEntities(factorAccessGroups, CoreId, _errorLoggerService));
                return ("افزودن کاربر ها موفقیت آمیز بود", true);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("خطا در افزودن کاربر ها ", false);
            }
        }

        public async Task<(string message, bool isSuccess)> Delete(Guid id)
        {
            try
            {
                var result = await _unitOfWork.FactorAccessGroupRepository.DeleteCore(id);
                await _unitOfWork.Save();
                if (!result)
                {
                    return ("خطا در حذف گروه دسترسی ", false);
                }
                return ("حذف گروه دسترسی موفقیت آمیز بود", true);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("خطا در حذف گروه دسترسی ", false);
            }
        }

        public async Task<AllFactorAccessGroupsDto> Get(Guid id)
        {
            try
            {
                return await FactorAccessGroupAutoMapper.EntityToDtoAllAccessGroup(
                    await _unitOfWork.FactorAccessGroupRepository.Get(id), _errorLoggerService);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new AllFactorAccessGroupsDto();
            }
        }

        public async Task<List<FactorAccessGroupDto>> GetAll()
        {
            try
            {
                return await FactorAccessGroupAutoMapper.EntitiesToDtos(
                            await _unitOfWork.FactorAccessGroupRepository.GetAll(), _errorLoggerService);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<FactorAccessGroupDto>();
            }
        }

        public async Task<(string message, bool isSuccess)> Update(AllFactorAccessGroupsDto factorAccessGroup)
        {
            try
            {
                await _unitOfWork.FactorAccessGroupRepository.DeleteFactorTypes(factorAccessGroup.FactorAccessGroupDto.Id);
                await _unitOfWork.FactorAccessGroupRepository.DeleteGroups(factorAccessGroup.FactorAccessGroupDto.Id);
                await _unitOfWork.FactorAccessGroupRepository.DeleteOrganizationUnits(factorAccessGroup.FactorAccessGroupDto.Id);
                await _unitOfWork.FactorAccessGroupRepository.DeleteRoles(factorAccessGroup.FactorAccessGroupDto.Id);
                await _unitOfWork.FactorAccessGroupRepository.DeleteUsers(factorAccessGroup.FactorAccessGroupDto.Id);
               
                await _unitOfWork.FactorAccessGroupRepository.DeletePermmisions(factorAccessGroup.FactorAccessGroupDto.Id);
                await _unitOfWork.FactorAccessGroupRepository.DeleteProperties(factorAccessGroup.FactorAccessGroupDto.Id);
                await _unitOfWork.Save();

                if (factorAccessGroup.FactorAccessGroupFactorType != null)
                    await AddAccessGroupFactorTypes(factorAccessGroup.FactorAccessGroupFactorType, factorAccessGroup.FactorAccessGroupDto.Id);

                if (factorAccessGroup.FactorAccessGroupGroups != null)
                    await AddAccessGroupGroups(factorAccessGroup.FactorAccessGroupGroups, factorAccessGroup.FactorAccessGroupDto.Id);

                if (factorAccessGroup.FactorAccessGroupsOrganizationUnits != null)
                    await AddAccessGroupOrganizationUnits(factorAccessGroup.FactorAccessGroupsOrganizationUnits, factorAccessGroup.FactorAccessGroupDto.Id);

                if (factorAccessGroup.FactorAccessGroupRoleOfOrganizations != null)
                    await AddAccessGroupRoleOfOrganizations(factorAccessGroup.FactorAccessGroupRoleOfOrganizations, factorAccessGroup.FactorAccessGroupDto.Id);

                if (factorAccessGroup.FactorAccessGroupUsers != null)
                    await AddAccessGroupUsers(factorAccessGroup.FactorAccessGroupUsers, factorAccessGroup.FactorAccessGroupDto.Id);

                
                if (factorAccessGroup.FactorAccessGroupPermissions != null)
                    await AddAccessGroupPermissions(factorAccessGroup.FactorAccessGroupPermissions, factorAccessGroup.FactorAccessGroupDto.Id);

                if (factorAccessGroup.FactorAccessGroupProperties != null)
                    await AddAccessGroupProperties(factorAccessGroup.FactorAccessGroupProperties, factorAccessGroup.FactorAccessGroupDto.Id);

                var result = await _unitOfWork.FactorAccessGroupRepository.Update(
                    await FactorAccessGroupAutoMapper.DtoToEntity(factorAccessGroup, _errorLoggerService));
                await _unitOfWork.Save();
                if (!result)
                {
                    return ("گروه دسترسی مد نظر یافت نشد", false);
                }
                return ("گروه دسترسی با موفقیت بروزرسانی شد", true);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("خطا در بروز رسانی گروه دسترسی", false);
            }
        }

        public async Task<UserAccessOnEditAndDelete> UserAccessOnEditAndDelete(Guid userId, Guid partEntityId)
        {
            try
            {

                var FactorEntity = await _unitOfWork.FactorRepository.Get(partEntityId);

                var DeleteAndEditAccess = new UserAccessOnEditAndDelete();

                var EntitiesIdsForDelete = await _filterService.GetAccessGroupIds(userId, 0, 4);
                var EntitiesIdsForEdit = await _filterService.GetAccessGroupIds(userId, 0, 3);
                var factorTypeIdsDelete = await _unitOfWork.FactorAccessGroupRepository.GetFactorTypeUserHaveAccess(EntitiesIdsForDelete.accessGroupForFactorType);
                var RoleOfOrganizationIdsDelete = await _unitOfWork.FactorAccessGroupRepository.GetRoleOfOrganizationUserHaveAccess(EntitiesIdsForDelete.accessGroupForRoleOfOrganization);
                var OrganizationIdsDelete = await _unitOfWork.FactorAccessGroupRepository.GetOrganizationUserHaveAccess(EntitiesIdsForDelete.accessGroupForOrganizationalunit);
                var factorTypeIdsEdit = await _unitOfWork.FactorAccessGroupRepository.GetFactorTypeUserHaveAccess(EntitiesIdsForEdit.accessGroupForFactorType);
                var RoleOfOrganizationIdsEdit = await _unitOfWork.FactorAccessGroupRepository.GetRoleOfOrganizationUserHaveAccess(EntitiesIdsForEdit.accessGroupForRoleOfOrganization);
                var OrganizationIdsEdit = await _unitOfWork.FactorAccessGroupRepository.GetOrganizationUserHaveAccess(EntitiesIdsForEdit.accessGroupForOrganizationalunit);
                if (
                    factorTypeIdsDelete.Any(x => x == FactorEntity.FactorTypeId) &&
                    RoleOfOrganizationIdsDelete.Any(x => x == FactorEntity.RoleOfOrganizationId) &&
                    OrganizationIdsDelete.Any(x => x == FactorEntity.OrganizationalunitId)
                    )
                {
                    DeleteAndEditAccess.AccessToDelete = true;
                }
                if (
                    factorTypeIdsEdit.Any(x => x == FactorEntity.FactorTypeId) &&
                    RoleOfOrganizationIdsEdit.Any(x => x == FactorEntity.RoleOfOrganizationId) &&
                    OrganizationIdsEdit.Any(x => x == FactorEntity.OrganizationalunitId)
                    )
                {
                    DeleteAndEditAccess.AccessToEdit = true;
                }
                return DeleteAndEditAccess;
            }
            catch (Exception ex)
            {

                return new UserAccessOnEditAndDelete();
            }
        }

       
    }
}
