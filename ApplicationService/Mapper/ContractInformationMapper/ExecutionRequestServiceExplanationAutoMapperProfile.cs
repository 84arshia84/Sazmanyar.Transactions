using AppCore.Entities.ContractsInformation.ExecutionRequestServiceExplanations;
using AppCore.Entities.ContractsInformation.ServiceExplanations;
using AppCore.Enums;
using ApplicationService.DtoModels.ContractDtos;
using ApplicationService.ServicesContract.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Mapper.ContractInformationMapper
{
    internal static class ExecutionRequestServiceExplanationAutoMapperProfile
    {
        public static ExecutionRequestServiceExplanation DtoToEntity(ExecutionRequestServiceExplanationDto dto, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entity = new ExecutionRequestServiceExplanation();
               
                entity.ID = dto.key;
                entity.Title = dto.Title;
                entity.ActivityReference = (ActivityReferenceEnum)dto.ActivityReference;
                entity.ProjectID = dto.ProjectID;
                entity.ProjectName = dto.ProjectName;
                entity.ProposalID = dto.ProposalID;
                entity.ProposalName = dto.ProposalName;
                entity.UnitAmount = dto.UnitAmount;
                entity.TotalAmount = dto.TotalAmount;
                entity.ProgramVolume = dto.ProgramVolume;
                entity.DeliverablePen = dto.DeliverablePen;
                entity.AccelerationRate = dto.AccelerationRate;
                entity.PrepaymentPercentage = dto.PrepaymentPercentage;
                entity.ExplanationStartingDate = dto.ExplanationStartingDate;
                entity.ExplanationEndingDate = dto.ExplanationEndingDate;
                entity.ExplanationType = (ExplanationTypeEnum)dto.ExplanationType;
                entity.ActivityCenterID = dto.ActivityCenterID;
                entity.ActivityCenterTitle = dto.ActivityCenterTitle;
                entity.CurrencyID = dto.CurrencyID;
                entity.FinePaymentMethodID = dto.FinePaymentMethodID;
                entity.UnitOfMeasurementID = dto.UnitOfMeasurementID;
                entity.IsSupplyList = dto.IsSupplyList;
                entity.SupplyListName = dto.SupplyListName;
                entity.SupplyListId = dto.SupplyListId;
                entity.CommodityId = dto.CommodityId;
                entity.CommodityName = dto.CommodityName;
                entity.IsDeleted = dto.IsDeleted;
                entity.Order = dto.Row;
                return entity;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new ExecutionRequestServiceExplanation();
            }

        }
        public static ExecutionRequestServiceExplanationDto EntityToDto(ExecutionRequestServiceExplanation entity, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dto = new ExecutionRequestServiceExplanationDto();
                dto.key = entity.ID;
                dto.Title = entity.Title;
                dto.ActivityReference = (int)entity.ActivityReference;
                dto.ProjectID = entity.ProjectID;
                dto.ProjectName = entity.ProjectName;
                dto.ProposalID = entity.ProposalID;
                dto.ProposalName = entity.ProposalName;
                dto.UnitAmount = entity.UnitAmount;
                dto.TotalAmount = entity.TotalAmount;
                dto.ProgramVolume = entity.ProgramVolume;
                dto.DeliverablePen = entity.DeliverablePen;
                dto.AccelerationRate = entity.AccelerationRate;
                dto.PrepaymentPercentage = entity.PrepaymentPercentage;
                dto.ExplanationStartingDate = entity.ExplanationStartingDate;
                dto.ExplanationEndingDate = entity.ExplanationEndingDate;
                dto.ExplanationType = (int)entity.ExplanationType;
                dto.ActivityCenterID = entity.ActivityCenterID;
                dto.ActivityCenterTitle = entity.ActivityCenterTitle;
                dto.CurrencyID = entity.CurrencyID;
                dto.FinePaymentMethodID = entity.FinePaymentMethodID;
                dto.UnitOfMeasurementID = entity.UnitOfMeasurementID;
                dto.IsSupplyList = entity.IsSupplyList;
                dto.SupplyListId = entity.SupplyListId;
                dto.SupplyListName = entity.SupplyListName;
                dto.CommodityId = entity.CommodityId;
                dto.CommodityName = entity.CommodityName;
                dto.Row = entity.Order;
                return dto;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new ExecutionRequestServiceExplanationDto();
            }

        }
        public static List<ExecutionRequestServiceExplanation> DtosToEntities(List<ExecutionRequestServiceExplanationDto> dtos, Guid transActionId, IErrorLoggerService errorLoggerService)
        {
            var entities = new List<ExecutionRequestServiceExplanation>();
            foreach (var item in dtos)
            {
                var entity = new ExecutionRequestServiceExplanation();
                entity = DtoToEntity(item, errorLoggerService);
                entity.TransactionExecutionRequestId = transActionId;
                entities.Add(entity);
            }
            return entities;
        }
        public static List<ExecutionRequestServiceExplanationDto> EntitiesToDtos(List<ExecutionRequestServiceExplanation> entities, IErrorLoggerService errorLoggerService)
        {
            var dtos = new List<ExecutionRequestServiceExplanationDto>();
            foreach (var item in entities)
            {
                var dto = new ExecutionRequestServiceExplanationDto();
                dto = EntityToDto(item, errorLoggerService);
                dto.TransactionExecutionRequestId = item.TransactionExecutionRequestId;
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}
