using ApplicationService.DtoModels.SettingDtos;
using AppCore.Entities.SettingEntities.CreditSources;
using AppCore.Entities.SettingEntities.TypeOfCooperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Mapper.SettingMapper
{
    internal class TypeOfCooperationAutoMapperProfile
    {
        public static TypeOfCooperation DtoToEntity(TypeOfCooperationDto dto)
        {
            var entity = new TypeOfCooperation();
            entity.Title = dto.title;
            entity.FirstWord = dto.firstWord;
            entity.IsDeleted = false;
            entity.ID = dto.key;
            entity.Order = (int)dto.row;
            return entity;
        }
        public static TypeOfCooperationDto EntityToDto(TypeOfCooperation entity, Int64 row)
        {
            var dto = new TypeOfCooperationDto();
            dto.title = entity.Title;
            dto.firstWord = entity.FirstWord;
            dto.row = row;
            dto.key = entity.ID;
            return dto;
        }
        public static List<TypeOfCooperation> DtosToEntities(List<TypeOfCooperationDto> dtos)
        {
            var entities = new List<TypeOfCooperation>();
            for (int i = 0; i < dtos.Count; i++)
            {
                var entity = new TypeOfCooperation();
                entity = DtoToEntity(dtos[i]);
                entities.Add(entity);
            }
            return entities;
        }
        public static List<TypeOfCooperationDto> EntitiesToDtos(List<TypeOfCooperation> entities)
        {
            var dtos = new List<TypeOfCooperationDto>();
            for (int i = 0; i < entities.Count; i++)
            {
                var dto = new TypeOfCooperationDto();
                dto = EntityToDto(entities[i], i + 1);
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}
