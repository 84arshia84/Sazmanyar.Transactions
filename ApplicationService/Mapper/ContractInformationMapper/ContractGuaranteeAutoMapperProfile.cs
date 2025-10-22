using AppCore.Entities.ContractsInformation.ContractAddendums;
using AppCore.Entities.ContractsInformation.ContractGuarantees;
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
    internal static class ContractGuaranteeAutoMapperProfile
    {
        public static async Task<ContractGuarantee> DtoToEntity(ContractGuaranteeDto dto, LoginUserDto user, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entity = new ContractGuarantee();
                entity.Id = dto.Id;
                entity.ReleaseConditionId = dto.ReleaseConditionId;
                entity.ContractId = dto.ContractId;
                entity.ForGuaranteeId = dto.ForGuaranteeId;
                entity.RealeaseDescription = dto.RealeaseDescription;
                entity.ReleaseDate = dto.ReleaseDate;
                entity.GuaranteePrice = dto.GuaranteePrice;
                entity.ValidityDate = dto.ValidityDate;
                entity.TypeOfGuaranteeId =dto.TypeOfGuaranteeId;
                entity.RealeaseExplenation = dto.RealeaseExplenation;
                entity.IsDeleted = dto.IsDeleted;
                entity.IsUpdate = dto.IsUpdated;
                return entity;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new ContractGuarantee();
            }
        }
        public static async Task<ContractGuaranteeDto> EntityToDto(ContractGuarantee entity, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dto = new ContractGuaranteeDto();
                dto.Id = entity.Id;
                dto.ReleaseConditionId = entity.ReleaseConditionId;
                dto.ContractId = entity.ContractId;
                dto.ForGuaranteeId = entity.ForGuaranteeId;
                dto.RealeaseDescription = entity.RealeaseDescription;
                dto.ReleaseDate = entity.ReleaseDate;
                dto.GuaranteePrice = entity.GuaranteePrice;
                dto.ValidityDate = entity.ValidityDate;
                dto.TypeOfGuaranteeId = entity.TypeOfGuaranteeId;
                dto.RealeaseExplenation = entity.RealeaseExplenation;
                dto.ForGuaranteeTitle = entity.ForGuarantee != null ? entity.ForGuarantee.Title : "";
                dto.ReleaseConditionTitle = entity.ReleaseCondition !=null ? entity.ReleaseCondition.Title :"";
                dto.TypeOfGuaranteeTitle = entity.TypeOfGuarantee.Title;
                return dto;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new ContractGuaranteeDto();
            }
        }
        public static async Task<List<ContractGuarantee>> DtosToEntities(List<ContractGuaranteeDto> dtos,Guid contractid, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entities = new List<ContractGuarantee>();
                foreach (var item in dtos)
                {
                    var entity = new ContractGuarantee();
                    entity = await DtoToEntity(item, new LoginUserDto(), errorLoggerService);
                    entity.ContractId = contractid;
                    entities.Add(entity);
                }
                return entities;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new List<ContractGuarantee>();
            }
        }
        public static async Task<List<ContractGuaranteeDto>> EntitiesToDtos(List<ContractGuarantee> entities, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dtos = new List<ContractGuaranteeDto>();
                foreach (var item in entities)
                {
                    var dto = new ContractGuaranteeDto();
                    dto = await EntityToDto(item, errorLoggerService);
                    dtos.Add(dto);
                }
                return dtos;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new List<ContractGuaranteeDto>();
            }
        }
    }
}
