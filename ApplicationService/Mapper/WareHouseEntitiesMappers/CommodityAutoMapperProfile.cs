using AppCore.Entities.User;
using AppCore.Entities.WareHouseEntities;
using ApplicationService.DtoModels.UserDtos;
using ApplicationService.DtoModels.WareHouseDtos;
using ApplicationService.ServicesContract.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Mapper.WareHouseEntitiesMappers
{
    internal static class CommodityAutoMapperProfile
    {
        public static CommodityDto EntityToDto(Commodity entity, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dto = new CommodityDto();
                dto.key = entity.Id;
                dto.Name = entity.Name;
                dto.CommodityCode = entity.CommodityCode;
                dto.SupplyListId = entity.SupplyListId;
                dto.SupplyListName = entity.SupplyListName;
                dto.SupplyDate = entity.SupplyDate;
                dto.Amount = entity.TheRequiredAmount - (entity.OnTheWayInventory + entity.ReserveInventory);
                if(dto.Amount < 0) {
                    dto.Amount = 0;
                }
                dto.ExitedInventory = entity.ExitedInventory;
                dto.FreeInventory = entity.FreeInventory;
                dto.OnTheWayInventory = entity.OnTheWayInventory;
                dto.ReserveInventory = entity.ReserveInventory;
                dto.TheRequiredAmount = entity.TheRequiredAmount;
                return dto;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new CommodityDto();
            }
        }
        public static List<CommodityDto> EntitiesToDtos(List<Commodity> entities, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dtos = new List<CommodityDto>();
                int count = 1;
                foreach (var item in entities)
                {
                    var dto = new CommodityDto();
                    dto = EntityToDto(item, errorLoggerService);
                    dto.Row = count++;
                    dtos.Add(dto);
                }
                return dtos;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new List<CommodityDto>();
            }
        }
    }
}
