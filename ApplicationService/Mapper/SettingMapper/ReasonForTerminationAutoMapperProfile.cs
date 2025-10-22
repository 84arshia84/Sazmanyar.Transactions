using ApplicationService.DtoModels.SettingDtos;
using AppCore.Entities.SettingEntities.CreditSources;
using AppCore.Entities.SettingEntities.ReasonForTerminations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Mapper.SettingMapper
{
    internal class ReasonForTerminationAutoMapperProfile
    {
        public static ReasonForTermination DtoToEntity(ReasonForTerminationDto dto)
        {
            var entity = new ReasonForTermination();
            entity.Title = dto.title;
            entity.ID = dto.key;
            entity.Order = (int)dto.row;
            return entity;
        }
        public static ReasonForTerminationDto EntityToDto(ReasonForTermination entity, Int64 row)
        {
            var dto = new ReasonForTerminationDto();
            dto.title = entity.Title;
            dto.row = row;
            dto.key = entity.ID;
            return dto;
        }
        public static List<ReasonForTermination> DtosToEntities(List<ReasonForTerminationDto> dtos)
        {
            var entities = new List<ReasonForTermination>();
            for (int i = 0; i < dtos.Count; i++)
            {
                var entity = new ReasonForTermination();
                entity = DtoToEntity(dtos[i]);
                entities.Add(entity);
            }
            return entities;
        }
        public static List<ReasonForTerminationDto> EntitiesToDtos(List<ReasonForTermination> entities)
        {
            var dtos = new List<ReasonForTerminationDto>();
            for (int i = 0; i < entities.Count; i++)
            {
                var dto = new ReasonForTerminationDto();
                dto = EntityToDto(entities[i], i + 1);
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}
