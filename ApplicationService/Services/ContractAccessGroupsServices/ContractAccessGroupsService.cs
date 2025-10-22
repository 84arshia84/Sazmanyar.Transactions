using AppCore.Entities.ContractAccessGroups;
using AppCore.Entities.ContractsInformation.ContractAddendums;
using AppCore.Entities.ContractsInformation.Contratcs;
using AppCore.Entities.ContractsInformation.TransactionExecutionRequests;
using AppCore.Enums;
using AppCore.UnitOfWork;
using ApplicationService.DtoModels.ContractAccessGroupsDtos;
using ApplicationService.DtoModels.UserDtos;
using ApplicationService.Mapper.ContractAccessGroupMappers;
using ApplicationService.ServicesContract.ContractAccessGroups;
using ApplicationService.ServicesContract.ExceptionHandling;
using InfraStructure.UnitOfWork;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Services.ContractAccessGroupsServices
{
    internal class ContractAccessGroupsService : IContractAccessGroupsService
    {
        private IUnitOfWork _unitOfWork;
        private IErrorLoggerService _errorLoggerService;
        private IContractAccessGroupFilterService _filterService;
        public ContractAccessGroupsService(
               IUnitOfWork unitOfWork,
               IErrorLoggerService errorLoggerService,
               IContractAccessGroupFilterService filterService
               )
        {
            _unitOfWork = unitOfWork;
            _errorLoggerService = errorLoggerService;
            _filterService = filterService;
        }
        public async Task<(string message, bool isSuccess)> AddAccessGroup(AllAccessGroupsDto contractAccessGroup)
        {
            try
            {
                contractAccessGroup.ContractAccessGroup.Id = Guid.NewGuid();

                var Core = await ContractAccessGroupAutoMapperProfile.DtoToEntity(contractAccessGroup, _errorLoggerService);
                (string, bool) Group;
                (string, bool) Roles;
                (string, bool) Types;
                (string, bool) Units;
                (string, bool) Users;
                (string, bool) Permission;
                (string, bool) Properties;
                (string, bool) SystemParts;

                await _unitOfWork.ContractAccessGroupRepository.AddAccessGroup(Core);

                await _unitOfWork.Save();

                if (contractAccessGroup.ContractAccessGroupGroups != null)
                {
                    Group = await AddAccessGroupGroups(contractAccessGroup.ContractAccessGroupGroups, Core.Id);
                    if (!Group.Item2)
                    {
                        return Group;
                    }
                }
                if (contractAccessGroup.ContractAccessGroupRoleOfOrganizations != null)
                {
                    Roles = await AddAccessGroupRoleOfOrganizations(contractAccessGroup.ContractAccessGroupRoleOfOrganizations, Core.Id);
                    if (!Roles.Item2)
                    {
                        return Roles;
                    }
                }
                if (contractAccessGroup.ContractAccessGroupContractType != null)
                {
                    Types = await AddAccessGroupContractTypes(contractAccessGroup.ContractAccessGroupContractType, Core.Id);
                    if (!Types.Item2)
                    {
                        return Types;
                    }
                }
                if (contractAccessGroup.ContractAccessGroupsOrganizationUnits != null)
                {
                    Units = await AddAccessGroupOrganizationUnits(contractAccessGroup.ContractAccessGroupsOrganizationUnits, Core.Id);
                    if (!Units.Item2)
                    {
                        return Units;
                    }
                }
                if (contractAccessGroup.ContractAccessGroupUsers != null)
                {
                    Users = await AddAccessGroupUsers(contractAccessGroup.ContractAccessGroupUsers, Core.Id);
                    if (!Users.Item2)
                    {
                        return Users;
                    }
                }
                if (contractAccessGroup.ContractAccessGroupPermissions != null)
                {
                    Permission = await AddAccessGroupPermissions(contractAccessGroup.ContractAccessGroupPermissions, Core.Id);
                    if (!Permission.Item2)
                    {
                        return Permission;
                    }
                }
                if (contractAccessGroup.ContractAccessGroupProperties != null)
                {
                    Properties = await AddAccessGroupProperties(contractAccessGroup.ContractAccessGroupProperties, Core.Id);
                    if (!Properties.Item2)
                    {
                        return Properties;
                    }
                }
                if (contractAccessGroup.ContractAccessGroupSystemParts != null)
                {
                    SystemParts = await AddAccessGroupSystemPart(contractAccessGroup.ContractAccessGroupSystemParts, Core.Id);
                    if (!SystemParts.Item2)
                    {
                        return SystemParts;
                    }
                }
                await _unitOfWork.Save();
                return ("افزودن گروه دسترسی با موفقیت انجام شد.", true);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("افزودن گروه دسترسی با خطا مواجه شد.", false);
            }
        }

        public async Task<(string message, bool isSuccess)> AddAccessGroupContractTypes(List<Guid> contractAccessGroupContractTypes, Guid CoreId)
        {
            try
            {
                await _unitOfWork.ContractAccessGroupRepository.AddAccessGroupContractTypes(
                    await ContractAccessGroupContractTypeAutoMapperProfile.DtosToEntities(contractAccessGroupContractTypes, CoreId, _errorLoggerService));
                return ("افزودن نوع معامله موفقیت آمیز بود", true);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("افزودن نوع معاملات با خطا مواجه شد.", false);
            }
        }

        public async Task<(string message, bool isSuccess)> AddAccessGroupGroups(List<Guid> contractAccessGroupGroups, Guid CoreId)
        {
            try
            {
                await _unitOfWork.ContractAccessGroupRepository.AddAccessGroupGroups(
                    await ContractAccessGroupGroupsAutoMapperProfile.DtosToEntities(contractAccessGroupGroups, CoreId, _errorLoggerService));
                return ("افزودن گروه ها موفقیت آمیز بود", true);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("خطا در افزودن گروه ها ", false);
            }
        }

        public async Task<(string message, bool isSuccess)> AddAccessGroupOrganizationUnits(List<Guid> contractAccessGroupOrganizations, Guid CoreId)
        {
            try
            {
                await _unitOfWork.ContractAccessGroupRepository.AddAccessGroupOrganizationUnits(
                    await ContractAccessGroupOrganizationUnitsAutoMapperProfile.DtosToEntities(contractAccessGroupOrganizations, CoreId, _errorLoggerService));
                return ("افزودن واحد ها موفقیت آمیز بود", true);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("خطا در افزودن واحد ها ", false);
            }
        }

        public async Task<(string message, bool isSuccess)> AddAccessGroupPermissions(ContractAccessGroupPermissionsDto contractAccessGroupPermissions, Guid CoreId)
        {
            try
            {
                await _unitOfWork.ContractAccessGroupRepository.AddAccessGroupPermissions(
                    await ContractAccessGroupPermissionsAutoMapperProfile.DtoToEntity(contractAccessGroupPermissions, CoreId, _errorLoggerService));
                return ("افزودن دسترسی های سامانه موفقیت آمیز بود", true);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("خطا در افزودن دسترسی های سامانه  ", false);
            }
        }

        public async Task<(string message, bool isSuccess)> AddAccessGroupProperties(ContractAccessGroupPropertiesDto contractAccessGroupProperties, Guid CoreId)
        {
            try
            {
                await _unitOfWork.ContractAccessGroupRepository.AddAccessGroupProperties(
                    await ContractAccessGroupPropertiesAutoMapperProfile.DtoToEntity(contractAccessGroupProperties, CoreId, _errorLoggerService));
                return ("افزودن جزئیات گروه دسترسی موفقیت آمیز بود", true);

            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("خطا در افزودن جزئیات گروه دسترسی  ", false);
            }
        }

        public async Task<(string message, bool isSuccess)> AddAccessGroupRoleOfOrganizations(List<Guid> contractAccessGroupRoles, Guid CoreId)
        {
            try
            {
                await _unitOfWork.ContractAccessGroupRepository.AddAccessGroupRoleOfOrganizations(
                    await ContractAccessGroupRoleOfOrganizationsAutoMapperProfile.DtosToEntities(contractAccessGroupRoles, CoreId, _errorLoggerService));
                return ("افزودن نقش ها موفقیت آمیز بود", true);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("خطا در افزودن نقش ها ", false);

            }
        }
        /// <summary>
        /// در این قسمت  میام دسترسی هایی که شخص بهش اطلاق شده مثلا ثبت و در کدام بخش سامانه مثلا تب معاملات ، راذخیره می کنیم
        /// </summary>
        /// <param name="contractAccessGroupSystemPartDto"></param>
        /// <param name="CoreId"></param>
        /// <returns></returns>
        public async Task<(string message, bool isSuccess)> AddAccessGroupSystemPart(List<ContractAccessGroupSystemPartDto> contractAccessGroupSystemPartDto, Guid CoreId)
        {
            try
            {
                await _unitOfWork.ContractAccessGroupRepository.AddAccessGroupSystemPart(
                    await ContractAccessGroupSystemPartAutoMapperProfile.DtosToEntities(contractAccessGroupSystemPartDto, CoreId, _errorLoggerService));
                return ("افزودن دسترسی داشتن ها در بخش سامانه موفقیت آمیز بود", true);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("افزودن دسترسی داشتن ها در بخش سامانه با خطا مواجه شد", false);
            }
        }

        public async Task<(string message, bool isSuccess)> AddAccessGroupUsers(List<Guid> contractAccessGroups, Guid CoreId)
        {
            try
            {
                await _unitOfWork.ContractAccessGroupRepository.AddAccessGroupUsers(
                    await ContractAccessGroupUsersAutoMapperProfile.DtosToEntities(contractAccessGroups, CoreId, _errorLoggerService));
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
                var result = await _unitOfWork.ContractAccessGroupRepository.DeleteCore(id);
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

        public async Task<AllAccessGroupsDto> Get(Guid id)
        {
            try
            {
                return await ContractAccessGroupAutoMapperProfile.EntityToDtoAllAccessGroup(
                    await _unitOfWork.ContractAccessGroupRepository.Get(id), _errorLoggerService);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new AllAccessGroupsDto();
            }
        }

        public async Task<List<ContractAccessGroupDto>> GetAll()
        {
            try
            {
                return await ContractAccessGroupAutoMapperProfile.EntitiesToDtos(
                            await _unitOfWork.ContractAccessGroupRepository.GetAll(), _errorLoggerService);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<ContractAccessGroupDto>();
            }
        }



        public async Task<List<ContractAccessGroupDto>> GetContractAccessGroupsById(Guid userId)
        {
            try
            {
                var entities = await _unitOfWork.ContractAccessGroupRepository.GetAccessGroupsByUserId(userId);
                var dtos = await ContractAccessGroupAutoMapperProfile.EntitiesToDtos(entities, _errorLoggerService);
                return dtos;
            }
            catch (Exception ex) 
            {
                _errorLoggerService.SaveError(ex);
                return new List<ContractAccessGroupDto>();
            }
        }

        public async Task<(string message, bool isSuccess)> Update(AllAccessGroupsDto contractAccessGroup)
        {
            try
            {
                await _unitOfWork.ContractAccessGroupRepository.DeleteContractTypes(contractAccessGroup.ContractAccessGroup.Id);
                await _unitOfWork.ContractAccessGroupRepository.DeleteGroups(contractAccessGroup.ContractAccessGroup.Id);
                await _unitOfWork.ContractAccessGroupRepository.DeleteOrganizationUnits(contractAccessGroup.ContractAccessGroup.Id);
                await _unitOfWork.ContractAccessGroupRepository.DeleteRoles(contractAccessGroup.ContractAccessGroup.Id);
                await _unitOfWork.ContractAccessGroupRepository.DeleteUsers(contractAccessGroup.ContractAccessGroup.Id);
                await _unitOfWork.ContractAccessGroupRepository.DeleteSystemParts(contractAccessGroup.ContractAccessGroup.Id);
                await _unitOfWork.ContractAccessGroupRepository.DeletePermmisions(contractAccessGroup.ContractAccessGroup.Id);
                await _unitOfWork.ContractAccessGroupRepository.DeleteProperties(contractAccessGroup.ContractAccessGroup.Id);
                await _unitOfWork.Save();

                if (contractAccessGroup.ContractAccessGroupContractType != null)
                    await AddAccessGroupContractTypes(contractAccessGroup.ContractAccessGroupContractType, contractAccessGroup.ContractAccessGroup.Id);

                if (contractAccessGroup.ContractAccessGroupGroups != null)
                    await AddAccessGroupGroups(contractAccessGroup.ContractAccessGroupGroups, contractAccessGroup.ContractAccessGroup.Id);

                if (contractAccessGroup.ContractAccessGroupsOrganizationUnits != null)
                    await AddAccessGroupOrganizationUnits(contractAccessGroup.ContractAccessGroupsOrganizationUnits, contractAccessGroup.ContractAccessGroup.Id);

                if (contractAccessGroup.ContractAccessGroupRoleOfOrganizations != null)
                    await AddAccessGroupRoleOfOrganizations(contractAccessGroup.ContractAccessGroupRoleOfOrganizations, contractAccessGroup.ContractAccessGroup.Id);

                if (contractAccessGroup.ContractAccessGroupUsers != null)
                    await AddAccessGroupUsers(contractAccessGroup.ContractAccessGroupUsers, contractAccessGroup.ContractAccessGroup.Id);

                if (contractAccessGroup.ContractAccessGroupSystemParts != null)
                    await AddAccessGroupSystemPart(contractAccessGroup.ContractAccessGroupSystemParts, contractAccessGroup.ContractAccessGroup.Id);

                if (contractAccessGroup.ContractAccessGroupPermissions != null)
                    await AddAccessGroupPermissions(contractAccessGroup.ContractAccessGroupPermissions, contractAccessGroup.ContractAccessGroup.Id);

                if (contractAccessGroup.ContractAccessGroupProperties != null)
                    await AddAccessGroupProperties(contractAccessGroup.ContractAccessGroupProperties, contractAccessGroup.ContractAccessGroup.Id);

                var result = await _unitOfWork.ContractAccessGroupRepository.Update(
                    await ContractAccessGroupAutoMapperProfile.DtoToEntity(contractAccessGroup, _errorLoggerService));
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

        public async Task<UserAccessOnEditAndDelete> UserAccessOnEditAndDelete(Guid userId, Guid partEntityId, int part)
        {
            try
            {
                var ContractEntity = new Contract();
                var ContractAddendumEntity = new ContractAddendum();
                var TransactionExecutionRequest = new TransactionExecutionRequest();
                var DeleteAndEditAccess = new UserAccessOnEditAndDelete();
                if ((SystemParts)part == SystemParts.Contract)
                {
                    ContractEntity = await _unitOfWork.ContractRepository.Get(partEntityId);
                }
                if ((SystemParts)part == SystemParts.ContractAddendum)
                {
                    ContractAddendumEntity = await _unitOfWork.ContractAddendumRepository.GetByContract(partEntityId);
                }
                if((SystemParts)part == SystemParts.TransactionExecutionRequest)
                {
                    TransactionExecutionRequest = await _unitOfWork.TransactionExecutionRequestRepository.Get(partEntityId);
                }
                var ContractTypeEntity = Guid.Empty;
                var OrganizationEntity = Guid.Empty;
                var RoleOfOrganizationEntity = Guid.Empty;
                if (ContractEntity.ID != Guid.Empty)
                {
                    ContractTypeEntity = ContractEntity.ContractTypeID;
                    OrganizationEntity = ContractEntity.OrganizationUnitID;
                    RoleOfOrganizationEntity = ContractEntity.RoleOFOrganizationID;
                }
                if (ContractAddendumEntity.Id != Guid.Empty)
                {
                    ContractTypeEntity = ContractAddendumEntity.Contract.ContractTypeID;
                    OrganizationEntity = ContractAddendumEntity.Contract.OrganizationUnitID;
                    RoleOfOrganizationEntity = ContractAddendumEntity.Contract.RoleOFOrganizationID;
                }
                if(TransactionExecutionRequest.Id != Guid.Empty)
                {
                    ContractTypeEntity = TransactionExecutionRequest.ContractTypeId;
                    OrganizationEntity = TransactionExecutionRequest.OrganizationId;
                }
                var EntitiesIdsForDelete = await _filterService.GetAccessGroupIds(userId, (SystemParts)part, 0, 4);
                var EntitiesIdsForEdit = await _filterService.GetAccessGroupIds(userId, (SystemParts)part, 0, 3);
                var contractTypeIdsDelete = await _unitOfWork.ContractAccessGroupRepository.GetContractTypeUserHaveAccess(EntitiesIdsForDelete.accessGroupForContractType);
                var organUnitIdsDelete = await _unitOfWork.ContractAccessGroupRepository.GetOrganizationUserHaveAccess(EntitiesIdsForDelete.accessGroupForOrganizationUnit);
                var roleOfOrganIdsDelete = await _unitOfWork.ContractAccessGroupRepository.GetRoleOfOrganizationUserHaveAccess(EntitiesIdsForDelete.accessGroupForRoleOfOrganization);
                var contractTypeIdsEdit = await _unitOfWork.ContractAccessGroupRepository.GetContractTypeUserHaveAccess(EntitiesIdsForEdit.accessGroupForContractType);
                var organUnitIdsEdit = await _unitOfWork.ContractAccessGroupRepository.GetOrganizationUserHaveAccess(EntitiesIdsForEdit.accessGroupForOrganizationUnit);
                var roleOfOrganIdsEdit = await _unitOfWork.ContractAccessGroupRepository.GetRoleOfOrganizationUserHaveAccess(EntitiesIdsForEdit.accessGroupForRoleOfOrganization);
                if (
                    contractTypeIdsDelete.Any(x => x == ContractTypeEntity) &&
                    organUnitIdsDelete.Any(x => x == OrganizationEntity) &&
                    roleOfOrganIdsDelete.Any(x => x == RoleOfOrganizationEntity)
                    )
                {
                    DeleteAndEditAccess.AccessToDelete = true;
                }
                if (
                    contractTypeIdsEdit.Any(x => x == ContractTypeEntity) &&
                    organUnitIdsEdit.Any(x => x == OrganizationEntity) &&
                    roleOfOrganIdsEdit.Any(x => x == RoleOfOrganizationEntity)
                    )
                {
                    DeleteAndEditAccess.AccessToEdit = true;
                }
                return DeleteAndEditAccess;
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new UserAccessOnEditAndDelete();
            }
        }
    }
}
