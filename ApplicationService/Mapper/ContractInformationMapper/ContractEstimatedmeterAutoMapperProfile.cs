using ApplicationService.DtoModels.ContractDtos;
using AppCore.Entities.ContractsInformation.ContractEstimatedmeters;
using ApplicationService.Services.ExceptionHandlingService;
using ApplicationService.ServicesContract.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Mapper.ContractInformationMapper
{
    internal static class ContractEstimatedmeterAutoMapperProfile
    {
        public static ContractEstimatedmeter DtoToEntity(EstimatedmeterDto dto, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entity = new ContractEstimatedmeter();
                entity.Id = dto.id;
                entity.Year = dto.year;
                entity.YearId = dto.yearId;
                entity.Field = dto.field;
                entity.FieldId = dto.fieldId;
                entity.Clause = dto.clause;
                entity.ClauseId = dto.clauseId;
                entity.Explenation = dto.explenation;
                entity.ExplenationId = dto.explenationId;
                entity.Amount = dto.amount;
                entity.CoefficientTitle = dto.coefficientTitle;
                entity.SumOfCoefficients = dto.sumOfCoefficients == null ? 1 : dto.sumOfCoefficients;
                entity.RawPrice = dto.rawPrice;
                entity.RowPrice = dto.rowPrice;
                entity.Description = dto.description;
                entity.ServiceExplanationId = dto.serviceExplanationId;
                entity.IsDeleted = dto.isDeleted;
                entity.UpdatedInAddendum = dto.updatedInAddendum;
                entity.IsAddendum = dto.isAddendum;
                entity.AddendumId = dto.addendumId;
                entity.Order = dto.row;
                return entity;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new ContractEstimatedmeter();
            }
        }
        public static EstimatedmeterDto EntityToDto(ContractEstimatedmeter entity, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dto = new EstimatedmeterDto();
                dto.id = entity.Id;
                dto.year = entity.Year;
                dto.yearId = entity.YearId;
                dto.field = entity.Field;
                dto.fieldId = entity.FieldId;
                dto.clause = entity.Clause;
                dto.clauseId = entity.ClauseId;
                dto.explenation = entity.Explenation;
                dto.explenationId = entity.ExplenationId;
                dto.amount = entity.Amount;
                dto.coefficientTitle = entity.CoefficientTitle;
                dto.sumOfCoefficients = entity.SumOfCoefficients;
                dto.rowPrice = entity.RowPrice;
                dto.description = entity.Description;
                dto.serviceExplanationId = entity.ServiceExplanationId;
                dto.updatedInAddendum = entity.UpdatedInAddendum;
                dto.isDeleted = false;
                dto.isAddendum = entity.IsAddendum;
                dto.updatedInAddendum = entity.UpdatedInAddendum;
                dto.addendumId = entity.AddendumId;
                dto.row = entity.Order != null ? (int)entity.Order : 1  ;
                return dto;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new EstimatedmeterDto();
            }
        }
        public static List<EstimatedmeterDto> EntitiesToDtos(List<ContractEstimatedmeter>  entities , IErrorLoggerService errorLoggerService)
        {
            var dtos =new List<EstimatedmeterDto>();
            int row = 1;
            foreach (var item in entities)
            {
                try
                {
                    var dto = new EstimatedmeterDto();
                    dto = EntityToDto(item, errorLoggerService);
                    dto.row = row;
                    dtos.Add(dto);
                    row++;
                }
                catch (Exception ex)
                {
                    errorLoggerService.SaveError(ex);
                    continue;
                }
               
            }
            return dtos;
        }
        public static List<ContractEstimatedmeter> DtosToEntities(List<EstimatedmeterDto> dtos , IErrorLoggerService errorLoggerService)
        {
            var entities = new List<ContractEstimatedmeter>();
            foreach (var item in dtos)
            {
                try
                {
                    var entity = new ContractEstimatedmeter();
                    entity = DtoToEntity(item, errorLoggerService);
                    entities.Add(entity);
                }
                catch (Exception ex)
                {
                    errorLoggerService.SaveError(ex);
                    continue;
                }
                
            }
            return entities;
        }
        public static List<ContractEstimatedmeter> DtosToEntities(List<EstimatedmeterDto> dtos,Guid serviceExplanationId, IErrorLoggerService errorLoggerService)
        {
            var entities = new List<ContractEstimatedmeter>();
            foreach (var item in dtos)
            {
                try
                {
                    var entity = new ContractEstimatedmeter();
                    entity = DtoToEntity(item, errorLoggerService);
                    entity.ServiceExplanationId = serviceExplanationId;
                    entities.Add(entity);
                }
                catch (Exception ex)
                {
                    errorLoggerService.SaveError(ex);
                    continue;
                }
                
            }
            return entities;
        }
    }
}
