using ApplicationService.DtoModels.PriceListsDtos;
using AppCore.Entities.PriceListEntities.PriceLists;
using ApplicationService.ServicesContract.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Mapper.PriceListMapper
{
    internal static class PriceListAutoMapperProfile
    {
        public static PriceList DtoToEntity(PriceListDtos dto, IErrorLoggerService errorLoggerService)
        {
			try
			{
				var entity=new PriceList();
				entity.ID = dto.id;
				entity.Year = dto.Year;
				return entity;
			}
			catch (Exception ex)
			{
				errorLoggerService.SaveError(ex);
                return new PriceList();
			}
        }
    }
}
