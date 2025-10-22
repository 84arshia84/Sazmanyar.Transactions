using ApplicationService.DtoModels.SettingDtos;
using AppCore.Entities.SettingEntities.CreditSources;
using AppCore.Entities.SettingEntities.ReasonForCancellations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Mapper.SettingMapper
{
    internal class ReasonForCancellationAutoMapperProfile
    {
        public static ReasonForCancellation DtoToEntity(ReasonForCancellationDto dto)
        {
            var entity = new ReasonForCancellation();
            entity.Title = dto.title;
            entity.ID = dto.key;
            entity.Order = (int)dto.row;
            return entity;
        }
        public static ReasonForCancellationDto EntityToDto(ReasonForCancellation entity, Int64 row)
        {
            var dto = new ReasonForCancellationDto();
            dto.title = entity.Title;
            dto.row = row;
            dto.key = entity.ID;
            return dto;
        }
        public static List<ReasonForCancellation> DtosToEntities(List<ReasonForCancellationDto> dtos)
        {
            var entities = new List<ReasonForCancellation>();
            for (int i = 0; i < dtos.Count; i++)
            {
                var entity = new ReasonForCancellation();
                entity = DtoToEntity(dtos[i]);
                entities.Add(entity);
            }
            return entities;
        }
        public static List<ReasonForCancellationDto> EntitiesToDtos(List<ReasonForCancellation> entities)
        {
            var dtos = new List<ReasonForCancellationDto>();
            for (int i = 0; i < entities.Count; i++)
            {
                var dto = new ReasonForCancellationDto();
                dto = EntityToDto(entities[i], i+1);
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}
