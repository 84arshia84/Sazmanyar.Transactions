using AppCore.Entities.WareHouseEntities;
using ApplicationService.DtoModels.WareHouseDtos;
using ApplicationService.ServicesContract.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Mapper.WareHouseEntitiesMappers
{
    internal class SupplyListAutoMapperProfile
    {
        public static SupplyListDto EntityToDto(SupplyList entity, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dto = new SupplyListDto();
                dto.Id = entity.Id;
                dto.Name = entity.Name;
                dto.ProjectName = entity.ProjectName;
                dto.ProjectId = entity.ProjectId;
                return dto;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new SupplyListDto();
            }
        }
        public static List<SupplyListDto> EntitiesToDtos(List<SupplyList> entities, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dtos = new List<SupplyListDto>();
                foreach (var item in entities)
                {
                    var dto = new SupplyListDto();
                    dto = EntityToDto(item, errorLoggerService);
                    dtos.Add(dto);
                }
                return dtos;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new List<SupplyListDto>();
            }
        }
    }
}
