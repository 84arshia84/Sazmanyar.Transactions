
using AppCore.Entities.FactorInformation.Factors;
using AppCore.Entities.FactorInformation.FactorServiceExplanations;
using AppCore.Enums;
using ApplicationService.DtoModels.FactorDtos.FactorServiceExplanation;
using ApplicationService.ServicesContract.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Mapper.FactorMappers
{
    internal static class FactorServiceExplanationAutoMapperProfile
    {
        public static FactorServiceExplanation DtoToEntityAdd(FactorServiceExplanationAddDto dto, Guid factorId, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entity = new FactorServiceExplanation();
                entity.Id = Guid.NewGuid();
                entity.Title = dto.Title;
                entity.ActivityReference = (ActivityReferenceEnum)dto.ActivityReference;
                entity.ProjectID = dto.ProjectID;
                entity.ProposalID = dto.ProposalID;
                entity.DeliverablePen = dto.DeliverablePen;
                entity.ExplanationType = (ExplanationTypeEnum)dto.ExplanationType;
                entity.UnitAmount = dto.UnitAmount;
                entity.TotalAmount = dto.TotalAmount;
                entity.AccelerationRate = dto.AccelerationRate;
                entity.Amount = dto.Amount;
                entity.IsSupplyList = dto.IsSupplyList;
                entity.CommodityId = dto.CommodityId;
                entity.CommodityName = dto.CommodityName;
                entity.CommodityCode = dto.CommodityCode;
                entity.SupplyListId = dto.SupplyListId;
                entity.SupplyListName = dto.SupplyListName;
                entity.ActivityCenterID = dto.ActivityCenterID;
                entity.CurrencyID = dto.CurrencyID;
                entity.UnitOfMeasurementID = dto.UnitOfMeasurementID;
                entity.FactorId = factorId;
                return entity;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return null;
            }
        }
        public static List<FactorServiceExplanation> DtosToEntitesAdd(List<FactorServiceExplanationAddDto> dtos, Guid factorId, IErrorLoggerService errorLoggerService)
        {
            var entities = new List<FactorServiceExplanation>();
            foreach (FactorServiceExplanationAddDto item in dtos)
            {
                try
                {
                    var entity = new FactorServiceExplanation();
                    entity = DtoToEntityAdd(item, factorId, errorLoggerService);
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
        public static FactorServiceExplanation DtoToEntityUpdate(FactorServiceExplanationUpdateDto dto, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entity = new FactorServiceExplanation();
                entity.Id = dto.Id;
                entity.Title = dto.Title;
                entity.ActivityReference = (ActivityReferenceEnum)dto.ActivityReference;
                entity.ProjectID = dto.ProjectID;
                entity.ProposalID = dto.ProposalID;
                entity.DeliverablePen = dto.DeliverablePen;
                entity.ExplanationType = (ExplanationTypeEnum)dto.ExplanationType;
                entity.UnitAmount = dto.UnitAmount;
                entity.TotalAmount = dto.TotalAmount;
                entity.AccelerationRate = dto.AccelerationRate;
                entity.Amount = dto.Amount;
                entity.IsSupplyList = dto.IsSupplyList;
                entity.CommodityId = dto.CommodityId;
                entity.CommodityName = dto.CommodityName;
                entity.CommodityCode = dto.CommodityCode;
                entity.SupplyListId = dto.SupplyListId;
                entity.SupplyListName = dto.SupplyListName;
                entity.ActivityCenterID = dto.ActivityCenterID;
                entity.CurrencyID = dto.CurrencyID;
                entity.UnitOfMeasurementID = dto.UnitOfMeasurementID;
                entity.FactorId = dto.FactorId;
                return entity;

            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return null;
            }
        }
        public static FactorServiceExplanationGetDto EntityToDto(FactorServiceExplanation entity, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dto = new FactorServiceExplanationGetDto();
                dto.Id = entity.Id;
                dto.FactorId = entity.FactorId;
                dto.Title = entity.Title;
                dto.ActivityReference = (int)entity.ActivityReference;
                dto.ProjectID = entity.ProjectID;
                dto.ProposalID = entity.ProposalID;
                dto.DeliverablePen = entity.DeliverablePen;
                dto.ExplanationType = (int)entity.ExplanationType;
                dto.UnitAmount = entity.UnitAmount;
                dto.TotalAmount = entity.TotalAmount;
                dto.AccelerationRate = entity.AccelerationRate;
                dto.Amount = entity.Amount;
                dto.IsSupplyList = entity.IsSupplyList;
                dto.CommodityId = entity.CommodityId;
                dto.CommodityName = entity.CommodityName;
                dto.CommodityCode = entity.CommodityCode;
                dto.SupplyListId = entity.SupplyListId;
                dto.SupplyListName = entity.SupplyListName;
                dto.ActivityCenterID = entity.ActivityCenterID;
                dto.CurrencyID = entity.CurrencyID;
                dto.UnitOfMeasurementID = entity.UnitOfMeasurementID;
              
                return dto;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new FactorServiceExplanationGetDto();
            }
        }
        public static List<FactorServiceExplanationGetDto> EntitiesToDtos(List<FactorServiceExplanation> entities, IErrorLoggerService errorLoggerService)
        {
            var dtos = new List<FactorServiceExplanationGetDto>();
            foreach (FactorServiceExplanation entity in entities)
            {
                try
                {
                    var dto = new FactorServiceExplanationGetDto();
                    dto = EntityToDto(entity, errorLoggerService);
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
