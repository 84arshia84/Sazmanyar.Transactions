using ApplicationService.DtoModels.SettingDtos;
using AppCore.Entities.SettingEntities.ReleaseConditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Mapper.SettingMapper
{
    internal class ReleaseConditionAutoMapperProfile
    {
        public static ReleaseCondition DtoToEntity(ReleaseConditionDto dto)
        {
            var entity = new ReleaseCondition();
            entity.Title = dto.title;
            entity.ID = dto.key;
            entity.Order = (int)dto.row;
            return entity;
        }
        public static ReleaseConditionDto EntityToDto(ReleaseCondition entity, Int64 row)
        {
            var dto = new ReleaseConditionDto();
            dto.title = entity.Title;
            dto.row = row;
            dto.key = entity.ID;
            return dto;
        }
        public static List<ReleaseCondition> DtosToEntities(List<ReleaseConditionDto> dtos)
        {
            var entities = new List<ReleaseCondition>();
            for (int i = 0; i < dtos.Count; i++)
            {
                var entity = new ReleaseCondition();
                entity = DtoToEntity(dtos[i]);
                entities.Add(entity);
            }
            return entities;
        }
        public static List<ReleaseConditionDto> EntitiesToDtos(List<ReleaseCondition> entities)
        {
            var dtos = new List<ReleaseConditionDto>();
            for (int i = 0; i < entities.Count; i++)
            {
                var dto = new ReleaseConditionDto();
                dto = EntityToDto(entities[i], i + 1);
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}
