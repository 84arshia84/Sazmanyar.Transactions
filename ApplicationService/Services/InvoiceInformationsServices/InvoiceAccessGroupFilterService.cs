using AppCore.Entities.ContractsInformation.Contratcs;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroupProperties;
using AppCore.Entities.InvoiceInformations.InvoiceBaseInformations;
using AppCore.Entities.InvoicesInformations.InvoiceType;
using AppCore.Entities.SettingEntities.ContractTypes;
using AppCore.Entities.SettingEntities.Organizationalunits;
using AppCore.Entities.SettingEntities.RoleOfOrganizations;
using AppCore.Enums;
using AppCore.UnitOfWork;
using ApplicationService.ServicesContract.ExceptionHandling;
using ApplicationService.ServicesContract.InvoiceInformations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvoiceType = AppCore.Entities.InvoicesInformations.InvoiceType.InvoiceType;

namespace ApplicationService.Services.InvoiceInformationsServices
{
    internal class InvoiceAccessGroupFilterService : IInvoiceAccessGroupFilterService
    {
        private IUnitOfWork _unitOfWork;
        private IErrorLoggerService _errorLoggerService;
        public InvoiceAccessGroupFilterService(
               IUnitOfWork unitOfWork,
               IErrorLoggerService errorLoggerService
               )
        {
            _unitOfWork = unitOfWork;
            _errorLoggerService = errorLoggerService;
        }
        public async Task<List<Contract>> FilterContractsInInvoice(List<Contract> contracts, Guid userId)
        {
            try
            {
                var AccessGroupsId = await GetAccessGroupIds(userId, (SystemParts)4, (AccessGroupProperties)0, 1);
                var contractTypeIds = await _unitOfWork.InvoiceAccessGroupRepository.GetContractTypeUserHaveAccess(AccessGroupsId.accessGroupForContractType);
                var organUnitIds = await _unitOfWork.InvoiceAccessGroupRepository.GetOrganizationUserHaveAccess(AccessGroupsId.accessGroupForOrganizationUnit);
                var roleOfOrganIds = await _unitOfWork.InvoiceAccessGroupRepository.GetRoleOfOrganizationUserHaveAccess(AccessGroupsId.accessGroupForRoleOfOrganization);

                var accessToContractIds = await _unitOfWork.ContractAccessGroupRepository.GetContractsUserHaveAccess(contractTypeIds, organUnitIds, roleOfOrganIds);
                if (accessToContractIds.Any())
                {
                    return contracts.Where(x => accessToContractIds.Contains(x.ID)).ToList();
                }
                return null;
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return null;
            }
        }
        public async Task<List<InvoiceType>> FilterInvoiceType(List<InvoiceType> invoiceType, Guid userId, SystemParts part, AccessGroupProperties property, int mode)
        {
            try
            {
                var AccessGroupsId = await GetAccessGroupIds(userId, part, property, mode);
                var invoiceTypeIds = await _unitOfWork.InvoiceAccessGroupRepository.GetInvoiceTypeserHaveAccess(AccessGroupsId.invoiceType);
                if (invoiceTypeIds.Any())
                {
                    return invoiceType.Where(x => invoiceTypeIds.Contains(x.Id)).ToList();
                }
                return null;
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return null;
            }
        }
        public async Task<List<InvoiceBaseInformation>> FilterInvoice(List<InvoiceBaseInformation> invoice, List<Guid> FlowIds, Guid userId)
        {
            try
            {
                var AccessGroupsId = await GetAccessGroupIds(userId, (SystemParts)4, (AccessGroupProperties)0, 2);
                var contractTypeIds = await _unitOfWork.InvoiceAccessGroupRepository.GetContractTypeUserHaveAccess(AccessGroupsId.accessGroupForContractType);
                var organUnitIds = await _unitOfWork.InvoiceAccessGroupRepository.GetOrganizationUserHaveAccess(AccessGroupsId.accessGroupForOrganizationUnit);
                var roleOfOrganIds = await _unitOfWork.InvoiceAccessGroupRepository.GetRoleOfOrganizationUserHaveAccess(AccessGroupsId.accessGroupForRoleOfOrganization);
                var invoiceTypeIds = await _unitOfWork.InvoiceAccessGroupRepository.GetInvoiceTypeserHaveAccess(AccessGroupsId.invoiceType);
                var accessToInvoiceIds = await _unitOfWork.InvoiceAccessGroupRepository.GetInvoicesUserHaveAccess(contractTypeIds, organUnitIds, roleOfOrganIds, invoiceTypeIds);
                if (accessToInvoiceIds.Any())
                {
                    FlowIds.AddRange(accessToInvoiceIds);
                    return invoice.Where(x => FlowIds.Contains(x.Id)).ToList();
                }
                if (FlowIds.Any())
                {
                    return invoice.Where(x => FlowIds.Contains(x.Id)).ToList();
                }
                return new List<InvoiceBaseInformation>();
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<InvoiceBaseInformation>();
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
        public async Task<(List<Guid> accessGroupForContractType, List<Guid> accessGroupForOrganizationUnit, List<Guid> accessGroupForRoleOfOrganization, List<Guid> invoiceType)> GetAccessGroupIds(Guid userId, SystemParts part, AccessGroupProperties property, int mode)
        {
            try
            {
                var accessContractTypeIds = new List<Guid>();
                var accessOrganizationIds = new List<Guid>();
                var accessRoleOfOrganization = new List<Guid>();
                var accessInvoicetypeIds = new List<Guid>();
                var accessSave = new List<InvoiceAccessGroupProperties>();
                var accessView = new List<InvoiceAccessGroupProperties>();
                var accessEdit = new List<InvoiceAccessGroupProperties>();
                var accessDelete = new List<InvoiceAccessGroupProperties>();
                switch (mode)
                {
                    case 1:
                        accessSave = await _unitOfWork.InvoiceAccessGroupRepository.GetUserSaveAccess(userId);
                        break;
                    case 2:
                        accessView = await _unitOfWork.InvoiceAccessGroupRepository.GetUserViewAccess(userId);
                        break;
                    case 3:
                        accessEdit = await _unitOfWork.InvoiceAccessGroupRepository.GetUserEditAccess(userId);
                        break;
                    case 4:
                        accessDelete = await _unitOfWork.InvoiceAccessGroupRepository.GetUserDeleteAccess(userId);
                        break;
                }
                if (accessSave.Count > 0)
                {
                    for (int i = 0; i < accessSave.Count; i++)
                    {
                        if (property == AccessGroupProperties.SaveContractType || !Enum.IsDefined(typeof(AccessGroupProperties), property))
                        {
                            if (accessSave[i].ContractTypeSave)
                            {
                                accessContractTypeIds.Add(accessSave[i].InvoiceAccessGroupId);
                            }
                        }
                        if (property == AccessGroupProperties.SaveOrganizationUnit || !Enum.IsDefined(typeof(AccessGroupProperties), property))
                        {
                            if (accessSave[i].OrganizationUnitSave)
                            {
                                accessOrganizationIds.Add(accessSave[i].InvoiceAccessGroupId);
                            }
                        }
                        if (property == AccessGroupProperties.SaveRoleOrganization || !Enum.IsDefined(typeof(AccessGroupProperties), property))
                        {
                            if (accessSave[i].RoleOfOrganizationSave)
                            {
                                accessRoleOfOrganization.Add(accessSave[i].InvoiceAccessGroupId);
                            }
                        }
                        if (property == AccessGroupProperties.SaveInvoiceType || !Enum.IsDefined(typeof(AccessGroupProperties), property))
                        {
                            if (accessSave[i].InvoiceTypeSave)
                            {
                                accessInvoicetypeIds.Add(accessSave[i].InvoiceAccessGroupId);
                            }
                        }
                    }
                }
                if (accessView.Count > 0)
                {
                    for (int i = 0; i < accessView.Count; i++)
                    {
                        if (property == AccessGroupProperties.ViewContractType || !Enum.IsDefined(typeof(AccessGroupProperties), property))
                        {
                            if (accessView[i].ContractTypeView)
                            {
                                accessContractTypeIds.Add(accessView[i].InvoiceAccessGroupId);
                            }
                        }
                        if (property == AccessGroupProperties.ViewOrganizationUnit || !Enum.IsDefined(typeof(AccessGroupProperties), property))
                        {
                            if (accessView[i].OrganizationUnitView)
                            {
                                accessOrganizationIds.Add(accessView[i].InvoiceAccessGroupId);
                            }
                        }
                        if (property == AccessGroupProperties.ViewRoleOrganization || !Enum.IsDefined(typeof(AccessGroupProperties), property))
                        {
                            if (accessView[i].RoleOfOrganizationView)
                            {
                                accessRoleOfOrganization.Add(accessView[i].InvoiceAccessGroupId);
                            }
                        }
                        if (property == AccessGroupProperties.ViewInvoiceType || !Enum.IsDefined(typeof(AccessGroupProperties), property))
                        {
                            if (accessView[i].InvoiceTypeView)
                            {
                                accessInvoicetypeIds.Add(accessView[i].InvoiceAccessGroupId);
                            }
                        }
                    }
                }
                if (accessEdit.Count > 0)
                {
                    for (int i = 0; i < accessEdit.Count; i++)
                    {
                        if (property == AccessGroupProperties.EditContractType || !Enum.IsDefined(typeof(AccessGroupProperties), property))
                        {
                            if (accessEdit[i].ContractTypeEdit)
                            {
                                accessContractTypeIds.Add(accessEdit[i].InvoiceAccessGroupId);
                            }
                        }
                        if (property == AccessGroupProperties.EditOrganizationUnit || !Enum.IsDefined(typeof(AccessGroupProperties), property))
                        {
                            if (accessEdit[i].OrganizationUnitEdit)
                            {
                                accessOrganizationIds.Add(accessEdit[i].InvoiceAccessGroupId);
                            }
                        }
                        if (property == AccessGroupProperties.EditRoleOrganization || !Enum.IsDefined(typeof(AccessGroupProperties), property))
                        {
                            if (accessEdit[i].RoleOfOrganizationEdit)
                            {
                                accessRoleOfOrganization.Add(accessEdit[i].InvoiceAccessGroupId);
                            }
                        }
                        if (property == AccessGroupProperties.EditInvoiceType || !Enum.IsDefined(typeof(AccessGroupProperties), property))
                        {
                            if (accessEdit[i].InvoiceTypeEdit)
                            {
                                accessInvoicetypeIds.Add(accessEdit[i].InvoiceAccessGroupId);
                            }
                        }
                    }
                }
                if (accessDelete.Count > 0)
                {
                    for (int i = 0; i < accessDelete.Count; i++)
                    {
                        if (property == AccessGroupProperties.DeleteContractType || !Enum.IsDefined(typeof(AccessGroupProperties), property))
                        {
                            if (accessDelete[i].ContractTypeDelete)
                            {
                                accessContractTypeIds.Add(accessDelete[i].InvoiceAccessGroupId);
                            }
                        }
                        if (property == AccessGroupProperties.DeleteOrganizationUnit || !Enum.IsDefined(typeof(AccessGroupProperties), property))
                        {
                            if (accessDelete[i].OrganizationUnitDelete)
                            {
                                accessOrganizationIds.Add(accessDelete[i].InvoiceAccessGroupId);
                            }
                        }
                        if (property == AccessGroupProperties.DeleteRoleOrganization || !Enum.IsDefined(typeof(AccessGroupProperties), property))
                        {
                            if (accessDelete[i].RoleOfOrganizationDelete)
                            {
                                accessRoleOfOrganization.Add(accessDelete[i].InvoiceAccessGroupId);
                            }
                        }
                        if (property == AccessGroupProperties.DeleteInvoiceType || !Enum.IsDefined(typeof(AccessGroupProperties), property))
                        {
                            if (accessDelete[i].InvoiceTypeDelete)
                            {
                                accessInvoicetypeIds.Add(accessDelete[i].InvoiceAccessGroupId);
                            }
                        }
                    }
                }
                accessContractTypeIds = accessContractTypeIds.Distinct().ToList();
                accessOrganizationIds = accessOrganizationIds.Distinct().ToList();
                accessRoleOfOrganization = accessRoleOfOrganization.Distinct().ToList();

                return (accessContractTypeIds, accessOrganizationIds, accessRoleOfOrganization, accessInvoicetypeIds);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return (null, null, null, null);
            }
        }
    }
}
