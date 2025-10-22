using AppCore.Entities.SettingEntities.Statuses;
using ApplicationService.DtoModels.SettingDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Mapper.SettingMapper
{
    internal static class StatusAutoMapperProfile
    {
        public static Status DtoToEntity(StatusDto dto)
        {
            var entity = new Status();
            entity.Title = dto.title;
            entity.ID = dto.key;
            entity.Order = (int)dto.row;
            return entity;
        }
        public static StatusDto EntityToDto(Status entity)
        {
            var dto = new StatusDto();
            dto.title = entity.Title;
            dto.key = entity.ID;
            dto.row = entity.Order;
            return dto;
        }
        public static List<Status> DtosToEntities(List<StatusDto> dtos)
        {
            var entities = new List<Status>();
            for (int i = 0; i < dtos.Count; i++)
            {
                var entity = new Status();
                entity = DtoToEntity(dtos[i]);
                entities.Add(entity);
            }
            return entities;
        }
        public static List<StatusDto> EntitiesToDtos(List<Status> entities)
        {
            var dtos = new List<StatusDto>();
            for (int i = 0; i < entities.Count; i++)
            {
                var dto = new StatusDto();
                dto = EntityToDto(entities[i]);
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}
