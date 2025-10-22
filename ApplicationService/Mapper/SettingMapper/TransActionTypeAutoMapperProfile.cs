using ApplicationService.DtoModels.SettingDtos;
using AppCore.Entities.SettingEntities.CreditSources;
using AppCore.Entities.SettingEntities.TransActionTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Mapper.SettingMapper
{
    internal class TransActionTypeAutoMapperProfile
    {
        public static TransActionType DtoToEntity(TransActionTypeDto dto)
        {
            var entity = new TransActionType();
            entity.Title = dto.title;
            entity.ID = dto.key;
            entity.Order = (int)dto.row;
            return entity;
        }
        public static TransActionTypeDto EntityToDto(TransActionType entity, Int64 row)
        {
            var dto = new TransActionTypeDto();
            dto.title = entity.Title;
            dto.row = row;
            dto.key = entity.ID;
            return dto;
        }
        public static List<TransActionType> DtosToEntities(List<TransActionTypeDto> dtos)
        {
            var entities = new List<TransActionType>();
            for (int i = 0; i < dtos.Count; i++)
            {
                var entity = new TransActionType();
                entity = DtoToEntity(dtos[i]);
                entities.Add(entity);
            }
            return entities;
        }
        public static List<TransActionTypeDto> EntitiesToDtos(List<TransActionType> entities)
        {
            var dtos = new List<TransActionTypeDto>();
            for (int i = 0; i < entities.Count; i++)
            {
                var dto = new TransActionTypeDto();
                dto = EntityToDto(entities[i], i + 1);
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}
