using AppCore.Entities.ContractsInformation.ContractCoefficients;
using ApplicationService.DtoModels.ContractDtos;
using ApplicationService.ServicesContract.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Mapper.ContractInformationMapper
{
    internal static class ContractCoefficientAutoMapperProfile
    {
        public static ContractCoefficient DtoToEntity(ContractCoefficientDto dto, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entity= new ContractCoefficient();
                entity.Id=dto.Id;
                entity.Title=dto.Title;
                entity.CoefficientValue = dto.defaultCoefficient;
                entity.DefaultRowValue = dto.defaultRows;
                entity.UpdatedInAddendum = dto.IsUpdated;
                entity.IsDeleted = dto.IsDeleted;
                entity.IsAddendum = dto.IsAddendum;
                entity.AddendumId = dto.AddendumId;
                entity.Order = dto.row;
                return entity;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new ContractCoefficient();
            }
        }
        public static ContractCoefficientDto EntityToDto(ContractCoefficient entity, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dto = new ContractCoefficientDto();
                dto.Id=entity.Id;
                dto.Title=entity.Title;
                dto.defaultCoefficient = entity.CoefficientValue != null ? (decimal)entity.CoefficientValue : 1;
                dto.defaultRows = entity.DefaultRowValue != null ? entity.DefaultRowValue : "" ;
                dto.row = entity.Order != null ? (int)entity.Order : 1;
                dto.IsAddendum=entity.IsAddendum;
                dto.AddendumId=entity.AddendumId;
                return dto;

            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new ContractCoefficientDto();
            }
        }
        public static List<ContractCoefficient> DtosToEntities(List<ContractCoefficientDto> dtos,Guid contractId, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entities= new List<ContractCoefficient>();
                foreach (var item in dtos)
                {
                    var entity= new ContractCoefficient();
                    entity=DtoToEntity(item, errorLoggerService);
                    entity.ContractId=contractId;
                    entities.Add(entity);
                }
                return entities;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new List<ContractCoefficient>();
            }
        }
        public static List<ContractCoefficientDto> EntitiesToDtos(List<ContractCoefficient> entities, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dtos = new List<ContractCoefficientDto>();
                foreach (var item in entities)
                {
                    var dto = new ContractCoefficientDto();
                    dto= EntityToDto(item, errorLoggerService);
                    dtos.Add(dto);
                }
                return dtos;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new List<ContractCoefficientDto>();
            }
        }
    }
}
