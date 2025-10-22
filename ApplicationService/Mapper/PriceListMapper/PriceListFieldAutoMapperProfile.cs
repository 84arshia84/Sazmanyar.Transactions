using ApplicationService.DtoModels.PriceListsDtos;
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
    internal static class PriceListFieldAutoMapperProfile
    {
        public static PriceListField DtoToEntity(PriceListFieldDto dto, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entity = new PriceListField();
                entity.ID = dto.id;
                entity.FieldTitle = dto.fieldTitle;
                entity.PriceListID = dto.PriceListID;
                return entity;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new PriceListField();
            }
        }
        public static PriceListFieldDto EntityToDto(PriceListField entity, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dto = new PriceListFieldDto();
                dto.id = entity.ID;
                dto.fieldTitle = entity.FieldTitle;
                dto.PriceListID = entity.PriceListID;
                return dto;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new PriceListFieldDto();
            }
        }
        public static List<PriceListFieldDto> EntitesToDtos(List<PriceListField> entities, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dtos = new List<PriceListFieldDto>();
                foreach (var item in entities)
                {
                    var dto = new PriceListFieldDto();
                    dto = EntityToDto(item, errorLoggerService);
                    dtos.Add(dto);
                }
                return dtos;
            }
            catch (Exception ex)
            {

                errorLoggerService.SaveError(ex);
                return new List<PriceListFieldDto>();
            }
        }
    }
}
