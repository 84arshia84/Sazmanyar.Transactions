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
    internal class ContractAccessGroupPermissionsAutoMapperProfile
    {
        public static async Task<ContractAccessGroupPermissions> DtoToEntity(ContractAccessGroupPermissionsDto dto,Guid contractAccessGroup, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entity = new ContractAccessGroupPermissions();
                entity.Id = dto.Id;
                entity.ContractAccessGroupId = contractAccessGroup;
                entity.TransactionExecutionRequest = dto.TransactionExecutionRequest;
                entity.Contract = dto.Contract;
                entity.ContractAddendum = dto.ContractAddendum;
                entity.AccessGroupSettings = dto.AccessGroupSettings;
                entity.BaseSettings = dto.BaseSettings;
                entity.PriceListSettings = dto.PriceListSettings;
                entity.WorkFlow = dto.WorkFlow;
                return entity;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new ContractAccessGroupPermissions();
            }
        }
        public static async Task<ContractAccessGroupPermissionsDto> EntityToDto(ContractAccessGroupPermissions entity, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dto = new ContractAccessGroupPermissionsDto();
                dto.Id = entity.Id;
                dto.TransactionExecutionRequest = entity.TransactionExecutionRequest;
                dto.Contract = entity.Contract;
                dto.ContractAddendum = entity.ContractAddendum;
                dto.AccessGroupSettings = entity.AccessGroupSettings;
                dto.BaseSettings = entity.BaseSettings;
                dto.PriceListSettings= entity.PriceListSettings;
                dto.WorkFlow = entity.WorkFlow;
                return dto;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new ContractAccessGroupPermissionsDto();
            }
        }
    }
}
