using AppCore.Entities.ContractAccessGroups;
using AppCore.Entities.User;
using AppCore.Enums;
using AppCore.UnitOfWork;
using ApplicationService.DtoModels.UserDtos;
using ApplicationService.Mapper.UserMapper;
using ApplicationService.ServicesContract.ExceptionHandling;
using ApplicationService.ServicesContract.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Services.UserSerive
{
    internal class UserService : IUserService
    {
        private IUnitOfWork _unitOfWork;
        private IErrorLoggerService _errorLoggerService;
        public UserService(IUnitOfWork unitOfWork, IErrorLoggerService errorLoggerService)
        {
            _unitOfWork = unitOfWork;
            _errorLoggerService = errorLoggerService;
        }
        public async Task<List<LoginUserDto>> GetAll(string connectionString)
        {
            try
            {
                return UserAutoMapperProfile.EntitiesToDtos(await _unitOfWork.UserRepository.GetAll(connectionString), _errorLoggerService);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<LoginUserDto>();
            }
        }

        public async Task<LoginUserDto> GetByFullQualifyName(string fullqualify, string connectionString)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.GetByFullQualifyName(fullqualify, connectionString);
                if (user == null)
                {
                    return new LoginUserDto();
                }
                return UserAutoMapperProfile.EntityToDto(user, _errorLoggerService);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new LoginUserDto();
            }
        }
        /// <summary>
        /// دسترسی کاربر به بخش های سامانه
        /// </summary>
        /// <param name="fullqualify"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<UserAccessOnPermissionsDto> GetUserPermissionsAndSaveAccess(string fullqualify, string connectionString)
        {
            try
            {
                var userAccess = new UserAccessOnPermissionsDto();
                if (fullqualify.ToLower() == "sharepoint\\system")
                {
                    userAccess.AccessToContract = true;
                    userAccess.AccessToWorkFlow = true;
                    userAccess.AccessToAccessGroupSettings = true;
                    userAccess.AccessToTransactionExecutionRequest = true;
                    userAccess.AccessToBaseSettings = true;
                    userAccess.AccessToContractAddendum = true;
                    userAccess.AccessToPriceListSettings = true;
                    userAccess.AccessToInvoice = true;
                    userAccess.AccessToInvoiceWorkFlow = true;
                    userAccess.AccessToInvoiceAccessGroupSetting = true;
                    userAccess.AccessToInvoiceBaseSetting = true;
                    userAccess.AccessToFactor = true;
                    userAccess.AccessToSaveFactor = true;
                    userAccess.AccessToFactorAccessGroup =true;
                    return userAccess;
                }
                var user = await _unitOfWork.UserRepository.GetByFullQualifyName(fullqualify, connectionString);
                var access = await _unitOfWork.ContractAccessGroupRepository.GetUserPermissionsAndSaveAccess(user.ID);
                var invoiceAccess = await _unitOfWork.InvoiceAccessGroupRepository.GetUserPermissions(user.ID);
                var factorAccess = await _unitOfWork.FactorAccessGroupRepository.GetUserPermissionsAndSaveAccess(user.ID);
                for (int i = 0; i < access.properties.Count; i++)
                {
                    var ThisSystemPart = access.systemParts.Where(x => x.ContractAccessGroupId == access.properties[i].ContractAccessGroupId).ToList();
                    if (ThisSystemPart.Count == 0)
                    {
                        userAccess.AccessToSaveContract = true;
                        userAccess.AccessToSaveContractAddendum = true;
                        userAccess.AccessToSaveTransactionExecutionRequest = true;
                        break;
                    }
                    if (!userAccess.AccessToSaveContract) {
                        userAccess.AccessToSaveContract = CheckPermissionAccess(ThisSystemPart, (SystemParts)2);
                    }
                    if (!userAccess.AccessToSaveTransactionExecutionRequest)
                    {
                        userAccess.AccessToSaveTransactionExecutionRequest = CheckPermissionAccess(ThisSystemPart, (SystemParts)1);
                    }
                    if (!userAccess.AccessToSaveContractAddendum)
                    {
                        userAccess.AccessToSaveContractAddendum = CheckPermissionAccess(ThisSystemPart, (SystemParts)3);
                    }

                }
                for (int i = 0; i < access.permissions.Count; i++)
                {
                    if (access.permissions[i].Contract == true )
                    {
                        userAccess.AccessToContract = true;
                    }
                    if (access.permissions[i].AccessGroupSettings == true)
                    {
                        userAccess.AccessToAccessGroupSettings = true;
                    }
                    if (access.permissions[i].BaseSettings == true)
                    {
                        userAccess.AccessToBaseSettings = true;
                    }
                    if (access.permissions[i].ContractAddendum == true)
                    {
                        userAccess.AccessToContractAddendum = true;
                    }
                    if (access.permissions[i].TransactionExecutionRequest == true)
                    {
                        userAccess.AccessToTransactionExecutionRequest = true;
                    }
                    if (access.permissions[i].PriceListSettings == true)
                    {
                        userAccess.AccessToPriceListSettings = true;
                    }
                    if (access.permissions[i].WorkFlow == true)
                    {
                        userAccess.AccessToWorkFlow = true;
                    }
                }
                for (int i = 0; i < invoiceAccess.Count; i++)
                {
                    if (invoiceAccess[i].Invoice == true)
                    {
                        userAccess.AccessToInvoice = true;
                    }
                    if (invoiceAccess[i].AccessGroupSettings == true)
                    {
                        userAccess.AccessToInvoiceAccessGroupSetting = true;
                    }
                    if (invoiceAccess[i].BaseSettings == true)
                    {
                        userAccess.AccessToInvoiceBaseSetting = true;
                    }
                    if (invoiceAccess[i].WorkFlow == true)
                    {
                        userAccess.AccessToInvoiceWorkFlow = true;
                    }
                   
                }
                for (int i = 0; i < factorAccess.permissions.Count; i++)
                {
                    if (factorAccess.permissions[i].AccessGroupSettings == true)
                    {
                        userAccess.AccessToFactorAccessGroup = true;
                    }
                    if (factorAccess.permissions[i].Factor == true)
                    {
                        userAccess.AccessToFactor = true;
                    }
                    if (factorAccess.permissions[i].BaseSettings == true)
                    {
                        userAccess.AccessToInvoiceBaseSetting = true;
                    }
                    if (factorAccess.permissions[i].WorkFlow == true)
                    {
                        userAccess.AccessToInvoiceWorkFlow = true;
                    }
                }
                for (int i = 0; i < factorAccess.properties.Count; i++)
                {
                    if (factorAccess.properties[i].FactorTypeSave == true || factorAccess.properties[i].OrganizationUnitSave || factorAccess.properties[i].RoleOfOrganizationSave == true)
                    {
                        userAccess.AccessToSaveFactor = true;
                        break;
                    }
                }
                return userAccess;
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new UserAccessOnPermissionsDto();
            }
        }
        internal bool CheckPermissionAccess(List<ContractAccessGroupSystemParts> access, SystemParts parts)
        {
            try
            {
                for (int i = 0; i < access.Count; i++)
                {
                    if (parts == SystemParts.Contract)
                    {
                        if (SystemParts.Contract == access[i].SystemParts)
                        {
                            return true;
                        }
                    }
                    if (parts == SystemParts.ContractAddendum)
                    {
                        if (SystemParts.ContractAddendum == access[i].SystemParts)
                        {
                            return true;
                        }
                    }
                    if (parts == SystemParts.TransactionExecutionRequest)
                    {
                        if (SystemParts.TransactionExecutionRequest == access[i].SystemParts)
                        {
                            return true;
                        }
                    }

                }
                return false;
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return false;
            }

        }
    }
}
