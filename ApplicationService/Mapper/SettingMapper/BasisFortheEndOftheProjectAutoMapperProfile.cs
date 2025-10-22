using ApplicationService.DtoModels.SettingDtos;
using AppCore.Entities.SettingEntities.BasisForStartingTheProjects;
using AppCore.Entities.SettingEntities.BasisFortheEndOftheProjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Mapper.SettingMapper
{
    internal class BasisFortheEndOftheProjectAutoMapperProfile
    {
        public static BasisFortheEndOftheProject DtoToEntity(BasisFortheEndOftheProjectDto dto)
        {
            var entity = new BasisFortheEndOftheProject();
            entity.Title = dto.title;
            entity.ID = dto.key;
            entity.Order = (int)dto.row;
            return entity;
        }
        public static BasisFortheEndOftheProjectDto EntityToDto(BasisFortheEndOftheProject entity, Int64 row)
        {
            var dto = new BasisFortheEndOftheProjectDto();
            dto.title = entity.Title;
            dto.row = row;
            dto.key = entity.ID;
            return dto;
        }
        public static List<BasisFortheEndOftheProject> DtosToEntities(List<BasisFortheEndOftheProjectDto> dtos)
        {
            var entities = new List<BasisFortheEndOftheProject>();
            for (int i = 0; i < dtos.Count; i++)
            {
                var entity = new BasisFortheEndOftheProject();
                entity = DtoToEntity(dtos[i]);
                entities.Add(entity);
            }
            return entities;
        }
        public static List<BasisFortheEndOftheProjectDto> EntitiesToDtos(List<BasisFortheEndOftheProject> entities)
        {
            var dtos = new List<BasisFortheEndOftheProjectDto>();
            for (int i = 0; i < entities.Count; i++)
            {
                var dto = new BasisFortheEndOftheProjectDto();
                dto = EntityToDto(entities[i], i+1);
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}
