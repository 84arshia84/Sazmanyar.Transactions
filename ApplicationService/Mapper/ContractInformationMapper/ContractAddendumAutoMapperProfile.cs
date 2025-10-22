using AppCore.Entities.ContractsInformation.ContractAddendums;
using AppCore.Enums;
using ApplicationService.DtoModels.ContractDtos;
using ApplicationService.DtoModels.UserDtos;
using ApplicationService.ServicesContract.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Mapper.ContractInformationMapper
{
    internal static class ContractAddendumAutoMapperProfile
    {
        public static async Task<ContractAddendum> DtoToEntity(ContractAddendumDto dto, LoginUserDto user, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entity = new ContractAddendum();
                entity.Id = dto.Id;
                entity.Title = dto.Title;
                entity.AddendumDate = dto.AddendumDate;
                entity.AddendumTypeId = dto.AddendumTypeId;
                entity.ContractId = dto.ContractId;
                entity.AddendumChangeType = (AddendumChangeTypeEnum)dto.AddendumChangeType;
                entity.RateOfContractPriceChanged = dto.RateOfContractPriceChanged;
                entity.IsDeleted = dto.IsDeleted;
                return entity;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new ContractAddendum();
            }
        }
        public static async Task<ContractAddendumDto> EntityToDto(ContractAddendum entity, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dto = new ContractAddendumDto();
                dto.Id = entity.Id;
                dto.Title = entity.Title;
                dto.AddendumDate = entity.AddendumDate;
                dto.AddendumTypeId = entity.AddendumTypeId;
                dto.ContractId = entity.ContractId;
                dto.IsDeleted = entity.IsDeleted;
                dto.RateOfContractPriceChanged = entity.RateOfContractPriceChanged;
                dto.AddendumChangeType = (int)entity.AddendumChangeType;
                dto.AddendumTypeTitle = entity.AddendumType.Title;
                dto.IsfinalApproved = entity.IsFinalApprove;

                dto.Contract = new ContractDto();
                dto.Contract.contractTitle = entity.Contract.ContractTitle;
                dto.Contract.contractNumber = entity.Contract.ContractNumber;
                dto.Contract.contractExpertKey = entity.Contract.ContractExpertID;
                dto.Contract.consultantID = entity.Contract.ConsultantID;
                dto.Contract.contractTypeID = entity.Contract.ContractTypeID;
                dto.Contract.transActionTypeID = entity.Contract.TransActionTypeID;
                dto.Contract.roleOFOrganizationID = entity.Contract.RoleOFOrganizationID;
                dto.Contract.corespondentID = entity.Contract.CorespondentID;
                dto.Contract.organizationUnitID = entity.Contract.OrganizationUnitID;
                dto.Contract.creditSourceID = entity.Contract.CreditSourceID;
                dto.Contract.ContractDateOfNotification = entity.Contract.ContratTimeProfile.ContractDateOfNotification;
                dto.Contract.ContractExchangeDate = entity.Contract.ContratTimeProfile.ContractExchangeDate;
                dto.Contract.ContractStartDate = entity.Contract.ContratTimeProfile.ContractStartDate;
                dto.Contract.ContractEndDate = entity.Contract.ContratTimeProfile.ContractEndDate;
                dto.Contract.BasisForStartingProjectID = entity.Contract.ContratTimeProfile.BasisForStartingProjectID;
                return dto;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new ContractAddendumDto();
            }
        }
        public static async Task<List<ContractAddendum>> DtosToEntities(List<ContractAddendumDto> dtos, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entities = new List<ContractAddendum>();
                foreach (var item in dtos)
                {
                    var entity = new ContractAddendum();
                    entity = await DtoToEntity(item, new LoginUserDto(), errorLoggerService);
                    entities.Add(entity);
                }
                return entities;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new List<ContractAddendum>();
            }
        }
        public static async Task<List<ContractAddendumDto>> EntitiesToDtos(List<ContractAddendum> entities, IErrorLoggerService errorLoggerService)
        {

            var dtos = new List<ContractAddendumDto>();
            foreach (var item in entities)
            {
                try
                {
                    var dto = new ContractAddendumDto();
                    dto = await EntityToDto(item, errorLoggerService);
                    dtos.Add(dto);
                }
                catch (Exception ex)
                {
                    errorLoggerService.SaveError(ex);
                    continue;
                }
            }
            return dtos;
        }
    }
}
