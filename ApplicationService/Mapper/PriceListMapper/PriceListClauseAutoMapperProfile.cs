using ApplicationService.DtoModels.PriceListsDtos;
using AppCore.Entities.PriceListEntities.PriceListClauses;
using AppCore.Entities.PriceListEntities.PriceListFields;
using AppCore.Entities.PriceListEntities.PriceLists;
using ApplicationService.ServicesContract.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Mapper.PriceListMapper
{
    internal static class PriceListClauseAutoMapperProfile
    {
        public static PriceListClause DtoToEntity(PriceListClauseDto dto, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entity = new PriceListClause();
                entity.ID = dto.id;
                entity.PriceListFieldID = dto.FieldID;
                entity.ClauseTitle = dto.CluaseTitle;
                return entity;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new PriceListClause();
            }
        }
        public static PriceListClauseDto EntityToDto(PriceListClause entity, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dto = new PriceListClauseDto();
                dto.id = entity.ID;
                dto.FieldID = entity.PriceListFieldID;
                dto.CluaseTitle = entity.ClauseTitle;
                return dto;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new PriceListClauseDto();
            }
        }
        public static List<PriceListClauseDto> EntitesToDtos(List<PriceListClause> entities, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dtos = new List<PriceListClauseDto>();
                foreach (var item in entities)
                {
                    var dto = new PriceListClauseDto();
                    dto = EntityToDto(item, errorLoggerService);
                    dtos.Add(dto);
                }
                return dtos;
            }
            catch (Exception ex)
            {

                errorLoggerService.SaveError(ex);
                return new List<PriceListClauseDto>();
            }
        }
    }
}
