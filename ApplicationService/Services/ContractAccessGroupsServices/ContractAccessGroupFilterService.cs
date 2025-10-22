using AppCore.Entities.ContractAccessGroups;
using AppCore.Entities.ContractsInformation.ContractAddendums;
using AppCore.Entities.ContractsInformation.Contratcs;
using AppCore.Entities.ContractsInformation.TransactionExecutionRequests;
using AppCore.Entities.SettingEntities.ContractTypes;
using AppCore.Entities.SettingEntities.Organizationalunits;
using AppCore.Entities.SettingEntities.RoleOfOrganizations;
using AppCore.Enums;
using AppCore.UnitOfWork;
using ApplicationService.ServicesContract.ContractAccessGroups;
using ApplicationService.ServicesContract.ExceptionHandling;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Services.ContractAccessGroupsServices
{
    public class ContractAccessGroupFilterService : IContractAccessGroupFilterService
    {
        private IUnitOfWork _unitOfWork;
        private IErrorLoggerService _errorLoggerService;
        public ContractAccessGroupFilterService(
               IUnitOfWork unitOfWork,
               IErrorLoggerService errorLoggerService
               )
        {
            _unitOfWork = unitOfWork;
            _errorLoggerService = errorLoggerService;
        }
        public async Task<List<ContractAddendum>> FilterContractAddendums(List<ContractAddendum> contractAddendams, List<Guid> FlowIds, Guid userId)
        {
            try
            {
                var AccessGroupsId = await GetAccessGroupIds(userId, (SystemParts)3, (AccessGroupProperties)0, 2);
                var contractTypeIds = await _unitOfWork.ContractAccessGroupRepository.GetContractTypeUserHaveAccess(AccessGroupsId.accessGroupForContractType);
                var organUnitIds = await _unitOfWork.ContractAccessGroupRepository.GetOrganizationUserHaveAccess(AccessGroupsId.accessGroupForOrganizationUnit);
                var roleOfOrganIds = await _unitOfWork.ContractAccessGroupRepository.GetRoleOfOrganizationUserHaveAccess(AccessGroupsId.accessGroupForRoleOfOrganization);
                var accessToAddendumIds = await _unitOfWork.ContractAccessGroupRepository.GetContractAddendumsUserHaveAccess(contractTypeIds, organUnitIds, roleOfOrganIds);
                if (accessToAddendumIds.Any())
                {
                    if (FlowIds != null)
                    {
                        FlowIds.AddRange(accessToAddendumIds);

                    }
                    else
                    {
                        FlowIds = new List<Guid>();
                        FlowIds.AddRange(accessToAddendumIds);
                    }
                    return contractAddendams.Where(x => FlowIds.Contains(x.Id)).ToList();
                }
                if (FlowIds != null)
                {
                    if (FlowIds.Any())
                    {
                        return contractAddendams.Where(x => FlowIds.Contains(x.Id)).ToList();
                    }
                }
                return new List<ContractAddendum>();

            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<ContractAddendum>();
            }
        }

        public async Task<List<Contract>> FilterContracts(List<Contract> contracts, List<Guid> FlowIds, Guid userId)
        {
            try
            {
                var AccessGroupsId = await GetAccessGroupIds(userId, (SystemParts)2, (AccessGroupProperties)0, 2);
                var contractTypeIds = await _unitOfWork.ContractAccessGroupRepository.GetContractTypeUserHaveAccess(AccessGroupsId.accessGroupForContractType);
                var organUnitIds = await _unitOfWork.ContractAccessGroupRepository.GetOrganizationUserHaveAccess(AccessGroupsId.accessGroupForOrganizationUnit);
                var roleOfOrganIds = await _unitOfWork.ContractAccessGroupRepository.GetRoleOfOrganizationUserHaveAccess(AccessGroupsId.accessGroupForRoleOfOrganization);

                var accessToContractIds = await _unitOfWork.ContractAccessGroupRepository.GetContractsUserHaveAccess(contractTypeIds, organUnitIds, roleOfOrganIds);
                if (accessToContractIds.Any())
                {
                    if (FlowIds != null)
                    {
                        FlowIds.AddRange(accessToContractIds);
                    }
                    else
                    {
                        FlowIds = new List<Guid>();
                        FlowIds.AddRange(accessToContractIds);
                    }
                    return contracts.Where(x => FlowIds.Contains(x.ID)).ToList();
                }
                if (FlowIds != null)
                {
                    if (FlowIds.Any())
                    {
                        return contracts.Where(x => FlowIds.Contains(x.ID)).ToList();
                    }
                }

                return new List<Contract>();
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<Contract>();
            }
        }
        public async Task<List<TransactionExecutionRequest>> FilterTransActionExecutionRequest(List<TransactionExecutionRequest> transaction, List<Guid> FlowIds, Guid userId)
        {
            try
            {
                var AccessGroupsId = await GetAccessGroupIds(userId, (SystemParts)1, (AccessGroupProperties)0, 2);
                var contractTypeIds = await _unitOfWork.ContractAccessGroupRepository.GetContractTypeUserHaveAccess(AccessGroupsId.accessGroupForContractType);
                var organUnitIds = await _unitOfWork.ContractAccessGroupRepository.GetOrganizationUserHaveAccess(AccessGroupsId.accessGroupForOrganizationUnit);
                var accessTotransactionIds = await _unitOfWork.ContractAccessGroupRepository.GetTransactionsUserHaveAccess(contractTypeIds, organUnitIds);
                if (accessTotransactionIds.Any())
                {
                    if (FlowIds != null)
                    {
                        FlowIds.AddRange(accessTotransactionIds);
                    }
                    else
                    {
                        FlowIds = new List<Guid>();
                        FlowIds.AddRange(accessTotransactionIds);
                    }
                    return transaction.Where(x => FlowIds.Contains(x.Id)).ToList();
                }
                if (FlowIds != null)
                {
                    if (FlowIds.Any())
                    {
                        return transaction.Where(x => FlowIds.Contains(x.Id)).ToList();
                    }
                }

                return new List<TransactionExecutionRequest>();
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<TransactionExecutionRequest>();
            }
        }

        public async Task<List<ContractType>> FilterContractType(List<ContractType> contractTypes, Guid userId, SystemParts part, AccessGroupProperties property, int mode)
        {
            try
            {
                var AccessGroupsId = await GetAccessGroupIds(userId, part, property, mode);
                var contractTypeIds = await _unitOfWork.ContractAccessGroupRepository.GetContractTypeUserHaveAccess(AccessGroupsId.accessGroupForContractType);
                if (contractTypeIds.Any())
                {
                    return contractTypes.Where(x => contractTypeIds.Contains(x.ID)).ToList();
                }
                return null;
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return null;
            }
        }

        public async Task<List<Organizationalunit>> FilterOrganizationalunit(List<Organizationalunit> organizationalunit, Guid userId, SystemParts part, AccessGroupProperties property, int mode)
        {
            try
            {
                var AccessGroupsId = await GetAccessGroupIds(userId, part, property, mode);
                var organUnitIds = await _unitOfWork.ContractAccessGroupRepository.GetOrganizationUserHaveAccess(AccessGroupsId.accessGroupForOrganizationUnit);
                if (organUnitIds.Any())
                {
                    return organizationalunit.Where(x => organUnitIds.Contains(x.ID)).ToList();
                }
                return null;

            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return null;
            }
        }

        public async Task<List<RoleOfOrganization>> FilterRoleOfOrganization(List<RoleOfOrganization> roleOfOrganization, Guid userId, SystemParts part, AccessGroupProperties property, int mode)
        {
            try
            {
                var AccessGroupsId = await GetAccessGroupIds(userId, part, property, mode);
                var roleOfOrganIds = await _unitOfWork.ContractAccessGroupRepository.GetRoleOfOrganizationUserHaveAccess(AccessGroupsId.accessGroupForRoleOfOrganization);
                if (roleOfOrganIds.Any())
                {
                    return roleOfOrganization.Where(x => roleOfOrganIds.Contains(x.ID)).ToList();
                }
                return null;
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return null;
            }
        }



        /// <summary>
        /// mode
        /// 1- save
        /// 2- view
        /// 3- edit
        /// 4- delete
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="part"></param>
        /// <param name="property"></param>
        /// <param name="mode">
        /// </param>
        /// <returns></returns>
        public async Task<(List<Guid> accessGroupForContractType, List<Guid> accessGroupForOrganizationUnit, List<Guid> accessGroupForRoleOfOrganization)> GetAccessGroupIds(Guid userId, SystemParts part, AccessGroupProperties property, int mode)
        {
            try
            {
                var accessContractTypeIds = new List<Guid>();
                var accessOrganizationIds = new List<Guid>();
                var accessRoleOfOrganization = new List<Guid>();
                (List<ContractAccessGroupSystemParts> systemParts, List<ContractAccessGroupProperties> properties) accessSave = (new List<ContractAccessGroupSystemParts>(), new List<ContractAccessGroupProperties>());
                (List<ContractAccessGroupSystemParts> systemParts, List<ContractAccessGroupProperties> properties) accessView = (new List<ContractAccessGroupSystemParts>(), new List<ContractAccessGroupProperties>());
                (List<ContractAccessGroupSystemParts> systemParts, List<ContractAccessGroupProperties> properties) accessEdit = (new List<ContractAccessGroupSystemParts>(), new List<ContractAccessGroupProperties>());
                (List<ContractAccessGroupSystemParts> systemParts, List<ContractAccessGroupProperties> properties) accessDelete = (new List<ContractAccessGroupSystemParts>(), new List<ContractAccessGroupProperties>());
                switch (mode)
                {
                    case 1:
                        accessSave = await _unitOfWork.ContractAccessGroupRepository.GetUserSaveAccess(userId);
                        break;
                    case 2:
                        accessView = await _unitOfWork.ContractAccessGroupRepository.GetUserViewAccess(userId);
                        break;
                    case 3:
                        accessEdit = await _unitOfWork.ContractAccessGroupRepository.GetUserEditAccess(userId);
                        break;
                    case 4:
                        accessDelete = await _unitOfWork.ContractAccessGroupRepository.GetUserDeleteAccess(userId);
                        break;
                }
                if (accessSave.properties.Count > 0 || accessSave.systemParts.Count > 0)
                {
                    for (int i = 0; i < accessSave.properties.Count; i++)
                    {
                        var ThisSystemPart = accessSave.systemParts.Where(x => x.ContractAccessGroupId == accessSave.properties[i].ContractAccessGroupId).ToList();
                        if (property == AccessGroupProperties.SaveContractType || !Enum.IsDefined(typeof(AccessGroupProperties), property))
                        {
                            if (accessSave.properties[i].ContractTypeSave)
                            {
                                if (ThisSystemPart.Count == 0)
                                {
                                    accessContractTypeIds.Add(accessSave.properties[i].ContractAccessGroupId);
                                }
                                if (ThisSystemPart.Any(x => x.SystemParts == part))
                                {
                                    accessContractTypeIds.Add(accessSave.properties[i].ContractAccessGroupId);
                                }
                            }
                        }
                        if (property == AccessGroupProperties.SaveOrganizationUnit || !Enum.IsDefined(typeof(AccessGroupProperties), property))
                        {
                            if (accessSave.properties[i].OrganizationUnitSave)
                            {
                                if (ThisSystemPart.Count == 0)
                                {
                                    accessOrganizationIds.Add(accessSave.properties[i].ContractAccessGroupId);
                                }
                                if (ThisSystemPart.Any(x => x.SystemParts == part))
                                {
                                    accessOrganizationIds.Add(accessSave.properties[i].ContractAccessGroupId);
                                }
                            }
                        }
                        if (property == AccessGroupProperties.SaveRoleOrganization || !Enum.IsDefined(typeof(AccessGroupProperties), property))
                        {
                            if (accessSave.properties[i].RoleOfOrganizationSave)
                            {
                                if (ThisSystemPart.Count == 0)
                                {
                                    accessRoleOfOrganization.Add(accessSave.properties[i].ContractAccessGroupId);
                                }
                                if (ThisSystemPart.Any(x => x.SystemParts == part))
                                {
                                    accessRoleOfOrganization.Add(accessSave.properties[i].ContractAccessGroupId);
                                }
                            }
                        }

                    }
                }
                if (accessView.properties.Count > 0 || accessView.systemParts.Count > 0)
                {
                    for (int i = 0; i < accessView.properties.Count; i++)
                    {
                        var ThisSystemPart = accessView.systemParts.Where(x => x.ContractAccessGroupId == accessView.properties[i].ContractAccessGroupId).ToList();
                        if (property == AccessGroupProperties.ViewContractType || !Enum.IsDefined(typeof(AccessGroupProperties), property))
                        {
                            if (accessView.properties[i].ContractTypeView)
                            {
                                if (ThisSystemPart.Count == 0)
                                {
                                    accessContractTypeIds.Add(accessView.properties[i].ContractAccessGroupId);
                                }
                                if (ThisSystemPart.Any(x => x.SystemParts == part))
                                {
                                    accessContractTypeIds.Add(accessView.properties[i].ContractAccessGroupId);
                                }
                            }
                        }
                        if (property == AccessGroupProperties.ViewOrganizationUnit || !Enum.IsDefined(typeof(AccessGroupProperties), property))
                        {
                            if (accessView.properties[i].OrganizationUnitView)
                            {
                                if (ThisSystemPart.Count == 0)
                                {
                                    accessOrganizationIds.Add(accessView.properties[i].ContractAccessGroupId);
                                }
                                if (ThisSystemPart.Any(x => x.SystemParts == part))
                                {
                                    accessOrganizationIds.Add(accessView.properties[i].ContractAccessGroupId);
                                }
                            }
                        }
                        if (property == AccessGroupProperties.ViewRoleOrganization || !Enum.IsDefined(typeof(AccessGroupProperties), property))
                        {
                            if (accessView.properties[i].RoleOfOrganizationView)
                            {
                                if (ThisSystemPart.Count == 0)
                                {
                                    accessRoleOfOrganization.Add(accessView.properties[i].ContractAccessGroupId);
                                }
                                if (ThisSystemPart.Any(x => x.SystemParts == part))
                                {
                                    accessRoleOfOrganization.Add(accessView.properties[i].ContractAccessGroupId);
                                }
                            }
                        }

                    }
                }
                if (accessEdit.properties.Count > 0 || accessEdit.systemParts.Count > 0)
                {
                    for (int i = 0; i < accessEdit.properties.Count; i++)
                    {
                        var ThisSystemPart = accessView.systemParts.Where(x => x.ContractAccessGroupId == accessEdit.properties[i].ContractAccessGroupId).ToList();
                        if (property == AccessGroupProperties.EditContractType || !Enum.IsDefined(typeof(AccessGroupProperties), property))
                        {
                            if (accessEdit.properties[i].ContractTypeEdit)
                            {
                                if (ThisSystemPart.Count == 0)
                                {
                                    accessContractTypeIds.Add(accessEdit.properties[i].ContractAccessGroupId);
                                }
                                if (ThisSystemPart.Any(x => x.SystemParts == part))
                                {
                                    accessContractTypeIds.Add(accessEdit.properties[i].ContractAccessGroupId);
                                }
                            }
                        }
                        if (property == AccessGroupProperties.EditOrganizationUnit || !Enum.IsDefined(typeof(AccessGroupProperties), property))
                        {
                            if (accessEdit.properties[i].OrganizationUnitEdit)
                            {
                                if (ThisSystemPart.Count == 0)
                                {
                                    accessOrganizationIds.Add(accessEdit.properties[i].ContractAccessGroupId);
                                }
                                if (ThisSystemPart.Any(x => x.SystemParts == part))
                                {
                                    accessOrganizationIds.Add(accessEdit.properties[i].ContractAccessGroupId);
                                }
                            }
                        }
                        if (property == AccessGroupProperties.EditRoleOrganization || !Enum.IsDefined(typeof(AccessGroupProperties), property))
                        {
                            if (accessEdit.properties[i].RoleOfOrganizationEdit)
                            {
                                if (ThisSystemPart.Count == 0)
                                {
                                    accessRoleOfOrganization.Add(accessEdit.properties[i].ContractAccessGroupId);
                                }
                                if (ThisSystemPart.Any(x => x.SystemParts == part))
                                {
                                    accessRoleOfOrganization.Add(accessEdit.properties[i].ContractAccessGroupId);
                                }
                            }
                        }
                    }
                }
                if (accessDelete.properties.Count > 0 || accessDelete.systemParts.Count > 0)
                {
                    for (int i = 0; i < accessDelete.properties.Count; i++)
                    {
                        var ThisSystemPart = accessView.systemParts.Where(x => x.ContractAccessGroupId == accessDelete.properties[i].ContractAccessGroupId).ToList();
                        if (property == AccessGroupProperties.DeleteContractType || !Enum.IsDefined(typeof(AccessGroupProperties), property))
                        {
                            if (accessDelete.properties[i].ContractTypeDelete)
                            {
                                if (ThisSystemPart.Count == 0)
                                {
                                    accessContractTypeIds.Add(accessDelete.properties[i].ContractAccessGroupId);
                                }
                                if (ThisSystemPart.Any(x => x.SystemParts == part))
                                {
                                    accessContractTypeIds.Add(accessDelete.properties[i].ContractAccessGroupId);
                                }
                            }
                        }
                        if (property == AccessGroupProperties.DeleteOrganizationUnit || !Enum.IsDefined(typeof(AccessGroupProperties), property))
                        {
                            if (accessDelete.properties[i].OrganizationUnitDelete)
                            {
                                if (ThisSystemPart.Count == 0)
                                {
                                    accessOrganizationIds.Add(accessDelete.properties[i].ContractAccessGroupId);
                                }
                                if (ThisSystemPart.Any(x => x.SystemParts == part))
                                {
                                    accessOrganizationIds.Add(accessDelete.properties[i].ContractAccessGroupId);
                                }
                            }
                        }
                        if (property == AccessGroupProperties.DeleteRoleOrganization || !Enum.IsDefined(typeof(AccessGroupProperties), property))
                        {
                            if (accessDelete.properties[i].RoleOfOrganizationDelete)
                            {
                                if (ThisSystemPart.Count == 0)
                                {
                                    accessRoleOfOrganization.Add(accessDelete.properties[i].ContractAccessGroupId);
                                }
                                if (ThisSystemPart.Any(x => x.SystemParts == part))
                                {
                                    accessRoleOfOrganization.Add(accessDelete.properties[i].ContractAccessGroupId);
                                }
                            }
                        }
                    }
                }

                accessContractTypeIds = accessContractTypeIds.Distinct().ToList();
                accessOrganizationIds = accessOrganizationIds.Distinct().ToList();
                accessRoleOfOrganization = accessRoleOfOrganization.Distinct().ToList();

                return (accessContractTypeIds, accessOrganizationIds, accessRoleOfOrganization);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return (null, null, null);
            }
        }
    }
}
