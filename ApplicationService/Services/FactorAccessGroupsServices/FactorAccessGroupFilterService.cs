using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Entities.FactorAccessGroup;
using AppCore.Entities.FactorInformation.Factors;
using AppCore.Entities.SettingEntities.FactorTypes;
using AppCore.Entities.SettingEntities.Organizationalunits;
using AppCore.Entities.SettingEntities.RoleOfOrganizations;
using AppCore.Enums;
using AppCore.UnitOfWork;
using ApplicationService.ServicesContract.ExceptionHandling;
using ApplicationService.ServicesContract.FactorAccessGroups;

namespace ApplicationService.Services.FactorAccessGroupsServices
{
    public class FactorAccessGroupFilterService : IFactorAccessGroupFilterService
    {
        private IUnitOfWork _unitOfWork;
        private IErrorLoggerService _errorLoggerService;
        public FactorAccessGroupFilterService(IUnitOfWork unitOfWork, IErrorLoggerService errorLoggerService)
        {
            _unitOfWork = unitOfWork;
            _errorLoggerService = errorLoggerService;
        }
        public async Task<List<Factor>> FilterFactor(List<Factor> factors, List<Guid> FlowIds, Guid userId)
        {
            try
            {
                var AccessGroupsId = await GetAccessGroupIds(userId, (EnumFactorAccessGroupProperties)0, 2);
                var factorTypesIds = await _unitOfWork.FactorAccessGroupRepository.GetFactorTypeUserHaveAccess(AccessGroupsId.accessGroupForFactorType);
                var OrganizationsIds = await _unitOfWork.FactorAccessGroupRepository.GetOrganizationUserHaveAccess(AccessGroupsId.accessGroupForOrganizationalunit);
                var RoleOfOrganizationsIds = await _unitOfWork.FactorAccessGroupRepository.GetRoleOfOrganizationUserHaveAccess(AccessGroupsId.accessGroupForRoleOfOrganization);

                var accessTofactorIds = await _unitOfWork.FactorAccessGroupRepository.GetFactorUserHaveAccess(factorTypesIds, OrganizationsIds, RoleOfOrganizationsIds);
                if (accessTofactorIds.Any())
                {
                    FlowIds.AddRange(accessTofactorIds);
                    return factors.Where(x => FlowIds.Contains(x.Id)).ToList();
                }
                if (FlowIds.Any())
                {
                    return factors.Where(x => FlowIds.Contains(x.Id)).ToList();
                }
                return null;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public async Task<List<FactorType>> FilterFactorType(List<FactorType> factors, Guid userId, EnumFactorAccessGroupProperties property, int mode)
        {
            try
            {
                var AccessGroupsId = await GetAccessGroupIds(userId, property, mode);
                var factortypeIds = await _unitOfWork.FactorAccessGroupRepository.GetFactorTypeUserHaveAccess(AccessGroupsId.accessGroupForFactorType);
                if (factortypeIds.Any())
                {
                    return factors.Where(x => factortypeIds.Contains(x.ID)).ToList();
                }
                return null;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public async Task<List<Organizationalunit>> FilterOrganizationalunit(List<Organizationalunit> organizationalunits, Guid userId, EnumFactorAccessGroupProperties property, int mode)
        {
            try
            {
                var AccessGroupsId = await GetAccessGroupIds(userId, property, mode);
                var organUnitIds = await _unitOfWork.FactorAccessGroupRepository.GetOrganizationUserHaveAccess(AccessGroupsId.accessGroupForOrganizationalunit);
                if (organUnitIds.Any())
                {
                    return organizationalunits.Where(x => organUnitIds.Contains(x.ID)).ToList();
                }
                return null;

            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return null;
            }
        }

        public async Task<List<RoleOfOrganization>> FilterRoleOfOrganization(List<RoleOfOrganization> roleOfOrganizations, Guid userId, EnumFactorAccessGroupProperties property, int mode)
        {
            try
            {
                var AccessGroupsId = await GetAccessGroupIds(userId, property, mode);
                var roleOfOrganIds = await _unitOfWork.FactorAccessGroupRepository.GetRoleOfOrganizationUserHaveAccess(AccessGroupsId.accessGroupForRoleOfOrganization);
                if (roleOfOrganIds.Any())
                {
                    return roleOfOrganizations.Where(x => roleOfOrganIds.Contains(x.ID)).ToList();
                }
                return null;
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return null;
            }
        }

        public async Task<(List<Guid> accessGroupForFactorType, List<Guid> accessGroupForOrganizationalunit, List<Guid> accessGroupForRoleOfOrganization)> GetAccessGroupIds(Guid userId, EnumFactorAccessGroupProperties property, int mode)
        {
            try
            {
                var accessfactorTypeIds = new List<Guid>();
                var accessorganizationIds = new List<Guid>();
                var accessroleoforganIds = new List<Guid>();

                var accessSave = new List<FactorAccessGroupProperties>();
                var accessView = new List<FactorAccessGroupProperties>();
                var accessEdit = new List<FactorAccessGroupProperties>();
                var accessDelete = new List<FactorAccessGroupProperties>();
                if (mode == 1)
                {
                    accessSave = await _unitOfWork.FactorAccessGroupRepository.GetUserSaveAccess(userId);
                }
                if (mode == 2)
                {
                    accessView = await _unitOfWork.FactorAccessGroupRepository.GetUserViewAccess(userId);
                }
                if (mode == 3)
                {
                    accessEdit = await _unitOfWork.FactorAccessGroupRepository.GetUserEditAccess(userId);
                }
                if (mode == 4)
                {
                    accessDelete = await _unitOfWork.FactorAccessGroupRepository.GetUserDeleteAccess(userId);
                }
                if (accessSave.Count > 0)
                {
                    for (int i = 0; i < accessSave.Count; i++)
                    {
                        if (property == EnumFactorAccessGroupProperties.SaveFactorType || !Enum.IsDefined(typeof(EnumFactorAccessGroupProperties), property))
                        {
                            if (accessSave[i].FactorTypeSave)
                            {
                                accessfactorTypeIds.Add(accessSave[i].FactorAccessGroupId);
                            }
                        }
                        if (property == EnumFactorAccessGroupProperties.SaveOrganizationUnit || !Enum.IsDefined(typeof(EnumFactorAccessGroupProperties), property))
                        {
                            if (accessSave[i].OrganizationUnitSave)
                            {
                                accessorganizationIds.Add(accessSave[i].FactorAccessGroupId);
                            }
                        }
                        if (property == EnumFactorAccessGroupProperties.SaveRoleOrganization || !Enum.IsDefined(typeof(EnumFactorAccessGroupProperties), property))
                        {
                            if (accessSave[i].RoleOfOrganizationSave)
                            {
                                accessroleoforganIds.Add(accessSave[i].FactorAccessGroupId);
                            }
                        }

                    }
                }
                if (accessView.Count > 0)
                {
                    for (int i = 0; i < accessView.Count; i++)
                    {
                        if (property == EnumFactorAccessGroupProperties.ViewFactorType || !Enum.IsDefined(typeof(EnumFactorAccessGroupProperties), property))
                        {
                            if (accessView[i].FactorTypeView)
                            {
                                accessfactorTypeIds.Add(accessView[i].FactorAccessGroupId);
                            }
                        }
                        if (property == EnumFactorAccessGroupProperties.ViewOrganizationUnit || !Enum.IsDefined(typeof(EnumFactorAccessGroupProperties), property))
                        {
                            if (accessView[i].OrganizationUnitView)
                            {
                                accessorganizationIds.Add(accessView[i].FactorAccessGroupId);
                            }
                        }
                        if (property == EnumFactorAccessGroupProperties.ViewRoleOrganization || !Enum.IsDefined(typeof(EnumFactorAccessGroupProperties), property))
                        {
                            if (accessView[i].RoleOfOrganizationView)
                            {
                                accessroleoforganIds.Add(accessView[i].FactorAccessGroupId);
                            }
                        }
                    }
                }
                if (accessEdit.Count > 0)
                {
                    for (int i = 0; i < accessEdit.Count; i++)
                    {
                        if (property == EnumFactorAccessGroupProperties.EditFactorType || !Enum.IsDefined(typeof(EnumFactorAccessGroupProperties), property))
                        {
                            if (accessEdit[i].FactorTypeEdit)
                            {
                                accessfactorTypeIds.Add(accessEdit[i].FactorAccessGroupId);
                            }
                        }
                        if (property == EnumFactorAccessGroupProperties.EditOrganizationUnit || !Enum.IsDefined(typeof(EnumFactorAccessGroupProperties), property))
                        {
                            if (accessEdit[i].OrganizationUnitEdit)
                            {
                                accessorganizationIds.Add(accessEdit[i].FactorAccessGroupId);
                            }
                        }
                        if (property == EnumFactorAccessGroupProperties.EditRoleOrganization || !Enum.IsDefined(typeof(EnumFactorAccessGroupProperties), property))
                        {
                            if (accessEdit[i].RoleOfOrganizationEdit)
                            {
                                accessroleoforganIds.Add(accessEdit[i].FactorAccessGroupId);
                            }
                        }

                    }
                }
                if (accessDelete.Count > 0)
                {
                    for (int i = 0; i < accessDelete.Count; i++)
                    {
                        if (property == EnumFactorAccessGroupProperties.DeleteFactorType || !Enum.IsDefined(typeof(EnumFactorAccessGroupProperties), property))
                        {
                            if (accessDelete[i].FactorTypeDelete)
                            {
                                accessfactorTypeIds.Add(accessDelete[i].FactorAccessGroupId);
                            }
                        }
                        if (property == EnumFactorAccessGroupProperties.DeleteOrganizationUnit || !Enum.IsDefined(typeof(EnumFactorAccessGroupProperties), property))
                        {
                            if (accessDelete[i].OrganizationUnitDelete)
                            {
                                accessorganizationIds.Add(accessDelete[i].FactorAccessGroupId);
                            }
                        }
                        if (property == EnumFactorAccessGroupProperties.DeleteRoleOrganization || !Enum.IsDefined(typeof(EnumFactorAccessGroupProperties), property))
                        {
                            if (accessDelete[i].RoleOfOrganizationDelete)
                            {
                                accessroleoforganIds.Add(accessDelete[i].FactorAccessGroupId);
                            }
                        }

                    }
                }
                accessfactorTypeIds = accessfactorTypeIds.Distinct().ToList();
                accessorganizationIds = accessorganizationIds.Distinct().ToList();
                accessroleoforganIds = accessroleoforganIds.Distinct().ToList();

                return (accessfactorTypeIds, accessorganizationIds, accessroleoforganIds);
            }
            catch (Exception ex)
            {

                return (null, null, null);
            }
        }

    }
}
    

