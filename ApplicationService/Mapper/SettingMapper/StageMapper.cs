using ApplicationService.DtoModels.SettingDtos;
using AppCore.Entities.SettingEntities.Stages;
using ApplicationService.ServicesContract.ExceptionHandling;
using ApplicationService.ServicesContract.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Mapper.SettingMapper
{
    internal static class StageMapper
    {

        public static StageDto EntityToDto(Stage entity, IErrorLoggerService errorLogger)
        {
            try
            {
                var dto = new StageDto();
                dto.Title = entity.Title;
                dto.Key = entity.ID;
                return dto;
            }
            catch (Exception ex)
            {
                errorLogger.SaveError(ex);
                return new StageDto();
            }
        }
        public static List<StageDto> EntitiesToDtos(List<Stage> entities, IErrorLoggerService errorLogger)
        {
            var dtos = new List<StageDto>();
            for (int i = 0; i < entities.Count; i++)
            {
                var dto = new StageDto();
                dto = EntityToDto(entities[i], errorLogger);
                dto.Row = i;
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}
