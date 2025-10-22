using ApplicationService.DtoModels.SettingDtos;
using AppCore.Entities.SettingEntities.ForGuarantees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Mapper.SettingMapper
{
    internal class ForGuaranteeAutoMapperProfile
    {
        public static ForGuarantee DtoToEntity(ForGuaranteeDto dto)
        {
            var entity = new ForGuarantee();
            entity.Title = dto.title;
            entity.ID = dto.key;
            entity.Order = (int)dto.row;
            return entity;
        }
        public static ForGuaranteeDto EntityToDto(ForGuarantee entity, Int64 row)
        {
            var dto = new ForGuaranteeDto();
            dto.title = entity.Title;
            dto.row = row;
            dto.key = entity.ID;
            return dto;
        }
        public static List<ForGuarantee> DtosToEntities(List<ForGuaranteeDto> dtos)
        {
            var entities = new List<ForGuarantee>();
            for (int i = 0; i < dtos.Count; i++)
            {
                var entity = new ForGuarantee();
                entity = DtoToEntity(dtos[i]);
                entities.Add(entity);
            }
            return entities;
        }
        public static List<ForGuaranteeDto> EntitiesToDtos(List<ForGuarantee> entities)
        {
            var dtos = new List<ForGuaranteeDto>();
            for (int i = 0; i < entities.Count; i++)
            {
                var dto = new ForGuaranteeDto();
                dto = EntityToDto(entities[i], i + 1);
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}
