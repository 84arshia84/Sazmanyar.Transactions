using ApplicationService.DtoModels.ContractDtos;
using ApplicationService.DtoModels.SettingDtos;
using AppCore.Entities.ContractsInformation.ServiceExplanations;
using AppCore.Enums;
using ApplicationService.ServicesContract.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationService.ServicesContract.ContractInformation;
using AppCore.Entities.ContractsInformation.ExecutionRequestServiceExplanations;

namespace ApplicationService.Mapper.ContractInformationMapper
{
    internal  class ServiceExplanationAutoMapperProfile
    {
        private readonly IExecutionRequestServiceExplanationService _exService;
        public ServiceExplanationAutoMapperProfile(IExecutionRequestServiceExplanationService exService)
        {
            this._exService = exService;
        }
        public static ServiceExplanation DtoToEntity(ServiceExplanationDto dto, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entity = new ServiceExplanation();
                if (dto.TransactionExecutionServiceExplanationId.HasValue)
                {
                    entity.ID = dto.TransactionExecutionServiceExplanationId.Value;
                }

                entity.ID = dto.key;
                entity.Title = dto.Title;
                entity.ActivityReference = (ActivityReferenceEnum)dto.ActivityReference;
                entity.ProjectID = dto.ProjectID;
                entity.ProjectName = dto.ProjectName;
                entity.ProposalID = dto.ProposalID;
                entity.ProposalName = dto.ProposalName;
                entity.DiscountAmount = dto.DiscountAmount;
                entity.UnitAmount = dto.UnitAmount;
                entity.TotalAmount = dto.TotalAmount;
                entity.ProgramVolume = dto.programVolume;
                entity.DeliverablePen = dto.DeliverablePen;
                entity.AccelerationRate = dto.AccelerationRate;
                entity.PrepaymentPercentage = dto.PrepaymentPercentage;
                entity.ExplanationStartingDate = dto.ExplanationStartingDate;
                entity.ExplanationEndingDate = dto.ExplanationEndingDate;
                entity.IsDeleted = dto.IsDeleted;
                entity.ExplanationType = (ExplanationTypeEnum)dto.ExplanationType;
                entity.ContractEstimatedmeters = null;
                if (entity.ExplanationType == (ExplanationTypeEnum)3)
                {
                    entity.ContractEstimatedmeters = ContractEstimatedmeterAutoMapperProfile.DtosToEntities(dto.estimatedmeterDtos, entity.ID, errorLoggerService);
                }
                entity.ActivityCenterID = dto.ActivityCenterID;
                entity.ActivityCenterTitle = dto.ActivityCenterTitle;
                entity.CurrencyID = dto.CurrencyID;
                entity.FinePaymentMethodID = dto.FinePaymentMethodID;
                entity.UnitOfMeasurementID = dto.UnitOfMeasurementID;
                entity.Order = dto.Row;
                entity.IsDeleted = (bool)(dto.IsDeleted == null ? false : dto.IsDeleted);
                entity.UpdatedInAddendum = dto.IsUpdated;
                entity.IsAddendum = dto.IsAddendum;
                entity.ContractAddendumId = dto.AddendumId;
                entity.IsForExecutionRequest = dto.IsForExecutionRequest;
                return entity;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new ServiceExplanation();
            }

        }
        public static ServiceExplanationDto EntityToDto(ServiceExplanation entity, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dto = new ServiceExplanationDto();
                dto.key = entity.ID;
                dto.Title = entity.Title;
                dto.ActivityReference = (int)entity.ActivityReference;
                dto.ProjectID = entity.ProjectID;
                dto.ProjectName = entity.ProjectName;
                dto.ProposalID = entity.ProposalID;
                dto.ProposalName = entity.ProposalName;
                dto.DiscountAmount = entity.DiscountAmount;
                dto.UnitAmount = entity.UnitAmount;
                dto.TotalAmount = entity.TotalAmount;
                dto.programVolume = entity.ProgramVolume;
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
                dto.IsForExecutionRequest = entity.IsForExecutionRequest;
                dto.IsDeleted = false;
                dto.IsAddendum = entity.IsAddendum;
                dto.AddendumId = entity.ContractAddendumId;
                dto.UpdatedInAddendum = entity.UpdatedInAddendum;
                dto.estimatedmeterDtos = entity.ContractEstimatedmeters != null ? ContractEstimatedmeterAutoMapperProfile.EntitiesToDtos(entity.ContractEstimatedmeters, errorLoggerService) : null;
                dto.Row = entity.Order;
                return dto;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new ServiceExplanationDto();
            }

        }
        public static List<ServiceExplanation> DtosToEntities(List<ServiceExplanationDto> dtos, Guid contractId, IErrorLoggerService errorLoggerService)
        {
            var entities = new List<ServiceExplanation>();
            foreach (var item in dtos)
            {
                try
                {
                    var entity = new ServiceExplanation();
                    entity = DtoToEntity(item, errorLoggerService);
                    entity.ContractID = contractId;
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
        public static List<ServiceExplanationDto> EntitiesToDtos(List<ServiceExplanation> entities, IErrorLoggerService errorLoggerService)
        {
            var dtos = new List<ServiceExplanationDto>();
            foreach (var item in entities)
            {
                try
                {
                    var dto = new ServiceExplanationDto();
                    dto = EntityToDto(item, errorLoggerService);
                    dto.ContractID = item.ContractID;
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
