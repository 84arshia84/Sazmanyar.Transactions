using AppCore.Entities.FactorInformation.FactorNettingProcessItems;
using AppCore.Entities.FactorInformation.Factors;
using AppCore.Entities.User;
using ApplicationService.DtoModels.FactorDtos.FactorNettingProcessItem;
using ApplicationService.ServicesContract.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Mapper.FactorMappers
{
    internal static class FactorNettingProcessItemAutoMapperProfile
    {
        public static FactorNettingProcessItem DtoToEntityAdd(FactorNettingProcessItemAddDto dto,Guid userId , IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entity = new FactorNettingProcessItem();
                entity.Id = Guid.NewGuid();
                entity.Title = dto.Title;
                entity.Percentage = dto.Percentage;
                entity.Amount = dto.Amount;
                entity.IsDeduction = dto.IsDeduction;
                entity.IsEditable = true;
                entity.IsDeleted = false;
                entity.InsertDate = DateTime.Now;
                entity.InsertBy = userId;
                entity.FactorId = dto.FactorId;
                entity.NettingProcessTypes = 0;
                entity.CurrencyId = dto.CurrencyId;
                return entity;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return null;
            }
        }
        public static FactorNettingProcessItem DtoToEntityUpdate(FactorNettingProcessItemUpdateDto dto, FactorNettingProcessItem lastData, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entity = new FactorNettingProcessItem();
                entity.Id = dto.Id;
                entity.Title = dto.Title;
                entity.Percentage = dto.Percentage;
                entity.Amount = dto.Amount;
                entity.IsDeduction = lastData.IsDeduction;
                entity.IsEditable = lastData.IsEditable;
                entity.IsDeleted = false;
                entity.InsertDate = lastData.InsertDate;
                entity.InsertBy = lastData.InsertBy;
                entity.FactorId = lastData.FactorId;
                entity.NettingProcessTypes = lastData.NettingProcessTypes;
                entity.CurrencyId = dto.CurrencyId;
                return entity;

            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return null;
            }
        }
        public static FactorNettingProcessItemGetDto EntityToDto(FactorNettingProcessItem entity, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dto = new FactorNettingProcessItemGetDto();
                dto.Id = entity.Id;
                dto.FactorId = entity.FactorId;
                dto.Title = entity.Title;
                dto.Percentage = entity.Percentage;
                dto.Amount = entity.Amount;
                dto.IsDeduction = entity.IsDeduction;
                dto.IsEditable = entity.IsEditable;
                dto.CurrencyId = entity.CurrencyId;
                return dto;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new FactorNettingProcessItemGetDto();
            }
        }
        public static List<FactorNettingProcessItemGetDto> EntitiesToDtos(List<FactorNettingProcessItem> entities, IErrorLoggerService errorLoggerService)
        {
            var dtos = new List<FactorNettingProcessItemGetDto>();
            foreach (FactorNettingProcessItem entity in entities)
            {
                try
                {
                    var dto = new FactorNettingProcessItemGetDto();
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
