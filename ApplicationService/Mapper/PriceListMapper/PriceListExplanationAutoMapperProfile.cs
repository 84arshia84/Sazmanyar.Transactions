using ApplicationService.DtoModels.PriceListsDtos;
using AppCore.Entities.PriceListEntities.PriceListExplanations;
using ApplicationService.ServicesContract.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Mapper.PriceListMapper
{
    internal static class PriceListExplanationAutoMapperProfile
    {

        public static PriceListExplanation DtoToEntity(PriceListExplanationsDto dto, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entity = new PriceListExplanation();
                entity.ID = dto.id;
                entity.RowNumber = dto.RowNumber;
                entity.UnitPrice = dto.UnitPrice;
                entity.PriceListClauseID = dto.PriceListClauseID;
                entity.Explanation = dto.Explanation;
                entity.Unit = dto.Unit;
                entity.IsStar = dto.IsStar;
                return entity;
            }
            catch (Exception ex)
            {
                return new PriceListExplanation();
            }


        }
        public static PriceListExplanationsDto EntityToDto(PriceListExplanation entity, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dto = new PriceListExplanationsDto();
                dto.id = entity.ID;
                dto.RowNumber = entity.RowNumber;
                dto.UnitPrice = entity.UnitPrice;
                dto.PriceListClauseID = entity.PriceListClauseID;
                dto.Explanation = entity.Explanation;
                dto.Unit = entity.Unit;
                dto.IsStar = entity.IsStar;
                return dto;
            }
            catch (Exception ex)
            {
                return new PriceListExplanationsDto();
            }

        }
        public static List<PriceListExplanation> DtosToEntities(List<PriceListExplanationsDto> dtos, IErrorLoggerService errorLoggerService)
        {
            var entities = new List<PriceListExplanation>();
            foreach (var item in dtos)
            {
                var entity = new PriceListExplanation();
                entity = DtoToEntity(item, errorLoggerService);
                entities.Add(entity);
            }
            return entities;
        }
        public static List<PriceListExplanationsDto> EntitiesToDtos(List<PriceListExplanation> entities, IErrorLoggerService errorLoggerService)
        {
            var dtos = new List<PriceListExplanationsDto>();
            foreach (var item in entities)
            {
                var dto = new PriceListExplanationsDto();
                dto = EntityToDto(item, errorLoggerService);
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}
