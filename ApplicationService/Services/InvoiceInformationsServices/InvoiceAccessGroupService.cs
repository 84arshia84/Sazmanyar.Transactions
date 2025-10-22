using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Entities.ContractsInformation.ContractAddendums;
using AppCore.Entities.ContractsInformation.Contratcs;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroups;
using AppCore.Entities.InvoiceInformations.InvoiceBaseInformations;
using AppCore.Enums;
using AppCore.UnitOfWork;
using ApplicationService.DtoModels.InvoiceDtos.InvoiceAccessGroupsDtos;
using ApplicationService.DtoModels.InvoiceDtos.NettingProcessDtos;
using ApplicationService.DtoModels.UserDtos;
using ApplicationService.Mapper.InvoiceInformationsMappers;
using ApplicationService.Services.ExceptionHandlingService;
using ApplicationService.ServicesContract.ExceptionHandling;
using ApplicationService.ServicesContract.InvoiceInformations;

namespace ApplicationService.Services.InvoiceInformationsServices
{
    public class InvoiceAccessGroupService : IInvoiceAccessGroupService
    {
        private IUnitOfWork _unitOfWork;
        private IErrorLoggerService _errorLoggerService;
        private IInvoiceAccessGroupFilterService _filterService;
        public InvoiceAccessGroupService(IUnitOfWork unitOfWork, IErrorLoggerService errorLoggerService, IInvoiceAccessGroupFilterService filterService)
        {
            _unitOfWork = unitOfWork;
            _errorLoggerService = errorLoggerService;
            _filterService = filterService;
        }
        public async Task<(string message, bool isSuccess)> AddInvoiceAccessGroup(AddInvoiceAccessGroupDto invoiceAccessGroupDto)
        {
            try
            {
                var invoiceAccessGroupId = Guid.NewGuid();
                await _unitOfWork.InvoiceAccessGroupRepository.Add(invoiceAccessGroupDto.InvoiceAccessGroupDtoToEntity(invoiceAccessGroupId, _errorLoggerService)
                    , invoiceAccessGroupDto.InvoiceAccessGroupInvoiceTypes.InvoiceAccessGroupInvoiceTypeIdsToEntities(invoiceAccessGroupId, _errorLoggerService)
                    , invoiceAccessGroupDto.InvoiceAccessGroupContractTypes.InvoiceAccessGroupContractTypeIdsToEntities(invoiceAccessGroupId, _errorLoggerService)
                    , invoiceAccessGroupDto.InvoiceAccessGroupOrganizationUnits.InvoiceAccessGroupOrganizationUnitIdsToEntities(invoiceAccessGroupId, _errorLoggerService)
                    , invoiceAccessGroupDto.InvoiceAccessGroupRoleOfOrganizations.InvoiceAccessGroupRoleOfOrganizationIdsToEntities(invoiceAccessGroupId, _errorLoggerService)
                    , invoiceAccessGroupDto.InvoiceAccessGroupGroups?.InvoiceAccessGroupGroupIdsToEntities(invoiceAccessGroupId, _errorLoggerService)
                    , invoiceAccessGroupDto.InvoiceAccessGroupUsers?.InvoiceAccessGroupUserIdsToEntities(invoiceAccessGroupId, _errorLoggerService)
                    , invoiceAccessGroupDto.InvoiceAccessGroupPermissions.InvoiceAccessGroupPermissionsDtoToEntity(invoiceAccessGroupId, _errorLoggerService)
                    , invoiceAccessGroupDto.InvoiceAccessGroupProperties.InvoiceAccessGroupPropertiesDtoToEntity(invoiceAccessGroupId, _errorLoggerService));
                await _unitOfWork.Save();
                return ("ثبت  با موفقیت انجام شد.", true);
            }
            catch (Exception ex) {
                await _errorLoggerService.SaveError(ex);
                return ("ثبت با خطا مواجه شد.", false);
            }
        }
        public async Task<(string message, bool isSuccess)> UpdateInvoiceAccessGroup(Guid invoiceAccessGroupId, AddInvoiceAccessGroupDto invoiceAccessGroupDto)
        {
            try
            {
                await DeleteInvoiceAccessGroup(invoiceAccessGroupId);
                await _unitOfWork.InvoiceAccessGroupRepository.Add(invoiceAccessGroupDto.InvoiceAccessGroupDtoToEntity(invoiceAccessGroupId, _errorLoggerService)
                    , invoiceAccessGroupDto.InvoiceAccessGroupInvoiceTypes.InvoiceAccessGroupInvoiceTypeIdsToEntities(invoiceAccessGroupId, _errorLoggerService)
                    , invoiceAccessGroupDto.InvoiceAccessGroupContractTypes.InvoiceAccessGroupContractTypeIdsToEntities(invoiceAccessGroupId, _errorLoggerService)
                    , invoiceAccessGroupDto.InvoiceAccessGroupOrganizationUnits.InvoiceAccessGroupOrganizationUnitIdsToEntities(invoiceAccessGroupId, _errorLoggerService)
                    , invoiceAccessGroupDto.InvoiceAccessGroupRoleOfOrganizations.InvoiceAccessGroupRoleOfOrganizationIdsToEntities(invoiceAccessGroupId, _errorLoggerService)
                    , invoiceAccessGroupDto.InvoiceAccessGroupGroups?.InvoiceAccessGroupGroupIdsToEntities(invoiceAccessGroupId, _errorLoggerService)
                    , invoiceAccessGroupDto.InvoiceAccessGroupUsers?.InvoiceAccessGroupUserIdsToEntities(invoiceAccessGroupId, _errorLoggerService)
                    , invoiceAccessGroupDto.InvoiceAccessGroupPermissions.InvoiceAccessGroupPermissionsDtoToEntity(invoiceAccessGroupId, _errorLoggerService)
                    , invoiceAccessGroupDto.InvoiceAccessGroupProperties.InvoiceAccessGroupPropertiesDtoToEntity(invoiceAccessGroupId, _errorLoggerService));
                await _unitOfWork.Save();
                return ("ویرایش  با موفقیت انجام شد.", true);
            }
            catch (Exception ex)
            {
                await _errorLoggerService.SaveError(ex);
                return ("ویرایش با خطا مواجه شد.", false);
            }
        }
        public async Task<GetInvoiceAccessGroupDto> GetInvoiceAccessGroupById(Guid Id)
        {
            try
            {
                var AccessGroup = await _unitOfWork.InvoiceAccessGroupRepository.GetById(Id);
                return AccessGroup.GetInvoiceAccessGroup(_errorLoggerService);
            }
            catch (Exception ex)
            {
                await _errorLoggerService.SaveError(ex);
                return new GetInvoiceAccessGroupDto();
            }
        }
        public async Task<List<AllInvoiceAccessGroupsDto>> GetAllInvoiceAccessGroups()
        {
            var accessGroups = await _unitOfWork.InvoiceAccessGroupRepository.GetAll();
            return accessGroups.GetAllAccessGroupDto();
        }
        public async Task<(string message, bool isSuccess)> DeleteInvoiceAccessGroup(Guid Id)
        {
            try
            {
                await _unitOfWork.InvoiceAccessGroupRepository.Delete(Id);
                await _unitOfWork.Save();
                return ("حذف  با موفقیت انجام شد.", true);
            }
            catch (Exception ex)
            {
                await _errorLoggerService.SaveError(ex);
                return ("حذف با خطا مواجه شد.", false);
            }
        }
        public async Task<UserAccessOnEditAndDelete> UserAccessOnEditAndDelete(Guid userId, Guid partEntityId, int part)
        {
            try
            {
                var InvoiceEntity = new InvoiceBaseInformation();
                var ContractEntity = new Contract();
                var DeleteAndEditAccess = new UserAccessOnEditAndDelete();
                if ((SystemParts)part == SystemParts.Invoice)
                {
                    InvoiceEntity = await _unitOfWork.InvoiceBaseInformationRepository.Get(partEntityId);
                    ContractEntity = await _unitOfWork.ContractRepository.Get(InvoiceEntity.ContractId);
                }
                var ContractTypeEntity = Guid.Empty;
                var OrganizationEntity = Guid.Empty;
                var RoleOfOrganizationEntity = Guid.Empty;
                var InvoiceType = Guid.Empty;
                if (ContractEntity.ID != Guid.Empty)
                {
                    ContractTypeEntity = ContractEntity.ContractTypeID;
                    OrganizationEntity = ContractEntity.OrganizationUnitID;
                    RoleOfOrganizationEntity = ContractEntity.RoleOFOrganizationID;
                }
                if (InvoiceEntity.Id != Guid.Empty)
                {
                    InvoiceType = InvoiceEntity.InvoiceTypeId;
                }
                var EntitiesIdsForDelete = await _filterService.GetAccessGroupIds(userId, (SystemParts)part, 0, 4);
                var EntitiesIdsForEdit = await _filterService.GetAccessGroupIds(userId, (SystemParts)part, 0, 3);
                var contractTypeIdsDelete = await _unitOfWork.InvoiceAccessGroupRepository.GetContractTypeUserHaveAccess(EntitiesIdsForDelete.accessGroupForContractType);
                var organUnitIdsDelete = await _unitOfWork.InvoiceAccessGroupRepository.GetOrganizationUserHaveAccess(EntitiesIdsForDelete.accessGroupForOrganizationUnit);
                var roleOfOrganIdsDelete = await _unitOfWork.InvoiceAccessGroupRepository.GetRoleOfOrganizationUserHaveAccess(EntitiesIdsForDelete.accessGroupForRoleOfOrganization);
                var invoiceTypeDelete = await _unitOfWork.InvoiceAccessGroupRepository.GetInvoiceTypeserHaveAccess(EntitiesIdsForDelete.invoiceType);

                var contractTypeIdsEdit = await _unitOfWork.InvoiceAccessGroupRepository.GetContractTypeUserHaveAccess(EntitiesIdsForEdit.accessGroupForContractType);
                var organUnitIdsEdit = await _unitOfWork.InvoiceAccessGroupRepository.GetOrganizationUserHaveAccess(EntitiesIdsForEdit.accessGroupForOrganizationUnit);
                var roleOfOrganIdsEdit = await _unitOfWork.InvoiceAccessGroupRepository.GetRoleOfOrganizationUserHaveAccess(EntitiesIdsForEdit.accessGroupForRoleOfOrganization);
                var invoiceTypeEdit = await _unitOfWork.InvoiceAccessGroupRepository.GetInvoiceTypeserHaveAccess(EntitiesIdsForEdit.invoiceType);
                if (
                    contractTypeIdsDelete.Any(x => x == ContractTypeEntity) &&
                    organUnitIdsDelete.Any(x => x == OrganizationEntity) &&
                    roleOfOrganIdsDelete.Any(x => x == RoleOfOrganizationEntity)&&
                    invoiceTypeDelete.Any(x=>x == InvoiceType)
                    )
                {
                    DeleteAndEditAccess.AccessToDelete = true;
                }
                if (
                    contractTypeIdsEdit.Any(x => x == ContractTypeEntity) &&
                    organUnitIdsEdit.Any(x => x == OrganizationEntity) &&
                    roleOfOrganIdsEdit.Any(x => x == RoleOfOrganizationEntity)&&
                    invoiceTypeEdit.Any( x => x == InvoiceType)
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
